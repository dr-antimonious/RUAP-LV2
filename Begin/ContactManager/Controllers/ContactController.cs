using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {
        public string[] Get()
        {
            return new[]
            {
                "Hello",
                "World"
            };
        }
    }
}
