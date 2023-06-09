﻿using Business.Concrete;
using DateAccess.Concrete.EntitiyFramework;
using DateAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Data Transformation object

            ProductTest();

            //CategoryTest();


        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());




            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));
            var result = productManager.GetProductDetails();

            if (result.Success==true)
            {
                foreach (var product in result.Data) 
                {
                    Console.WriteLine(product.ProductName + "/" + product.CategoryName);
                    
                }
                Console.WriteLine(result.Message);
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            
        }
    }
}
