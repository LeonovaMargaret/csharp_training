using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newContact = new ContactData("aa", "cc");
            newContact.Middlename = "bb";
            newContact.Nickname = "TestNameEdit";
            newContact.Title = "QA Automation";
            newContact.Company = "Silicon Valley";
            newContact.Address = "NewTestAddress";
            newContact.Home = "NewHome";
            newContact.Mobile = "0601279678";
            newContact.Work = "RemoteWork";
            newContact.Fax = "NewFax";
            newContact.Email = "1t1@test.com";
            newContact.Email2 = "ak21@gmail.com";
            newContact.Email3 = "1@b.ru";
            newContact.Homepage = "HomepageTest";
            newContact.Bday = "7";
            newContact.Bmonth = "October";
            newContact.Byear = "1996";
            newContact.Homepage = "HomepageTest";
            newContact.Aday = "13";
            newContact.Amonth = "October";
            newContact.Ayear = "2020";
            newContact.Address2 = "AddressTest New";
            newContact.Phone2 = "OdessaNewTest";
            newContact.Notes = "201 symbols test";

            int modifiedIndex = 2;

            if (!app.Contacts.IsContactExist(modifiedIndex))
            {
                app.Contacts.Create(newContact);
            }

            List<ContactData> oldContacts = ContactData.GetAll();

            app.Contacts.Modify(oldContacts[modifiedIndex], newContact);

            List<ContactData> newContacts = ContactData.GetAll();

            oldContacts[modifiedIndex].Firstname = newContact.Firstname;
            oldContacts[modifiedIndex].Lastname = newContact.Lastname;

            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
