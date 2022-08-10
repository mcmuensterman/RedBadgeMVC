using MyEventTrackerOG.Data;
using MyEventTrackerOG.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventTrackerOG.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _ctx;
        private Guid _userId;

        public CategoryService(ApplicationDbContext context)
        {
            _ctx = context;
        }

        public bool CreateCategory(CategoryCreate model)
        {
            var categoryEntity = new Category()
            {
                OwnerId = _userId,
                Name = model.Name,
            };

            _ctx.Categories.Add(categoryEntity);
            return _ctx.SaveChanges() == 1;
        }

        public IEnumerable<CategoryListItem> GetAllCategories()
        {
            var categories = _ctx.Categories
                .Where(e => e.OwnerId == _userId)
                .Select(e =>
                    new CategoryListItem()
                    {
                        CategoryId = e.Id,
                        Name = e.Name,
                        OwnerId = e.OwnerId
                    }).ToList();
            return categories;
        }

        public CategoryDetail GetCategoryById(int id)
        {
            var category = _ctx.Categories
                .Single(e => e.Id == id && e.OwnerId == _userId);
            return new CategoryDetail()
            {
                CategoryId = category.Id,
                Name = category.Name,
            };
        }

        public bool UpdateCategory(CategoryEdit model)
        {
            var category = _ctx.Categories.Find(model.CategoryId);
            if (category?.OwnerId != _userId || category is null) return false;
            category.Name = model.Name;


            return _ctx.SaveChanges() == 1;
        }

        public bool DeleteCategory(int categoryId)
        {
            var entity = _ctx.Categories
                .SingleOrDefault(e => e.Id == categoryId && e.OwnerId == _userId);
            _ctx.Categories.Remove(entity);

            return _ctx.SaveChanges() == 1;
        }

        public void SetUserId(Guid userId) => _userId = userId;
    }
}
