using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Validation;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Business.CCS;
using System.Linq;
using Core.Utilities.Business;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.NewFolder1;
using Core.Aspects.Caching;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        { 
            _productDal = productDal;
            _categoryService = categoryService;
        }
        //claim 
        [SecuredOperation("prduct.add,admin")]
      [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            // aynı isimde ürün eklenemez ve belirli kategorideki ürün sayısı 15 i geçtiyse yeni ürün eklenemez.
         IResult result=   BusinessRules.Run(CheckIfProductNameExist(product.ProductName), CheckIfProductCountOfCategoryCorrect(product.CategoryId),CheckIfCategoryLimitExceded());

            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);         
        }
       [CacheAspect] //key,value
        public IDataResult< List<Product>> GetAll()
        {
            // iş kodları
            //Yetkisi var mı ? burada gerekli işler yapılıyor
            if (DateTime.Now.Hour == 3)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>( _productDal.GetAll(),Messages.ProductsListed);
            
        }

        public IDataResult< List<Product>> GetAllByCategoryId(int id)
        {
            //filtreleme yapıldı category ıd ile .gelen id ile dbdeki id yi karşılaştırır. 
            return new SuccessDataResult<List<Product>>( _productDal.GetAll(p => p.CategoryId == id));
        }
        [CacheAspect]
     //   [PerformanceAspect(5)]
        public IDataResult< Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>( _productDal.Get(p=>p.ProductId==productId));
        }

        public IDataResult< List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            //buradabirim fiyatına göre aralık verilerek filtreleme yapıldı.
            return new SuccessDataResult<List<Product>> (_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>> (_productDal.GetProductDetails());
        }
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }

        [ValidationAspect(typeof(ProductValidator))]

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = (_productDal.GetAll
             (p => p.CategoryId == categoryId).Count);
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCatogoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExist(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {

            var result = _categoryService.GetAll();
            if (result.Data.Count> 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
     //   [TransactionAspect]
        public IResult AddTransactionalTest(Product product)
        {
            
            Add(product);
            if (product.UnitPrice < 10)
            {
                throw new Exception("");
            }
           
            Add(product);
            return null;
        }
    }
}
