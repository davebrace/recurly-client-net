using System.Linq;
using System.Xml.Linq;

namespace Recurly.Notifications
{
    public class Account
    {
        public Account(string accountCode, string username, string email, string firstName, string lastName,
                       string companyName)
        {
            AccountCode = accountCode;
            Username = username;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            CompanyName = companyName;
        }

        public Account(XDocument document)
        {
            XElement account = document.Descendants("account").FirstOrDefault();

            if (account == null) return;

            AccountCode = account.Element("account_code").Value;
            Username = account.Element("username").Value;
            Email = account.Element("email").Value;
            FirstName = account.Element("first_name").Value;
            LastName = account.Element("last_name").Value;
            CompanyName = account.Element("company_name").Value;
        }

        public string AccountCode { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string CompanyName { get; private set; }
    }
}