﻿using DateAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DateAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product> {
                new Product{ProductId=1, CategoryId=1, ProductName="Bardak",UnitPrice=10,UnitsInStock=15},
                new Product { ProductId = 2, CategoryId = 1, ProductName = "Kamera", UnitPrice = 500, UnitsInStock = 3 },
                new Product { ProductId = 3, CategoryId = 2, ProductName = "Telefon", UnitPrice = 1500, UnitsInStock = 2 },
                new Product { ProductId = 4, CategoryId = 2, ProductName = "Klavye", UnitPrice = 150, UnitsInStock = 65 },
                new Product { ProductId = 5, CategoryId = 2, ProductName = "Fare", UnitPrice = 85, UnitsInStock = 1 }
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {

            Product produtcToDelete = null;

            /*
            foreach (var p in _products)
            {
                if (product.ProductId==p.ProductId)
                {
                    produtcToDelete = p;
                }

            }
            */
            produtcToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            //Yukarıdaki kod satırında LINQ kullandık
            _products.Remove(produtcToDelete);
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

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {

            Product produtcToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            produtcToUpdate.ProductName = product.ProductName;
            produtcToUpdate.CategoryId = product.CategoryId;
            produtcToUpdate.UnitPrice = product.UnitPrice;
            produtcToUpdate.UnitsInStock = product.UnitsInStock;
        }

        
       /*
        List<Product> IProductDal.GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }
       */
        
    }
}
