using ContactManager.Models;
using ContactManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {
        private ContactRepository contactRepository;

        public ContactController(ContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public Contact[] Get()
        {
            return contactRepository.GetAllContacts();
        }
    }
}
