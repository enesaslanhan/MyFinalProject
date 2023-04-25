using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValiation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DateAccess.Abstract;
using DateAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    
    public class ProductManager : IProductService
    {
        IProductDal _ProductDal;
        ICategoryService _categoryService;
        

       
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _ProductDal = productDal;
            _categoryService = categoryService;
            
        }
        [SecuredOperation("product.add")]    
        [ValidationAspect(typeof(ProductValidator))]

        public IResult Add(Product product)
        {
            // business codes
            //aynı isimde ürün eklenemez
            //Eğer mevcut kategori sayısı 15'i gectiyse sisteme yeni ürün eklenemez.
            IResult result=BusinessRules.Run(ChechIfProductNameExists(product.ProductName), 
                ChechIfProductCountofCategoryCorrect(product.CategoryId),
                ChechIfCategoryLimitexceded());
            if (result!=null)
            {
                return result;
            }
            _ProductDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
 
        }
        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            //İş Kodları
            //Yetkisi var mı ?
            if (DateTime.Now.Hour==12)
            {
                return new ErorDataResult<List<Product>>(Messages.MaintenanceTime);

            }
            return new SuccessDataResult<List<Product>>(_ProductDal.GetAll(),Messages.ProductListed);

        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>( _ProductDal.GetAll(p=>p.CategoryId==id));
        }

       // [CacheAspect] 
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>( _ProductDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return  new SuccessDataResult<List<Product>>(_ProductDal.GetAll(p => p.UnitPrice>=min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 16)
            {
                return new ErorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);

            }
            return  new SuccessDataResult<List<ProductDetailDto>>(_ProductDal.GetProductDetails(),Messages.ProductListed);
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product porduct)
        {
            var result = _ProductDal.GetAll(p => p.CategoryId == porduct.CategoryId).Count;
            if (result >= 10)
            {
                return new ErorResult(Messages.ProductCountOfCategoryError);
            }
            throw new NotImplementedException();
        }
        private IResult ChechIfProductCountofCategoryCorrect(int categoryId)
        {
            var result = _ProductDal.GetAll(p => p.CategoryId ==categoryId).Count;
            if (result >= 10)
            {
                return new ErorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private IResult ChechIfProductNameExists(string productName)
        {
            bool result = _ProductDal.GetAll(p => p.ProductName ==productName).Any();
            if (result)
            {
                return new ErorResult(Messages.ProductNameAllreadyExists);
            }
            return new SuccessResult();
        }
        private IResult ChechIfCategoryLimitexceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErorResult(Messages.CategoryLimitExeceded);
            }
            return new SuccessResult();
        }

        public IResult AddTransactionalTest(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
