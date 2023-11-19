using ContactManager.Models;
using ContactManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Controllers
{
    [ApiController,
    Route("api/contacts")]
    public class ContactController : Controller
    {
        private ContactRepository contactRepository;

        public ContactController(ContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        [HttpGet,
        Route("get")]
        public Contact[] Get()
        {
            return contactRepository.GetAllContacts();
        }

        [HttpPost,
        Route("post")]
        public IActionResult Post([FromQuery] Contact contact)
        {
            this.contactRepository.SaveContact(contact);

            return Ok();
        }
    }
}
