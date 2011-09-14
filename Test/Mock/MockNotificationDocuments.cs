using System.Xml.Linq;

namespace Recurly.Test.Mock
{
    public class MockNotificationDocuments
    {
        public XDocument GetSuccessfulPaymentNotificationDocument()
        {
            const string xml =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?> <successful_payment_notification>   <account>     <account_code>verena@test.com</account_code>     <username>verena</username>     <email>verena@test.com</email>     <first_name>Verena</first_name>     <last_name>Test</last_name>     <company_name>Company, Inc.</company_name>   </account>   <transaction>     <id>a5143c1d3a6f4a8287d0e2cc1d4c0427</id>     <invoice_id nil=\"true\"></invoice_id>     <action>purchase</action>     <date type=\"datetime\">2009-11-22T13:10:38-08:00</date>     <amount_in_cents type=\"integer\">1000</amount_in_cents>     <status>Success</status>     <message>Bogus Gateway: Forced success</message>     <reference></reference>     <cvv_result code=\"\"></cvv_result>     <avs_result code=\"\"></avs_result>     <avs_result_street></avs_result_street>     <avs_result_postal></avs_result_postal>     <test type=\"boolean\">true</test>     <voidable type=\"boolean\">true</voidable>     <refundable type=\"boolean\">true</refundable>   </transaction> </successful_payment_notification>";
            return XDocument.Parse(xml);
        }

        public XDocument GetExpiredSubscriptionNotificationDocument()
        {
            const string xml =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?> <expired_subscription_notification>   <account>     <account_code>jane.doe@gmail.com</account_code>     <username></username>     <email>janedoe@gmail.com</email>     <first_name>Jane</first_name>     <last_name>Doe</last_name>     <company_name></company_name>   </account>   <subscription>     <plan>       <plan_code>1dpt</plan_code>       <name>Subscription One</name>     </plan>     <state>expired</state>     <quantity type=\"integer\">1</quantity>     <total_amount_in_cents type=\"integer\">200</total_amount_in_cents>     <activated_at type=\"datetime\">2010-09-23T22:05:03Z</activated_at>     <canceled_at type=\"datetime\">2010-09-23T22:05:43Z</canceled_at>     <expires_at type=\"datetime\">2010-09-24T22:05:03Z</expires_at>     <current_period_started_at type=\"datetime\">2010-09-23T22:05:03Z</current_period_started_at>     <current_period_ends_at type=\"datetime\">2010-09-24T22:05:03Z</current_period_ends_at>     <trial_started_at nil=\"true\" type=\"datetime\">     </trial_started_at><trial_ends_at nil=\"true\" type=\"datetime\"></trial_ends_at>   </subscription> </expired_subscription_notification>";
            return XDocument.Parse(xml);
        }

        public XDocument GetCanceledAccountNotificationDocument()
        {
            const string xml =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?> <canceled_account_notification>   <account>     <account_code>verena@test.com</account_code>     <username></username>     <email>verena@test.com</email>     <first_name>Verena</first_name>     <last_name>Test</last_name>     <company_name></company_name>   </account> </canceled_account_notification>";

            return XDocument.Parse(xml);
        }

        public XDocument GetNewSubscriptionNotificationDocument()
        {
            const string xml =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?> <new_subscription_notification>   <account>     <account_code>1035</account_code>     <username nil=\"true\"></username>     <email>dbrace@buzzreferrals.com</email>     <first_name>Dave</first_name>     <last_name>Brace</last_name>     <company_name>Buzz Referrals</company_name>   </account>   <subscription>     <plan>       <plan_code>basic</plan_code>       <name>Basic</name>     </plan>     <state>active</state>     <quantity type=\"integer\">1</quantity>     <total_amount_in_cents type=\"integer\">9900</total_amount_in_cents>     <activated_at type=\"datetime\">2011-09-14T15:09:47Z</activated_at>     <canceled_at type=\"datetime\"></canceled_at>     <expires_at type=\"datetime\"></expires_at>     <current_period_started_at type=\"datetime\">2011-09-14T15:09:47Z</current_period_started_at>     <current_period_ends_at type=\"datetime\">2011-10-15T07:00:00Z</current_period_ends_at>     <trial_started_at type=\"datetime\">2011-09-14T15:09:47Z</trial_started_at>     <trial_ends_at type=\"datetime\">2011-10-15T07:00:00Z</trial_ends_at>   </subscription> </new_subscription_notification>";
            return XDocument.Parse(xml);
        }

        public XDocument GetEmptyDocument(string type)
        {
            var doc = new XDocument();
            doc.Add(new XElement(type));
            return doc;
        }
    }
}