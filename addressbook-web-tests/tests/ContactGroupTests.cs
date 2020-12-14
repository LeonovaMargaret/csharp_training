using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class ContactGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            app.ContactGroup.CheckIfContactOrGroupExists();

            int id = 1;

            GroupData group = GroupData.GetAll()[id];
            List<ContactData> oldList = group.GetContacts();

            IEnumerable<ContactData> contactExceptGroups = ContactData.GetAll().Except(oldList);

            if(!contactExceptGroups.Any())
            {
                ContactData contactToCreate = new ContactData("1234", "12390512");
                app.Contacts.Create(contactToCreate);
                contactExceptGroups = ContactData.GetAll().Except(oldList);
            }

            ContactData contact = contactExceptGroups.First();

            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }

        [Test]
        public void TestDeletingContactFromGroup()
        {
            app.ContactGroup.CheckIfContactOrGroupExists();

            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();

            if (oldList.Count == 0)
            {
                /* this part isn't needed because CheckIfContactOrGroupExists() method is already applied if no contacts exist
                 * ContactData contactToAddToGroup = ContactData.GetAll()[0];
                if (contactToAddToGroup == null)
                {
                    ContactData contactToCreate = new ContactData("flasdk", "fj280_0");
                    app.Contacts.Create(contactToCreate);
                    contactToAddToGroup = ContactData.GetAll()[0];
                }*/
                app.Contacts.AddContactToGroup(ContactData.GetAll()[0], group);
                oldList = group.GetContacts();
            }

            ContactData contact = oldList.First();

            app.Contacts.DeleteContactFromGroup(group, contact);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}