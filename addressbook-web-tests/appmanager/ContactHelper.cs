using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {

        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(int v, ContactData newContact)
        {
            InitContactModification(v);
            FillContactForm(newContact);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(ContactData oldContact, ContactData newContact)
        {
            InitContactModification(oldContact.Id);
            FillContactForm(newContact);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int v)
        {
            SelectContact(v);
            RemoveContact();
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            return this;
        }

        public ContactHelper Remove(ContactData contact)
        {
            SelectContact(contact.Id);
            RemoveContact();
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact(int v)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (v+1) + "]/td/input")).Click();
            return this;
        }

        public ContactHelper SelectContact(string id)
        {
            driver.FindElement(By.Id(id)).Click();
            return this;
        }

        public bool IsContactExist (int v)
        {
            return IsElementPresent(By.XPath("//table[@id='maintable']/tbody/tr[" + v + "]/td/input"));
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if(contactCache == null)
            {
                contactCache = new List<ContactData>();
                ICollection<IWebElement> elements = driver.FindElements(By.XPath("//tr[@name = 'entry']"));
                foreach (IWebElement element in elements)
                {
                    IWebElement lastName = element.FindElement(By.XPath("./td[2]"));
                    IWebElement firstName = element.FindElement(By.XPath("./td[3]"));
                    contactCache.Add(new ContactData(firstName.Text, lastName.Text));
                }

            }

            return contactCache;
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();

            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));

            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string middleName = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string nickName = driver.FindElement(By.Name("nickname")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string faxPhone = driver.FindElement(By.Name("fax")).GetAttribute("value");
            string homePhone2 = driver.FindElement(By.Name("phone2")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePage = driver.FindElement(By.Name("homepage")).GetAttribute("value");

            string bDay = driver.FindElement(By.Name("bday")).FindElement(By.CssSelector("option[selected='selected']")).Text;
            string bMonth = driver.FindElement(By.Name("bmonth")).FindElement(By.CssSelector("option[selected='selected']")).Text;
            string bYear = driver.FindElement(By.Name("byear")).GetAttribute("value");

            string aDay = driver.FindElement(By.Name("aday")).FindElement(By.CssSelector("option[selected='selected']")).Text;
            string aMonth = driver.FindElement(By.Name("amonth")).FindElement(By.CssSelector("option[selected='selected']")).Text;
            string aYear = driver.FindElement(By.Name("ayear")).GetAttribute("value");

            string address2 = driver.FindElement(By.Name("address2")).Text;
            string notes = driver.FindElement(By.Name("notes")).Text;

            return new ContactData(firstName, lastName)
            {
                Middlename = middleName, Nickname = nickName,
                Title = title, Company = company, Address = address, Homepage = homePage,
                Home = homePhone, Mobile = mobilePhone, Work = workPhone, Fax = faxPhone,
                Email = email, Email2 = email2, Email3 = email3,
                Bday = bDay, Bmonth = bMonth, Byear = bYear,
                Aday = aDay, Amonth = aMonth, Ayear = aYear,
                Address2 = address2, Phone2 = homePhone2, Notes = notes
            };
        }

        public string GetContactInformationFromDetailsPage(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactDetails(index);
            string contactText = driver.FindElement(By.CssSelector("div#content")).Text;
            return contactText;
        }

        public string CollectContactToDetails(ContactData contacts)
        {
            string details = "";

            if (contacts.Firstname != "" || contacts.Middlename != "" || contacts.Lastname != "" || contacts.Nickname != ""
                || contacts.Company != "" || contacts.Title != "" || contacts.Address != "")
            {
                if (contacts.Firstname != "" || contacts.Middlename != "" || contacts.Lastname != "")
                {
                    details += System.String.Format(@"{0} {1} {2}", contacts.Firstname, contacts.Middlename, contacts.Lastname);
                    details = Regex.Replace(details, "  ", " ");
                }
                if (contacts.Nickname != "")
                {
                    details += "\n" + contacts.Nickname;
                }
                if (contacts.Title != "")
                {
                    details += "\n" + contacts.Title;
                }
                if (contacts.Company != "")
                {
                    details += "\n" + contacts.Company;
                }
                if (contacts.Address != "")
                {
                    details += "\n" + contacts.Address;
                }
            }

            if (contacts.Home != "" || contacts.Mobile != "" || contacts.Work != "" || contacts.Fax != "")
            {
                details += "\n";

                if (contacts.Home != "")
                {
                    details += "\nH: " + contacts.Home;
                }

                if (contacts.Mobile != "")
                {
                    details += "\nM: " + contacts.Mobile;
                }

                if (contacts.Work != "")
                {
                    details += "\nW: " + contacts.Work;
                }

                if (contacts.Fax != "")
                {
                    details += "\nF: " + contacts.Fax;
                }
            }

            if (contacts.Email != "" || contacts.Email2 != "" || contacts.Email3 != "" || contacts.Homepage != "")
            {
                details += "\n";

                if (contacts.Email != "")
                {
                    details += "\n" + contacts.Email;
                }

                if (contacts.Email2 != "")
                {
                    details += "\n" + contacts.Email2;
                }

                if (contacts.Email3 != "")
                {
                    details += "\n" + contacts.Email3;
                }

                if (contacts.Homepage != "")
                {

                    details += "\nHomepage:\n" + contacts.Homepage;

                }
            }

            if (contacts.Bday != "-" || contacts.Bmonth != "-" || contacts.Byear != "")
            {
                details += "\n\nBirthday";

                if (contacts.Bday != "-")
                {
                    details += " " + contacts.Bday + ".";
                }
                if (contacts.Bmonth != "-")
                {
                    details += " " + contacts.Bmonth;
                }
                if (contacts.Byear != "")
                {
                    details += " " + contacts.Byear + " (" + YearCount(contacts.Bday, contacts.Bmonth, contacts.Byear) + ")";
                }
            }

            if (contacts.Aday != "-" || contacts.Amonth != "-" || contacts.Ayear != "")
            {
                if (contacts.Bday != "-" || contacts.Bmonth != "-" || contacts.Byear != "")
                {
                    details += "\nAnniversary";
                }
                else
                {
                    details += "\n\nAnniversary";
                }

                if (contacts.Aday != "-")
                {
                    details += " " + contacts.Aday + ".";
                }
                if (contacts.Amonth != "-")
                {
                    details += " " + contacts.Amonth;
                }
                if (contacts.Ayear != "" && DateTime.Now.Year >= Int32.Parse(contacts.Ayear))
                {
                    details += " " + contacts.Ayear + " (" + YearCount(contacts.Aday, contacts.Amonth, contacts.Ayear) + ")";
                }
                if (contacts.Ayear != "" && DateTime.Now.Year < Int32.Parse(contacts.Ayear))
                {
                    details += " " + contacts.Ayear;
                }
            }

            if (contacts.Address2 != "")
            {
                details += "\n\n" + contacts.Address2;
            }

            if (contacts.Phone2 != "")
            {
                details += "\n\n" + "P: " + contacts.Phone2;
            }

            if (contacts.Notes != "")
            {
                details += "\n\n" + contacts.Notes;
            }

            details.Trim();
            details = Regex.Replace(details, "  ", " ");
            return details;
        }

        public string YearCount(string day, string month, string year)
        {
            int yearCount = 0;
            int monthToInt = 0;
            int currentDay = DateTime.Now.Day;
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            if (month != "-")
            {
                monthToInt = DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month;
            }

            if (day == "-" && month != "-")
            {
                yearCount = (currentYear - Int32.Parse(year) - 1) +
                  (((currentMonth > monthToInt) ||
                  ((currentMonth == monthToInt))) ? 1 : 0);
            }
            if (month == "-" && day != "-")
            {
                if (currentYear > Int32.Parse(year))
                {
                    yearCount = (currentYear - Int32.Parse(year) - 1);
                }
                else
                {
                    yearCount = 0;
                }
            }

            else
            {
                yearCount = (currentYear - Int32.Parse(year) - 1) +
                      (((currentMonth > monthToInt) ||
                      ((currentMonth == monthToInt) && (currentDay >= Int32.Parse(day)))) ? 1 : 0);
            }

            Console.Out.Write(yearCount);

            return yearCount.ToString();
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper InitContactModification(string id)
        {
            driver.FindElement(By.CssSelector("[href*='edit.php?id=" + id + "']")).Click();
            return this;
        }

        public ContactHelper InitContactDetails(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("nickname"), contact.Nickname);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.Home);
            Type(By.Name("mobile"), contact.Mobile);
            Type(By.Name("work"), contact.Work);
            Type(By.Name("fax"), contact.Fax);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            Type(By.Name("homepage"), contact.Homepage);
            Type(By.Name("byear"), contact.Byear);
            Type(By.Name("ayear"), contact.Ayear);
            Type(By.Name("address2"), contact.Address2);
            Type(By.Name("phone2"), contact.Phone2);
            Type(By.Name("notes"), contact.Notes);

            SelectFromDropDown(By.Name("bday"), contact.Bday);
            SelectFromDropDown(By.Name("bmonth"), contact.Bmonth);
            SelectFromDropDown(By.Name("aday"), contact.Aday);
            SelectFromDropDown(By.Name("amonth"), contact.Amonth);

            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }
    }
}
