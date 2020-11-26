﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            int removedIndex = 1;

            if (!app.Groups.IsGroupExist(removedIndex))
            {
                GroupData group = new GroupData("aaa");
                app.Groups.Create(group);
            }

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            app.Groups.Remove(removedIndex);
            List<GroupData> newGroups = app.Groups.GetGroupList();
            GroupData toBeRemoved = oldGroups[removedIndex - 1];
            oldGroups.RemoveAt(removedIndex - 1);
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}