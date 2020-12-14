using System;

namespace WebAddressbookTests
{
    public class ContactGroupHelper : HelperBase
    {
        public ContactGroupHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void CheckIfContactOrGroupExists()
        {
            if (!(manager.Contacts.IsContactExist(2) || manager.Groups.IsGroupExist(0)))
            {
                if (!manager.Contacts.IsContactExist(2))
                {
                    ContactData contactToCreate = new ContactData("a", "c");
                    manager.Contacts.Create(contactToCreate);
                }
                if (!manager.Groups.IsGroupExist(0))
                {
                    GroupData groupToCreate = new GroupData("aaa");
                    manager.Groups.Create(groupToCreate);
                }
            }
        }
    }
}
