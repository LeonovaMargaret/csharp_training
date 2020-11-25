using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomGroupDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(30), GenerateRandomString(30))
                {
                    Middlename = GenerateRandomString(30),
                    Nickname = GenerateRandomString(30),
                    Title = GenerateRandomString(30),
                    Company = GenerateRandomString(30),
                    Address = GenerateRandomString(30),
                    Home = GenerateRandomString(30),
                    Mobile = GenerateRandomString(14),
                    Work = GenerateRandomString(14),
                    Fax = GenerateRandomString(14),
                    Email = GenerateRandomString(30),
                    Email2 = GenerateRandomString(30),
                    Email3 = GenerateRandomString(30),
                    Homepage = GenerateRandomString(30),

                    Bday = GenerateRandomDay(),
                    Bmonth = GenerateRandomMonth(),
                    Byear = GenerateRandomString(4),
                    Aday = GenerateRandomDay(),
                    Amonth = GenerateRandomMonth(),
                    Ayear = GenerateRandomString(4),

                    Address2 = GenerateRandomString(30),
                    Phone2 = GenerateRandomString(30),
                    Notes = GenerateRandomString(200)
                });
            }
            return contacts;
        }

        [Test, TestCaseSource("RandomGroupDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Create(contact);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
