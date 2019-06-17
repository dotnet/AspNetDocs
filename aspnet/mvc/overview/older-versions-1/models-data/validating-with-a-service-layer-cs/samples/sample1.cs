using System.Collections.Generic;
using System.Linq;

namespace MvcApplication1.Models
{
    public class ProductRepository : MvcApplication1.Models.IProductRepository
    {
        private ProductDBEntities _entities = new ProductDBEntities();

        public IEnumerable<ProductServiceModel> ListProducts()
        {
            return _entities.ProductSet.ProjectTo<ProductServceModel>().ToList();
        }

        public bool CreateProduct(ProductServiceModel productToCreate)
        {
            try
            {
            
                 var product = Mapper.Map<Product>(productToCreate);              
                _entities.AddToProductSet(productToCreate);
                _entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

    public interface IProductRepository
    {
        bool CreateProduct(ProductServiceModel productToCreate);
        IEnumerable<ProductServiceModel> ListProducts();
    }

}
