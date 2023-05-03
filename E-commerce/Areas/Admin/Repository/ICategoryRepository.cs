namespace E_commerce.Areas.Admin.Repository
{
    public  interface ICategoryRepository
    {
        List<Category> GettAll();
          Task AddAsync(Category category,IFormFile file);
       void Edit(Category category);
        Category GetCategory(int id);
        void Delete (Category c);



    }
}
