using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNotes.BusinessLayer.Abstract;
using MyNotes.BusinessLayer.ValueObject;
using MyNotes.DataAccessLayer;
using MyNotes.EntityLayer;

namespace MyNotes.BusinessLayer
{
    public class CategoryManager : ManagerBase<Category>
    {
        private Repository<Category> repo = new Repository<Category>();

        public List<Category> IndexCat()
        {
            return repo.List();
        }
        public int InsertCat(CategoryViewModel cat)
        {
            Category entity = new Category();
            entity.Tittle = cat.Category.Tittle;
            entity.Description = cat.Category.Description;
            //entity.CreatedOn = null;
            //entity.ModifiedUserName = null;
            //entity.ModifiedOn = null;
            return repo.Insert(entity);
        }

        public int UpdateCat(CategoryViewModel cat)
        {
            Category entity = repo.Find(s => s.Id == cat.Category.Id);
            //entity.Id = cat.Category.Id;
            entity.Tittle = cat.Category.Tittle;
            entity.Description = cat.Category.Description;
          
           

            return repo.Update(entity);
        }
        public CategoryViewModel FindCat(int? id)
        {
            var cat = repo.QList().FirstOrDefault(x => x.Id == id);
            CategoryViewModel cvm = new CategoryViewModel();
            cvm.Category.Id = cat.Id;
            cvm.Category.Tittle = cat.Tittle;
            cvm.Category.Description = cat.Description;
            cvm.Category.CreatedOn = cat.CreatedOn;
            cvm.Category.ModifiedUserName = cat.ModifiedUserName;
            cvm.Category.ModifiedOn = cat.ModifiedOn;

            return cvm;

        }
        public CategoryViewModel GetEditCat(int? id)
        {
            var cat = repo.QList().FirstOrDefault(x => x.Id == id);
            CategoryViewModel cvm = new CategoryViewModel();
            cvm.Category.Id = cat.Id;
            cvm.Category.Tittle = cat.Tittle;
            cvm.Category.Description = cat.Description;
            cvm.Category.CreatedOn = cat.CreatedOn;
            cvm.Category.ModifiedUserName = cat.ModifiedUserName;
            cvm.Category.ModifiedOn = cat.ModifiedOn;

            return cvm;

        }

        public int DeleteCat(int? id)
        {

            return repo.Delete(repo.Find(s => s.Id == id));
        }

    }
}
