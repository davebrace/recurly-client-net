using System;
using System.Linq;
using System.Xml.Linq;

namespace Recurly.Notifications
{
    public class Transaction
    {
        public Transaction(XDocument document)
        {
            XElement transaction = document.Descendants("transaction").FirstOrDefault();

            if (transaction == null) return;

            Id = transaction.Element("id").Value;
            InvoiceId = transaction.Element("invoice_id").Value;
            Action = transaction.Element("action").Value;
            AmountInCents = int.Parse(transaction.Element("amount_in_cents").Value);
            Date = DateTime.Parse(transaction.Element("date").Value);
            Status = transaction.Element("status").Value;
            Message = transaction.Element("message").Value;
            Reference = transaction.Element("reference").Value;
            CvvResult = transaction.Element("cvv_result").Value;
            AvsResult = transaction.Element("avs_result").Value;
            AvsResultStreet = transaction.Element("avs_result_street").Value;
            AvsResultPostal = transaction.Element("avs_result_postal").Value;
            Test = bool.Parse(transaction.Element("test").Value);
            Voidable = bool.Parse(transaction.Element("voidable").Value);
            Refundable = bool.Parse(transaction.Element("refundable").Value);
        }

        public Transaction(string id, string invoiceId, string action, DateTime date, string status, string message,
                           string reference, string cvvResult, string avsResult, string avsResultStreet,
                           string avsResultPostal, bool test,
                           bool voidable, bool refundable)
        {
            Id = id;
            InvoiceId = invoiceId;
            Action = action;
            Date = date;
            Status = status;
            Message = message;
            Reference = reference;
            CvvResult = cvvResult;
            AvsResult = avsResult;
            AvsResultStreet = avsResultStreet;
            AvsResultPostal = avsResultPostal;
            Test = test;
            Voidable = voidable;
            Refundable = refundable;
        }

        public string Id { get; private set; }
        public string InvoiceId { get; private set; }
        public string Action { get; private set; }
        public int AmountInCents { get; private set; }
        public DateTime Date { get; private set; }
        public string Status { get; private set; }
        public string Message { get; private set; }
        public string Reference { get; private set; }
        public string CvvResult { get; private set; }
        public string AvsResult { get; private set; }
        public string AvsResultStreet { get; private set; }
        public string AvsResultPostal { get; private set; }
        public bool Test { get; private set; }
        public bool Voidable { get; private set; }
        public bool Refundable { get; private set; }
    }
}