using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
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

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            return (List<ContactData>)new XmlSerializer(typeof(List<ContactData>)).Deserialize(new StreamReader(@"contacts.xml"));
        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(@"contacts.json"));
        }

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTestFromRandomDataProvider(ContactData contact)
        {
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Create(contact);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test, TestCaseSource("ContactDataFromXmlFile")]
        public void ContactCreationTestFromXml(ContactData contact)
        {
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Create(contact);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test, TestCaseSource("ContactDataFromJsonFile")]
        public void ContactCreationTestFromJson(ContactData contact)
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
