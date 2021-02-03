using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager()
        {
        }

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public List<Product> GetAll()
        {
            ///iş kodları
            //Yetkisi var mı ? burada gerekli işler yapılıyor
            return _productDal.GetAll();
            
        }

        public List<Product> GetAllByCategoryId(int id)
        {
            //filtreleme yapıldı category ıd ile .gelen id ile dbdeki id yi karşılaştırır. 
            return _productDal.GetAll(p => p.CategoryId == id);
        }

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            //buradabirim fiyatına göre aralık verilerek filtreleme yapıldı.
            return _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max);
        }
    }
}
