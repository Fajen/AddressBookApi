using Microsoft.AspNetCore.Mvc;
using AddressBookApi.Models;
using AddressBookApi.Services;

namespace AddressBookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactService _service;

        public ContactsController(ContactService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contact>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Contact> Get(int id)
        {
            var contact = _service.GetById(id);
            if (contact == null)
                return NotFound();
            return Ok(contact);
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Contact>> Search([FromQuery] string q)
        {
            var results = _service.Search(q);
            return Ok(results);
        }

        [HttpPost]
        public ActionResult<Contact> Create(Contact contact)
        {
            var newContact = _service.Create(contact);
            return CreatedAtAction(nameof(Get), new { id = newContact.Id }, newContact);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Contact updated)
        {
            var success = _service.Update(id, updated);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _service.Delete(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
