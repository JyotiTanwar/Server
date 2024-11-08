using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ContactManager.Core.Interfaces;
using ContactManager.Core.Models;

public class ContactService : IContactService
{
    private readonly string _filePath;
    private readonly List<Contact> _contacts;

    public ContactService(string filePath)
    {
        _filePath = filePath;

        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, JsonSerializer.Serialize(new List<Contact>()));
        }

        _contacts = LoadContactsFromFile();
    }

    private List<Contact> LoadContactsFromFile()
    {
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Contact>>(json);
    }

    private void SaveContactsToFile()
    {
        var json = JsonSerializer.Serialize(_contacts);
        File.WriteAllText(_filePath, json);
    }

    public IEnumerable<Contact> GetContacts()
    {
        return _contacts;
    }

    public Contact GetContact(int id)
    {
        return _contacts.FirstOrDefault(c => c.Id == id);
    }

    public Contact CreateContact(Contact contact)
    {
        contact.Id = _contacts.Any() ? _contacts.Max(c => c.Id) + 1 : 1;
        _contacts.Add(contact);
        SaveContactsToFile();
        return contact;
    }

    public void UpdateContact(int id, Contact contact)
    {
        var existingContact = _contacts.FirstOrDefault(c => c.Id == id);
        if (existingContact != null)
        {
            existingContact.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;
            existingContact.Email = contact.Email;
            SaveContactsToFile();
        }
    }

    public void DeleteContact(int id)
    {
        var contact = _contacts.FirstOrDefault(c => c.Id == id);
        if (contact != null)
        {
            _contacts.Remove(contact);
            SaveContactsToFile();
        }
    }
}
