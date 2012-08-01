﻿using System;
using System.Net;
using System.Web;
using System.Xml;

namespace Recurly
{
    public class RecurlySubscription
    {
        #region ChangeTimeframe enum

        public enum ChangeTimeframe
        {
            Now,
            Renewal
        }

        #endregion

        #region RefundType enum

        public enum RefundType
        {
            Full,
            Partial,
            None
        }

        #endregion

        private const string UrlPrefix = "/accounts/";
        private const string UrlPostfix = "/subscription";
        private const string ReactivatePostFix = "/subscription/reactivate";
        private DateTime? trialPeriodEndsAt;

        public RecurlySubscription(RecurlyAccount account)
        {
            Account = account;
            Quantity = 1;
        }

        /// <summary>
        /// Account in Recurly
        /// </summary>
        public RecurlyAccount Account { get; private set; }

        public int? Quantity { get; set; }
        public string PlanCode { get; set; }
        public string CouponCode { get; set; }
        public string State { get; private set; }

        // Additional information
        /// <summary>
        /// Date the subscription started.
        /// </summary>
        public DateTime? ActivatedAt { get; private set; }

        /// <summary>
        /// If set, the date the subscriber canceled their subscription.
        /// </summary>
        public DateTime? CanceledAt { get; private set; }

        /// <summary>
        /// If set, the subscription will expire/terminate on this date.
        /// </summary>
        public DateTime? ExpiresAt { get; private set; }

        /// <summary>
        /// Date the current invoice period started.
        /// </summary>
        public DateTime? CurrentPeriodStartedAt { get; private set; }

        /// <summary>
        /// The subscription is paid until this date. Next invoice date.
        /// </summary>
        public DateTime? CurrentPeriodEndsAt { get; private set; }

        /// <summary>
        /// Date the trial started, if the subscription has a trial.
        /// </summary>
        public DateTime? TrialPeriodStartedAt { get; private set; }

        /// <summary>
        /// Date the trial ends, if the subscription has/had a trial.
        /// 
        /// This may optionally be set on new subscriptions to specify an exact time for the 
        /// subscription to commence.  The subscription will be active and in "trial" until
        /// this date.
        /// </summary>
        public DateTime? TrialPeriodEndsAt
        {
            get { return trialPeriodEndsAt; }
            set
            {
                if (ActivatedAt.HasValue)
                    throw new InvalidOperationException("Cannot set TrialPeriodEndsAt on existing subscriptions.");
                if (value.HasValue && (value < DateTime.UtcNow))
                    throw new ArgumentException("TrialPeriodEndsAt must occur in the future.");

                trialPeriodEndsAt = value;
            }
        }

        /// <summary>
        /// Unit amount per quantity.  Leave null to keep as is. Set to override plan's default amount.
        /// </summary>
        public int? UnitAmountInCents { get; set; }

        private static string SubscriptionUrl(string accountCode)
        {
            return UrlPrefix + HttpUtility.UrlEncode(accountCode) + UrlPostfix;
        }

        public static RecurlySubscription Get(string accountCode)
        {
            return Get(new RecurlyAccount(accountCode));
        }

        public static RecurlySubscription Get(RecurlyAccount account)
        {
            var sub = new RecurlySubscription(account);

            HttpStatusCode statusCode = RecurlyClient.PerformRequest(RecurlyClient.HttpRequestMethod.Get,
                                                                     SubscriptionUrl(account.AccountCode),
                                                                     sub.ReadXml);

            if (statusCode == HttpStatusCode.NotFound)
                return null;

            return sub;
        }


        public void Create()
        {
            HttpStatusCode statusCode = RecurlyClient.PerformRequest(RecurlyClient.HttpRequestMethod.Post,
                                                                     SubscriptionUrl(Account.AccountCode),
                                                                     WriteSubscriptionXml,
                                                                     ReadXml);
        }

        public void ChangeSubscription(ChangeTimeframe timeframe)
        {
            RecurlyClient.WriteXmlDelegate writeXmlDelegate;

            if (timeframe == ChangeTimeframe.Now)
                writeXmlDelegate = WriteChangeSubscriptionNowXml;
            else
                writeXmlDelegate = WriteChangeSubscriptionAtRenewalXml;

            HttpStatusCode statusCode = RecurlyClient.PerformRequest(RecurlyClient.HttpRequestMethod.Put,
                                                                     SubscriptionUrl(Account.AccountCode),
                                                                     writeXmlDelegate,
                                                                     ReadXml);
        }

        public static void ReactivateSubscription(string accountCode)
        {
            string reactivateUrl = UrlPrefix + HttpUtility.UrlEncode(accountCode) + ReactivatePostFix;
            RecurlyClient.PerformRequest(RecurlyClient.HttpRequestMethod.Post, reactivateUrl);
        }

        /// <summary>
        /// Cancel an active subscription.  The subscription will not renew, but will continue to be active
        /// through the remainder of the current term.
        /// </summary>
        /// <param name="accountCode">Subscriber's Account Code</param>
        public static void CancelSubscription(string accountCode)
        {
            RecurlyClient.PerformRequest(RecurlyClient.HttpRequestMethod.Delete, SubscriptionUrl(accountCode));
        }

