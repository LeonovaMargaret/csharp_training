using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformationWithTable()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }

        [Test]
        public void TestContactInformationWithDetails()
        {
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);
            string fromFormToDetails = app.Contacts.CollectContactToDetails(fromForm);
            string fromDetailsPage = app.Contacts.GetContactInformationFromDetailsPage(0);
            Assert.AreEqual(fromDetailsPage, fromFormToDetails);
        }
    }
}
