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
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("a", "c");
            contact.Middlename = "b";
            contact.Nickname = "TestName";
            contact.Title = "QA";
            contact.Company = "DataArt";
            contact.Address = "TestAddress";
            contact.Home = "Odessa";
            contact.Mobile = "0501279678";
            contact.Work = "OfficeWork";
            contact.Fax = "testFax";
            contact.Email = "1t@test.com";
            contact.Email2 = "ak2@gmail.com";
            contact.Email3 = "1@b.ru";
            contact.Homepage = "HomepageTest";
            contact.Bday = "7";
            contact.Bmonth = "October";
            contact.Byear = "1996";
            contact.Homepage = "HomepageTest";
            contact.Aday = "13";
            contact.Amonth = "October";
            contact.Ayear = "2020";
            contact.Address2 = "AddressTest test";
            contact.Phone2 = "OdessaTest";
            contact.Notes = "200 symbols test";

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
