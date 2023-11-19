using ContactManager.Models;
using Microsoft.Extensions.Caching.Memory;

namespace ContactManager.Services
{
    public class ContactRepository
    {
        private const string CacheKey = "ContactStore";
        private readonly IMemoryCache _memoryCache;

        public ContactRepository(IMemoryCache cache)
        {
            _memoryCache = cache;

            if (!_memoryCache.TryGetValue(CacheKey, out _))
            {
                var contacts = new[]
                {
                    new Contact
                    {
                        Id = 1, Name = "Glenn Block"
                    },
                    new Contact
                    {
                        Id = 2, Name = "Dan Roth"
                    }
                };

                _ = _memoryCache.Set(CacheKey, contacts);
            }
        }

        public Contact[] GetAllContacts()
        {
            if (_memoryCache.TryGetValue(CacheKey, out var contacts) && contacts is not null)
                return (Contact[])contacts;

            return new[]
            {
                new Contact
                {
                    Id = 0,
                    Name = "Placeholder"
                }
            };
        }

        public bool SaveContact(Contact contact)
        {
            Contact[] tempArray;
            List<Contact> tempList;

            try
            {
                if (_memoryCache.TryGetValue(CacheKey, out var currentData))
                {
                    if (currentData is not null)
                    {
                        tempArray = (Contact[]) currentData;
                        tempList = tempArray.ToList();
                        tempList.Add(contact);

                        tempArray = tempList.ToArray();
                    }
                    else
                        tempArray = new[] {contact};

                    _ = _memoryCache.Set(CacheKey, tempArray);
                    return true;
                }

                throw new($"Cache entry {CacheKey} non-existent.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
