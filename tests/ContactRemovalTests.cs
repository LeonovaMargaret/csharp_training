using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
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

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Remove(removedIndex);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.RemoveAt(removedIndex - 1);
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
