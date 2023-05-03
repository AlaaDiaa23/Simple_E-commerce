namespace E_commerce.RepositoryModels
{
    public class ContactRepo : IContactRepo
    {
        private readonly ApplicationDBContext db;

        public ContactRepo(ApplicationDBContext db)
        {
            this.db = db;
        }
        public void Create(Contact contact)
        {
            db.Contacts.Add(contact);
            db.SaveChanges();
        }

        public List<Contact> GetContacts()
        {
            return db.Contacts.ToList();
        }
    }
}
