using AddressBookApi.Models;

namespace AddressBookApi.Services
{
    public class ContactService
    {
        private readonly List<Contact> _contacts = new();
        private int _nextId = 1;

        public IEnumerable<Contact> GetAll() => _contacts;

        public Contact? GetById(int id) => _contacts.FirstOrDefault(c => c.Id == id);

        public IEnumerable<Contact> Search(string query) =>
            _contacts.Where(c =>
                c.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                c.LastName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                c.Email.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                c.Tag.Contains(query, StringComparison.OrdinalIgnoreCase));

        public Contact Create(Contact contact)
        {
            contact.Id = _nextId++;
            _contacts.Add(contact);
            return contact;
        }

        public bool Update(int id, Contact updated)
        {
            var existing = GetById(id);
            if (existing == null) return false;

            existing.FirstName = updated.FirstName;
            existing.LastName = updated.LastName;
            existing.Email = updated.Email;
            existing.Phone = updated.Phone;
            existing.Tag = updated.Tag;
            return true;
        }

        public bool Delete(int id)
        {
            var contact = GetById(id);
            if (contact == null) return false;
            _contacts.Remove(contact);
            return true;
        }
    }
}
