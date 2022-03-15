using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CVWebApi.Data;
using CVWebApi.DataLayer;
using CVWebApi.Models;
using CVWebApi.ViewModels;
using System;
using CVWebApi.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {

        private readonly ResumeContext _context;

        private readonly ILogger<ContactController> _logger;
        private readonly ContactDataLayer service;

        public ContactController(ILogger<ContactController> logger, ResumeContext context)
        {
            _context = context;
            _logger = logger;
            service = new ContactDataLayer(_context);
        }

        [HttpGet("GetAllContacts")]
        public IEnumerable<ContactViewModel> GetAllContacts()
        {
            var allContacts =  service.GetAllContacts();
            List<ContactViewModel> contactsList = new List<ContactViewModel>();
            allContacts.ForEach(contact => {
                var contactVM = contact.MapProperties<ContactViewModel>();
                contactsList.Add(contactVM);

            });

            return contactsList;
        }



        [HttpPost("AddNewContact")]
        public bool AddNewContact(ContactViewModel contactViewModel)
        {
            var contactDb = contactViewModel.MapProperties<Contact>();
            var results = service.AddNewContact(contactDb);
            return results;
        }
    }
}
