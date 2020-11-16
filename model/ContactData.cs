using System;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string firstname;
        private string middlename = "";
        private string lastname;
        private string nickname = "";
        private string title = "";
        private string company = "";
        private string address = "";
        private string home = "";
        private string mobile = "";
        private string work = "";
        private string fax = "";
        private string email = "";
        private string email2 = "";
        private string email3 = "";
        private string homepage = "";
        private string bday = "";
        private string bmonth = "-";
        private string byear = "";
        private string aday = "";
        private string amonth = "-";
        private string ayear = "";
        private string address2 = "";
        private string phone2 = "";
        private string notes = "";
        private string allPhones;
        private string allEmails;

        public ContactData(string firstname, string lastname)
        {
            this.firstname = firstname;
            this.lastname = lastname;
        }

        public string Firstname { get => firstname; set => firstname = value; }
        public string Middlename { get => middlename; set => middlename = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public string Title { get => title; set => title = value; }
        public string Company { get => company; set => company = value; }
        public string Address { get => address; set => address = value; }
        public string Home { get => home; set => home = value; }
        public string Mobile { get => mobile; set => mobile = value; }
        public string Work { get => work; set => work = value; }
        public string Fax { get => fax; set => fax = value; }
        public string Email { get => email; set => email = value; }
        public string Email2 { get => email2; set => email2 = value; }
        public string Email3 { get => email3; set => email3 = value; }
        public string Homepage { get => homepage; set => homepage = value; }
        public string Bday { get => bday; set => bday = value; }
        public string Bmonth { get => bmonth; set => bmonth = value; }
        public string Byear { get => byear; set => byear = value; }
        public string Aday { get => aday; set => aday = value; }
        public string Amonth { get => amonth; set => amonth = value; }
        public string Ayear { get => ayear; set => ayear = value; }
        public string Address2 { get => address2; set => address2 = value; }
        public string Phone2 { get => phone2; set => phone2 = value; }
        public string Notes { get => notes; set => notes = value; }

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
            return "firstname=" + Firstname + " lastname=" + Lastname;
        }
    }
}