        /// <summary>
        /// Immediately terminate the subscription and issue a refund.  The refund can be for the full amount
        /// or prorated until its paid-thru date.  If you need to refund a specific amount, please issue a
        /// refund against the individual transaction instead.
        /// </summary>
        /// <param name="accountCode">Subscriber's Account Code</param>
        /// <param name="refundType"></param>
        public static void RefundSubscription(string accountCode, RefundType refundType)
        {
            string refundTypeParameter = refundType.ToString().ToLower();

            string refundUrl = String.Format("{0}?refund={1}",
                                             SubscriptionUrl(accountCode),
                                             refundTypeParameter);

            RecurlyClient.PerformRequest(RecurlyClient.HttpRequestMethod.Delete, refundUrl);
        }

        /// <summary>
        /// Terminate the subscription immediately and do not issue a refund.
        /// </summary>
        /// <param name="accountCode"></param>
        public static void TerminateSubscription(string accountCode)
        {
            RefundSubscription(accountCode, RefundType.None);
        }

        #region Read and Write XML documents

        internal void ReadXml(XmlTextReader reader)
        {
            while (reader.Read())
            {
                // End of subscription element, get out of here
                if (reader.Name == "subscription" && reader.NodeType == XmlNodeType.EndElement)
                    break;

                if (reader.NodeType == XmlNodeType.Element)
                {
                    DateTime dateVal;
                    switch (reader.Name)
                    {
                        case "account":
                            Account = new RecurlyAccount(reader);
                            break;

                        case "plan_code":
                            PlanCode = reader.ReadElementContentAsString();
                            break;

                        case "state":
                            State = reader.ReadElementContentAsString();
                            break;

                        case "quantity":
                            Quantity = reader.ReadElementContentAsInt();
                            break;

                        case "unit_amount_in_cents":
                            UnitAmountInCents = reader.ReadElementContentAsInt();
                            break;

                        case "activated_at":
                            if (DateTime.TryParse(reader.ReadElementContentAsString(), out dateVal))
                                ActivatedAt = dateVal;
                            break;

                        case "canceled_at":
                            if (DateTime.TryParse(reader.ReadElementContentAsString(), out dateVal))
                                CanceledAt = dateVal;
                            break;

                        case "expires_at":
                            if (DateTime.TryParse(reader.ReadElementContentAsString(), out dateVal))
                                ExpiresAt = dateVal;
                            break;

                        case "current_period_started_at":
                            if (DateTime.TryParse(reader.ReadElementContentAsString(), out dateVal))
                                CurrentPeriodStartedAt = dateVal;
                            break;

                        case "current_period_ends_at":
                            if (DateTime.TryParse(reader.ReadElementContentAsString(), out dateVal))
                                CurrentPeriodEndsAt = dateVal;
                            break;

                        case "trial_started_at":
                            if (DateTime.TryParse(reader.ReadElementContentAsString(), out dateVal))
                                TrialPeriodStartedAt = dateVal;
                            break;

                        case "trial_ends_at":
                            if (DateTime.TryParse(reader.ReadElementContentAsString(), out dateVal))
                                trialPeriodEndsAt = dateVal;
                            break;

                        case "pending_subscription":
                            // TODO: Parse pending subscription
                            break;
                    }
                }
            }
        }

        protected void WriteSubscriptionXml(XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("subscription"); // Start: subscription

            xmlWriter.WriteElementString("plan_code", PlanCode);

            if (!String.IsNullOrEmpty(CouponCode))
                xmlWriter.WriteElementString("coupon_code", CouponCode);

            if (Quantity.HasValue)
                xmlWriter.WriteElementString("quantity", Quantity.Value.ToString());

            if (UnitAmountInCents.HasValue)
                xmlWriter.WriteElementString("unit_amount_in_cents", UnitAmountInCents.Value.ToString());

            if (TrialPeriodEndsAt.HasValue)
                xmlWriter.WriteElementString("trial_ends_at", TrialPeriodEndsAt.Value.ToString("s"));

            Account.WriteXml(xmlWriter);

            xmlWriter.WriteEndElement(); // End: subscription
        }

        protected void WriteChangeSubscriptionNowXml(XmlTextWriter xmlWriter)
        {
            WriteChangeSubscriptionXml(xmlWriter, ChangeTimeframe.Now);
        }

        protected void WriteChangeSubscriptionAtRenewalXml(XmlTextWriter xmlWriter)
        {
            WriteChangeSubscriptionXml(xmlWriter, ChangeTimeframe.Renewal);
        }

        protected void WriteChangeSubscriptionXml(XmlTextWriter xmlWriter, ChangeTimeframe timeframe)
        {
            xmlWriter.WriteStartElement("subscription"); // Start: subscription

            xmlWriter.WriteElementString("timeframe",
                                         (timeframe == ChangeTimeframe.Now ? "now" : "renewal"));

            if (!String.IsNullOrEmpty(PlanCode))
                xmlWriter.WriteElementString("plan_code", PlanCode);

            if (Quantity.HasValue)
                xmlWriter.WriteElementString("quantity", Quantity.Value.ToString());

            if (UnitAmountInCents.HasValue)
                xmlWriter.WriteElementString("unit_amount_in_cents", UnitAmountInCents.Value.ToString());

            xmlWriter.WriteEndElement(); // End: subscription
        }

        #endregion
    }
}