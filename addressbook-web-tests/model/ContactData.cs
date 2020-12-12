using System;
using System.Text.RegularExpressions;
using LinqToDB;
using LinqToDB.Mapping;
using System.Linq;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        [Column(Name = "firstname")]
        public string Firstname { get; set; } = "";

        [Column(Name = "middlename")]
        public string Middlename { get; set; } = "";

        [Column(Name = "lastname")]
        public string Lastname { get; set; } = "";

        [Column(Name = "nickname")]
        public string Nickname { get; set; } = "";

        [Column(Name = "title")]
        public string Title { get; set; } = "";

        [Column(Name = "company")]
        public string Company { get; set; } = "";

        [Column(Name = "address")]
        public string Address { get; set; } = "";

        [Column(Name = "home")]
        public string Home { get; set; } = "";

        [Column(Name = "mobile")]
        public string Mobile { get; set; } = "";

        [Column(Name = "work")]
        public string Work { get; set; } = "";

        [Column(Name = "fax")]
        public string Fax { get; set; } = "";

        [Column(Name = "email")]
        public string Email { get; set; } = "";

        [Column(Name = "email2")]
        public string Email2 { get; set; } = "";

        [Column(Name = "email3")]
        public string Email3 { get; set; } = "";

        [Column(Name = "homepage")]
        public string Homepage { get; set; } = "";

        [Column(Name = "bday")]
        public string Bday { get; set; } = "";

        [Column(Name = "bmonth")]
        public string Bmonth { get; set; } = "-";

        [Column(Name = "byear")]
        public string Byear { get; set; } = "";

        [Column(Name = "aday")]
        public string Aday { get; set; } = "";

        [Column(Name = "amonth")]
        public string Amonth { get; set; } = "-";

        [Column(Name = "ayear")]
        public string Ayear { get; set; } = "";

        [Column(Name = "address2")]
        public string Address2 { get; set; } = "";

        [Column(Name = "phone2")]
        public string Phone2 { get; set; } = "";

        [Column(Name = "notes")]
        public string Notes { get; set; } = "";

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        private string allPhones;
        private string allEmails;

        public ContactData()
        {
        }

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public static List<ContactData> GetAll()
        {
            LinqToDB.Data.DataConnection.DefaultSettings = new MySettings();

            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }

        public string AllPhones
        {
            get
            {
                if(allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(Home) + CleanUp(Mobile) + CleanUp(Work) + CleanUp(Phone2)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUp(Email) + CleanUp(Email2) + CleanUp(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") + "\n";
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            int result = Firstname.CompareTo(other.Firstname);
            if (result == 0)
            {
                result = Lastname.CompareTo(other.Lastname);
            }

            return result;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (Lastname == other.Lastname)
            {
                if (Firstname == other.Firstname)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Firstname.GetHashCode();
        }

        public override string ToString()
        {
            return "firstname=" + Firstname + "\nlastname=" + Lastname;
        }
    }
}
