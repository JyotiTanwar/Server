using ContactManager.Core.Models;
using ContactManager.Core.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contact>> GetContacts()
        {
            return Ok(_contactService.GetContacts());
        }

        [HttpGet("{id}")]
        public ActionResult<Contact> GetContact(int id)
        {
            var contact = _contactService.GetContact(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        public ActionResult<Contact> CreateContact(Contact contact)
        {
            var createdContact = _contactService.CreateContact(contact);
            return CreatedAtAction(nameof(GetContact), new { id = createdContact.Id }, createdContact);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, Contact contact)
        {
            var existingContact = _contactService.GetContact(id);
            if (existingContact == null)
            {
                return NotFound();
            }
            _contactService.UpdateContact(id, contact);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            var contact = _contactService.GetContact(id);
            if (contact == null)
            {
                return NotFound();
            }
            _contactService.DeleteContact(id);
            return NoContent();
        }
    }
}
