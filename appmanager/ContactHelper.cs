﻿using System;
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
            if (!InitContactModification(v))
            {
                Create(newContact);
                Modify(v, newContact);
                return this;
            }

            FillContactForm(newContact);
            SubmitContactModification();
            ReturnToHomePage();
            
            return this;
        }

        public ContactHelper Remove(int v)
        {
            if (!SelectContact(v))
            {
                ContactData contact = new ContactData("a", "c");
                Create(contact);
                Remove(v);
                return this;
            }

            RemoveContact();
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        public bool SelectContact(int v)
        {
            By selectedContact = By.XPath("//table[@id='maintable']/tbody/tr[" + v + "]/td/input");

            if (IsElementPresent(selectedContact))
            {
                driver.FindElement(selectedContact).Click();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public bool InitContactModification(int index)
        {
            By modifiedContact = By.XPath("(//img[@alt='Edit'])[" + index + "]");

            if (IsElementPresent(modifiedContact))
            {
                driver.FindElement(modifiedContact).Click();
                return true;
            }
            else
            {
                return false;
            }
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
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }
    }
}
