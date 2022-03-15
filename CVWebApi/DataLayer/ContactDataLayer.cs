using CVWebApi.Data;
using CVWebApi.Models;
using CVWebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CVWebApi.DataLayer
{

    public class ContactDataLayer
    {
        private readonly ResumeContext _context;
        public ContactDataLayer(ResumeContext context)
        {
            _context = context;
           

        }
        public List<Contact> GetAllContacts()
        {
            //int count = _context.Contact.ToList().Count;
            //var newContact = new Contact()
            //{
            //    Email = count + "AAAABBB@ABBBA.com",
            //    FirstName = "AAdfdsfdsfA",
            //    LastName = "BBdsfdfdsB",
            //    PhoneNum = "0525545454687214"
            //};
            //AddNewContact(newContact);
            return _context.Contact.ToList();

        }
        public bool AddNewContact(Contact contact)
        {
            if (ValidateContact(contact))
            {
                _context.Contact.Add(contact);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        private bool ValidateContact(Contact contact)
        {
            if (!IsValidEmail(contact.Email))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(contact.FirstName) || !CheckIfOnlyAlphaChars(contact.FirstName))
            {
                return false;
            }


            if (string.IsNullOrWhiteSpace(contact.LastName) || !CheckIfOnlyAlphaChars(contact.LastName))
            {
                return false;
            }


            if (!CheckIfOnlyDigitsChars(contact.PhoneNum))
            {
                return false;
            }

            return true;


        }

        private bool IsValidEmail(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private bool CheckIfOnlyAlphaChars(string str)
        {
            if(string.IsNullOrWhiteSpace(str))
            {
                return true;
            }

            Regex r = new Regex("^[a-z\u0590-\u05fe]+$");
            if (r.IsMatch(str))
            {
                return true;
            }
            return false;
        }

        private bool CheckIfOnlyDigitsChars(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return true;
            }

            Regex r = new Regex(@"^\d+$");
            if (r.IsMatch(str))
            {
                return true;
            }
            return false;
        }

    }
}
