using CVWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVWebApi.Data
{
    public class DbInitializer
    {
        public static void Initialize(ResumeContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Contact.Any())
            {
                return;   // DB has been seeded
            }
            Contact contact = new Contact()
            {
                Email = "AAAABBB@AA.com",
                FirstName = "AAA",
                LastName = "BBB",
                PhoneNum = "0525687214"
            };
            context.Contact.Add(contact);

            context.SaveChanges();
        }
    }

}
