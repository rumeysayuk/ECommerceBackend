using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product> {
                new Product{ProductId=1,CategoryId=1,ProductName="Bardak",
                    UnitPrice=15,UnitsInStock=15 },
                new Product{ProductId=2,CategoryId=2,ProductName="Telefon",
                    UnitPrice=1500,UnitsInStock=2 },
                new Product{ProductId=3,CategoryId=3,ProductName="Klavye",
                    UnitPrice=15,UnitsInStock=9 },
                 new Product{ProductId=4,CategoryId=4,ProductName="Fare",
                     UnitPrice=40,UnitsInStock=6 } ,
                new Product{ProductId=5,CategoryId=5,ProductName="Bilgisayar",
                    UnitPrice=855,UnitsInStock=5 }

            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            /*  _products.Remove(product);bu çalışmaz, bu şekilde listeden silme yapmaz çünkü referans tipler referans nosu üzerinden gider aynı no ya sahip olmadığı için gitmezler ve silme olmaz.string bool vs olsa silerdi onlar değer tip gibi çalışıyor bunlar refans tip olduğu için çalışmaz*/
            Product productToDelete = null;
            /*foreach (var p in _products)
            {
                if (product.ProductId == p.ProductId)
                {
                    productToDelete = p;
                }
            }*/
            //Bu da aşağıdaki ile aynı işi yapar .
            //bu her p için o an dolaştığı ürünün ıd si benim parametre ile dolaştığım ürünün ıd sine işitse p yi referans aldık
            productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList();

        }

        public void Update(Product product)
        {
            //gönderdiğim ürün ıd sine sahip olan listedeki ürünü bul 
           Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
            
        }
    }
}
