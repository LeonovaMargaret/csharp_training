using System;
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
            app.Contacts.Remove(removedIndex);
        }
    }
}
