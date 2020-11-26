using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using WebAddressbookTests;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string type = args[0];
            int count = Convert.ToInt32(args[1]);
            StreamWriter writer = new StreamWriter(args[2]);
            string format = args[3];

            if (type == "group")
            {
                List<GroupData> groups = new List<GroupData>();

                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                    {
                        Header = TestBase.GenerateRandomString(10),
                        Footer = TestBase.GenerateRandomString(10)
                    });
                }

                if (format == "xml")
                {
                    WriteGroupsToXmlFile(groups, writer);
                }
                else if (format == "json")
                {
                    WriteGroupsToJsonFile(groups, writer);
                }
                else
                {
                    System.Console.Out.Write("Unrecognized format" + format);
                }
            }
            else if (type == "contacts")
            {
                List<ContactData> contacts = new List<ContactData>();

                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactData(TestBase.GenerateRandomString(30), TestBase.GenerateRandomString(30))
                    {
                        Middlename = TestBase.GenerateRandomString(30),
                        Nickname = TestBase.GenerateRandomString(30),
                        Title = TestBase.GenerateRandomString(30),
                        Company = TestBase.GenerateRandomString(30),
                        Address = TestBase.GenerateRandomString(30),
                        Home = TestBase.GenerateRandomString(30),
                        Mobile = TestBase.GenerateRandomString(14),
                        Work = TestBase.GenerateRandomString(14),
                        Fax = TestBase.GenerateRandomString(14),
                        Email = TestBase.GenerateRandomString(30),
                        Email2 = TestBase.GenerateRandomString(30),
                        Email3 = TestBase.GenerateRandomString(30),
                        Homepage = TestBase.GenerateRandomString(30),

                        Bday = TestBase.GenerateRandomDay(),
                        Bmonth = TestBase.GenerateRandomMonth(),
                        Byear = TestBase.GenerateRandomString(4),
                        Aday = TestBase.GenerateRandomDay(),
                        Amonth = TestBase.GenerateRandomMonth(),
                        Ayear = TestBase.GenerateRandomString(4),

                        Address2 = TestBase.GenerateRandomString(30),
                        Phone2 = TestBase.GenerateRandomString(30),
                        Notes = TestBase.GenerateRandomString(200)
                    });
                }

                if (format == "xml")
                {
                    WriteContactsToXmlFile(contacts, writer);
                }
                else if (format == "json")
                {
                    WriteContactsToJsonFile(contacts, writer);
                }
                else
                {
                    System.Console.Out.Write("Unrecognized format" + format);
                }
            }
            else
            {
                System.Console.Out.Write("Unrecognized type" + type);
            }

            writer.Close();
        }

        static void WriteGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void WriteContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        static void WriteGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }

        static void WriteContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
