using System;
using System.Data;
using System.Collections.Generic;
using Dapper;
using System.Linq;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }


        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products;").ToList();
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO PRODUCTS (Name, Price, CategoryID) VALUES (@productName, @productPrice, @productCategoryID);",
               new { productName = name, productPrice = price, productCategoryID = categoryID });
        }

        public void UpdateProduct(string oldProduct, string name, double price, int categoryID)
        {
            _connection.Execute("UPDATE PRODUCTS SET Name = @productName, Price = @productPrice, CategoryID = @productCategoryID WHERE Name = @theProduct;",
            new
            {
                productName = name,
                productPrice = price,
                productCategoryID = categoryID,
                theProduct = oldProduct
            });
        }

        public void DeleteProduct(int prodID)
        {
            _connection.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
                new { productID = prodID });

            _connection.Execute("DELETE FROM sales WHERE ProductID = @productID;",
               new { productID = prodID });

            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
               new { productID = prodID });
        }
    }
}
