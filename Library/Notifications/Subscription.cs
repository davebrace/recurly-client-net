using System;
using System.Linq;
using System.Xml.Linq;

namespace Recurly.Notifications
{
    public class Subscription
    {
        public Subscription(Plan plan, string state, int quantity, int amountInCents, DateTime activatedAt,
                            DateTime canceledAt,
                            DateTime expiresAt, DateTime currentPeriodStartsAt, DateTime currentPeriodEndsAt,
                            DateTime? trialStartedAt,
                            DateTime? trialEndsAt)
        {
            Plan = plan;
            State = state;
            Quantity = quantity;
            AmountInCents = amountInCents;
            ActivatedAt = activatedAt;
            CanceledAt = canceledAt;
            ExpiresAt = expiresAt;
            CurrentPeriodStartedAt = currentPeriodStartsAt;
            CurrentPeriodEndsAt = currentPeriodEndsAt;
            TrialStartedAt = trialStartedAt;
            TrialEndsAt = trialEndsAt;
        }

        public Subscription(XDocument element)
        {
            XElement subscription = element.Descendants("subscription").FirstOrDefault();
            if (subscription == null) return;

            Plan = new Plan(subscription);
            State = subscription.Element("state").Value;
            Quantity = int.Parse(subscription.Element("quantity").Value);
            AmountInCents = int.Parse(subscription.Element("total_amount_in_cents").Value);
            ActivatedAt = DateTime.Parse(subscription.Element("activated_at").Value);
            CanceledAt = DateTime.Parse(subscription.Element("canceled_at").Value);
            ExpiresAt = DateTime.Parse(subscription.Element("expires_at").Value);
            CurrentPeriodStartedAt = DateTime.Parse(subscription.Element("current_period_started_at").Value);
            CurrentPeriodEndsAt = DateTime.Parse(subscription.Element("current_period_ends_at").Value);

            DateTime trialStartedAt, trialEndsAt;
            if (DateTime.TryParse(subscription.Element("trial_started_at").Value, out trialStartedAt))
                TrialStartedAt = trialStartedAt;
            if (DateTime.TryParse(subscription.Element("trial_ends_at").Value, out trialEndsAt))
                TrialEndsAt = trialEndsAt;
        }

        public Plan Plan { get; private set; }
        public string State { get; private set; }
        public int Quantity { get; private set; }
        public int AmountInCents { get; private set; }
        public DateTime ActivatedAt { get; private set; }
        public DateTime CanceledAt { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public DateTime CurrentPeriodStartedAt { get; private set; }
        public DateTime CurrentPeriodEndsAt { get; private set; }
        public DateTime? TrialStartedAt { get; private set; }
        public DateTime? TrialEndsAt { get; private set; }
    }
}