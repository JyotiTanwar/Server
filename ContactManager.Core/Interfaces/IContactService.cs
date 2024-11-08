using ContactManager.Core.Models;

namespace ContactManager.Core.Interfaces
{
    public interface IContactService
    {
        IEnumerable<Contact> GetContacts();
        Contact GetContact(int id);
        Contact CreateContact(Contact contact);
        void UpdateContact(int id, Contact contact);
        void DeleteContact(int id);
    }
}
