using System;
using NUnit.Framework;
using System.Collections.Generic;

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

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            GroupData oldData = oldGroups[modifiedIndex - 1];

            app.Groups.Modify(modifiedIndex, newData);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[modifiedIndex - 1].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach(GroupData group in newGroups)
            {
                if(group.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
        }
    }
}
