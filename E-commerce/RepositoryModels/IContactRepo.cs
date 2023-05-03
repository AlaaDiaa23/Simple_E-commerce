namespace E_commerce.RepositoryModels
{
    public interface IContactRepo
    {
        void Create(Contact contact);
        List<Contact> GetContacts();
    }
}
