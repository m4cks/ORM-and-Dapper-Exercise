using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {


            // ============= CONFIGURATION ==================


            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            
            IDbConnection conn = new MySqlConnection(connString);


            // ============= DEPARTMENT SECTION ==================


            var repo = new DapperDepartmentRepository(conn);
            PrintDepartments(repo.GetAllDepartments());

            var doAgain = true;
            while (doAgain)
            {
                Console.WriteLine("Type 1 to add a department, 2 to delete an existing department, other inputs don't count: ");
                var departmentSection = int.Parse(Console.ReadLine());

                if (departmentSection == 1)
                {
                    Console.WriteLine("To add a new department, Type a Department name: ");

                    var newDepartment = Console.ReadLine();

                    repo.InsertDepartment(newDepartment);
                }
                else if (departmentSection == 2)
                {
                    Console.WriteLine("To delete an existing department, Type its name: ");

                    var newDepartment = Console.ReadLine();

                    repo.DeleteDepartment(newDepartment);
                }
                PrintDepartments(repo.GetAllDepartments());

                Console.WriteLine("================================\nThat was fun! To go again, press 1! else we'll get on with Products!\n================================");
                doAgain = int.Parse(Console.ReadLine()) == 1 ? true : false;
            }


            // ============= PRODUCT SECTION ==================


            var repoProduct = new DapperProductRepository(conn);
            PrintProducts(repoProduct.GetAllProducts());

            doAgain = true;
            while (doAgain)
            {
                Console.WriteLine("Type 1 to Add a product, Type 2 to Update a product, Type 3 to delete a product, other inputs don't count");
                var productInput = int.Parse(Console.ReadLine());


                if (productInput == 1)
                {
                    Console.WriteLine("Type a new Product name (string)");
                    var newProductName = Console.ReadLine();
                    Console.WriteLine("How much does it cost? (double)");
                    var newProductPrice = Double.Parse(Console.ReadLine());
                    Console.WriteLine("What is its category ID? (int)");
                    var newProductCategoryID = int.Parse(Console.ReadLine());

                    repoProduct.CreateProduct(newProductName, newProductPrice, newProductCategoryID);

                }
                else if (productInput == 2)
                {
                    Console.WriteLine("Please type one of the product names above to change, else will not work: ");
                    var oldProduct = Console.ReadLine();
                    Console.WriteLine("Type a new Product name (string)");
                    var newProductName = Console.ReadLine();
                    Console.WriteLine("How much does it cost? (double)");
                    var newProductPrice = Double.Parse(Console.ReadLine());
                    Console.WriteLine("What is its category ID? (int)");
                    var newProductCategoryID = int.Parse(Console.ReadLine());

                    repoProduct.UpdateProduct(oldProduct, newProductName, newProductPrice, newProductCategoryID);

                }
                else if (productInput == 3)
                {
                    Console.WriteLine("Please type a PRODUCT ID (integer) in order to delete a specified product: ");
                    var oldProductID = int.Parse(Console.ReadLine());

                    repoProduct.DeleteProduct(oldProductID);
                }

                PrintProducts(repoProduct.GetAllProducts());

                Console.WriteLine("================================\nThat was fun! To go again, press 1! else we'll stop this thing :)\n================================");
                doAgain = int.Parse(Console.ReadLine()) == 1 ? true : false;
            }


            Console.WriteLine("\n\n================================\n\nThank You! Have a Good Day! :) \n\n================================");
        }


        public static void PrintProducts(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine(product.Name);
            }
        }
        public static void PrintDepartments(IEnumerable<Department> departments)
        {
            foreach (var dept in departments)
            {
                Console.WriteLine(dept.Name);
            }
        }
    }
}

