using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            int removedIndex = 2;

            if (!app.Contacts.IsContactExist(removedIndex))
            {
                ContactData contact = new ContactData("a", "c");
                app.Contacts.Create(contact);
            }

            List<ContactData> oldContacts = ContactData.GetAll();
            app.Contacts.Remove(oldContacts[removedIndex]);

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.RemoveAt(removedIndex);

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
