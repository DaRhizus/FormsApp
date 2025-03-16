using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormsApp.Models
{
    public class Repository
    {
        private static readonly List<Product> _products = new();
        private static readonly List<Category> _categories = new();

        static Repository()
        {
            _categories.Add(new Category {CategoryId = 1, CategoryName = "Phones"});
            _categories.Add(new Category {CategoryId = 2, CategoryName = "Computers"});
            _categories.Add(new Category {CategoryId = 3, CategoryName = "PC's"});
            
            
            _products.Add(new Product {Id = 1, Name = "Apple IPhone 15", Price = 40000, 
            Image="iphone_15.jpeg", IsActive=true, CategoryId = 1});
            _products.Add(new Product {Id = 2, Name = "Apple IPhone 14", Price = 35000, 
            Image="iphone_14.jpeg", IsActive=false, CategoryId = 1});
            _products.Add(new Product {Id = 3, Name = "Apple IPhone 13", Price = 25000, 
            Image="iphone_13.jpeg", IsActive=true, CategoryId = 1});
            _products.Add(new Product {Id = 4, Name = "Apple IPhone 12", Price = 10000, 
            Image="iphone_12.jpeg", IsActive=true, CategoryId = 1});
            

            _products.Add(new Product {Id = 5, Name="Lenovo Thinkpad", Price = 25000, 
            Image="lenovo_thinkpad.jpeg", IsActive=true, CategoryId=2});
            _products.Add(new Product {Id = 6, Name="Lenovo Ideapad", Price = 30000, 
            Image="lenovo_ideapad.jpeg", IsActive=false, CategoryId=2});
            _products.Add(new Product {Id = 7, Name="Macbook Pro", Price = 60000, 
            Image="macbook_pro.jpeg", IsActive=true, CategoryId=2});
            _products.Add(new Product {Id = 8, Name="Dell Alienware", Price =45000, 
            Image="dell_alienware.jpeg", IsActive=true, CategoryId=2});
        }

        public static List<Product> Products{
            get{
                return _products;
            }
        }

        public int? SelectedCategory{ get; set; }
        public static List<Category> Categories{
            get{
                return _categories;
            }
        }

        public static void CreateProduct(Product product){
            product.Id = (_products.LastOrDefault()?.Id ?? 0) + 1;
            _products.Add(product);
        }

        public static void CreateCategory(Category category){
            _categories.Add(category);
        }

        public static void Edit(Product updatedProduct){
            var entity = _products.FirstOrDefault(x => x.Id == updatedProduct.Id);

            if(entity != null){
                entity.Name = updatedProduct.Name;
                entity.Price = updatedProduct.Price;
                entity.IsActive = updatedProduct.IsActive;
                entity.CategoryId = updatedProduct.CategoryId;
                entity.Image = updatedProduct.Image;
            }
        }

        public static void EditIsActive(Product updatedProduct){
            var entity = _products.FirstOrDefault(x => x.Id == updatedProduct.Id);

            if(entity != null){
                entity.IsActive = updatedProduct.IsActive;
            }
        }


        public static void Delete(Product deleteProduct){
            if(deleteProduct!=null){
                _products.Remove(deleteProduct);
            }
        }
    }
}