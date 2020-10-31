using System;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("zzz");
            newData.Header = null;
            newData.Footer = null;

            int modifiedIndex = 1;

            if (!app.Groups.IsGroupExist(modifiedIndex))
            {
                app.Groups.Create(newData);
            }

            app.Groups.Modify(modifiedIndex, newData);
        }
    }
}
