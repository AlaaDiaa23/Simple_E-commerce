namespace E_commerce.Areas.Admin.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext db;

        public CategoryRepository(ApplicationDBContext db)
        {
            this.db = db;
        }
        public async Task AddAsync(Category category,IFormFile file)
        {
            if (file != null)
            {

                string ImageName = Guid.NewGuid().ToString() + ".png";
                string FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                using (var stream = File.Create(FilePath))
                {
                    await file.CopyToAsync(stream);
                }
                category.Name = ImageName;

            }
            db.Categories.Add(category);    
            db.SaveChanges();
        }

        public void Delete(Category c)
        {
            var item = db.Categories.Where(m => m.Id == c.Id);

            db.Categories.Remove(c);
            db.SaveChanges();


        }

        public void Edit(Category category)
        {
            var item = db.Categories.Find(category.Id);
            if (item != null)
            {
                db.Categories.Update(item);
                db.SaveChanges();
            }
        }

        public Category GetCategory(int id)
        {
            return db.Categories.Find(id);
        }

        public List<Category> GettAll()
        {
            return db.Categories.ToList();
        }
    }
}
