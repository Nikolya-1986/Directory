using Directory.Data;
using Directory.Models;
using Directory.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Directory.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ContactController: ControllerBase
    {
        private readonly ContactDbContext contactDbContext;
        public ContactController(ContactDbContext contactDbContext)
        {
            this.contactDbContext = contactDbContext;
        }

        [HttpGet]
        [Route("[controller]s")]
        public IActionResult GetAllContacts()
        {
            var contacts = contactDbContext.Contacts.ToList();
            return Ok(contacts);
        }

        [HttpGet]
        [Route("[controller]")]
        public IActionResult GetContact(Guid id)
        {
            var contact = contactDbContext.Contacts.Find(id);
            return Ok(contact);
        }

        [HttpPost]
        [Route("add[controller]")]
        public IActionResult AddContact(AddContactDto addContact)
        {
            var domainModelContact = new Contact
            {
                Id = Guid.NewGuid(),
                Name = addContact.Name,
                Email = addContact.Email,
                Phone = addContact.Phone,
                Favorite = addContact.Favorite,
            };
            contactDbContext.Contacts.Add(domainModelContact);
            contactDbContext.SaveChanges();

            return Ok(domainModelContact);
        }

        [HttpPut]
        [Route("update[controller]/{id:guid}")]
        public IActionResult UpdateContact(Guid id, AddContactDto updateContact)
        {
            var contact = contactDbContext.Contacts.Find(id);
            if (contact is null)
            {
                return NotFound();
            }
            contact.Name = updateContact.Name;
            contact.Email = updateContact.Email;
            contact.Phone = updateContact.Phone;
            contact.Favorite = updateContact.Favorite;

            contactDbContext.Contacts.Update(contact);
            contactDbContext.SaveChanges();
            return Ok();
;        }

        [HttpDelete]
        [Route("delete[controller]/{id:guid}")]
        public IActionResult DeleteContact(Guid id)
        {
            var contact = contactDbContext.Contacts.Find(id);
            if (contact is not null)
            {
                contactDbContext.Contacts.Remove(contact);
                contactDbContext.SaveChanges();
            }
            return Ok();
        }
    }
}