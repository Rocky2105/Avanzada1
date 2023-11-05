using System.IO.Compression;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WorkingWithEFCore;
partial class Program
{
    static void ListProducts(int[]? productsIdToHighlight = null)
    {
        using (Northwind db = new())
        {
            if ((db.Products is null) || (!db.Products.Any()))
            {
                Fail("There are no products");
            }
            WriteLine("{0,-3} | {1,-35} | {2,8} | {3,5} | {4}",
            "Id", "Product name", "Cost", "Stock", "Disc.");

            foreach (var product in db.Products!)
            {
                ConsoleColor backgroundColor = ForegroundColor;
                if((productsIdToHighlight is not null) && productsIdToHighlight.Contains(product.ProductId))
                {
                    ForegroundColor = ConsoleColor.Green;
                }
                WriteLine($"{product.ProductId:000} | {product.ProductName,-35} | {product.Cost:#,##,0.00,8} | {product.Stock,5} | {product.Discontinued}");
                ForegroundColor = backgroundColor;
            }
        }
    }

    // * CRUD, Create, Read, Update, Delete

    // * Create
    static (int affected, int productId) AddProduct(int categoryId, string productName, decimal? price)
    {
        using (Northwind db = new())
        {
            if(db.Products is null) return (0,0);
            Product p = new(){
                CategoryId = categoryId,
                ProductName = productName,
                Cost = price,
                Stock = 27
            };

            EntityEntry<Product> entity = db.Products.Add(p);
            WriteLine($"State: {entity.State}, ProductId: {p.ProductId}");

            int affected = db.SaveChanges();
            WriteLine($"State: {entity.State}, ProductId: {p.ProductId}");

            return (affected, p.ProductId);
        }
    }

    // * Update
    static (int affected, int productId) IncreaseProductPrice(string productNameStartsWith, decimal amount)
    {
        using (Northwind db = new())
        {
            if(db.Products is null) return (0,0);

            Product updateProduct = db.Products.First(p => p.ProductName!.StartsWith(productNameStartsWith));
            updateProduct.Cost = amount;
            int affected = db.SaveChanges();
            return(affected, updateProduct.ProductId);
        }
    }

    //* Delete
    // static int DeleteProducts(string productNameStartsWith)
    // {
    //     using(Northwind db = new())
    //     {
    //         IQueryable<Product> products = db.Products.
    //         Where(p => p.ProductName.StartsWith(productNameStartsWith));

    //         if((products is null) || (!products.Any()))
    //         {
    //             WriteLine("No products found");
    //         }
    //         else 
    //         {
    //             if(db.Products is null) return 0;

    //         }
    //     }
    // }

    static (int affected, int productId) EfficientAdd(Product product)
    {
        using (Northwind db = new())
        {
            if (db.Products is null) return (0, 0);

            EntityEntry<Product> entity = db.Products.Add(product);
            WriteLine($"State: {entity.State}, ProductId: {product.ProductId}");

            int affected = db.SaveChanges();
            WriteLine($"State: {entity.State}, ProductId: {product.ProductId}");

            return (affected, product.ProductId);
        }
    }

    static (int affected, int productId) EfficentUpdate(Product product)
    {
        using (Northwind db = new())
        {
            if (db.Products is null) return (0, 0);

            Product find = db.Products.Where(p => p.ProductId == product.ProductId).FirstOrDefault()!;

            if (find is null)
            {
                return (0, 0);
            }

            if(!(find.ProductName.Equals(product.ProductName)))
            {
                find.ProductName = product.ProductName;
            }
            if (!(find.SupplierId.Equals(product.SupplierId)))
            {
                find.SupplierId = product.SupplierId;
            }


            if(!(find.CategoryId.Equals(product.CategoryId)))
            {
                find.CategoryId = product.CategoryId;

            }
            if(!(find.QuantityPerUnit!.Equals(product.QuantityPerUnit)))
            {
                find.QuantityPerUnit = product.QuantityPerUnit;

            }
            if (!(find.Cost!.Equals(product.Cost)))
            {
                find.Cost = product.Cost;
            }

            if (!(find.Stock!.Equals(product.Stock)))
            {
                find.Stock = product.Stock;
            }

            if (!(find.UnitsOnOrder!.Equals(product.UnitsOnOrder)))
            {
                find.UnitsOnOrder = product.UnitsOnOrder;
            }

            if (!(find.ReorderLevel!.Equals(product.ReorderLevel)))
            {
                find.ReorderLevel = product.ReorderLevel;
            }

            if (!(find.Discontinued!.Equals(product.Discontinued)))
            {
                find.Discontinued = product.Discontinued;
            }

            int affected = db.SaveChanges();
            return (affected, find.ProductId);
        }
    }
}