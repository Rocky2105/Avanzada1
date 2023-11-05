using WorkingWithEFCore;

Northwind db = new();
WriteLine($"Provider : {db.Database.ProviderName}");

// QueryingCategories();
// QueryingProducts();
// QueryingWithLike();

ListProducts();
//* Using create
// var resultAdd = AddProduct(categoryId: 6, productName: "La Pizza de Don Cangrejo", price: 500M);
// if(resultAdd.affected == 1)
// {
//     WriteLine($"Add product successful with ID : {resultAdd.productId}");
// }
// ListProducts(productsIdToHighlight: new int[] {resultAdd.productId });

//* Using update
// var resultUpdate = IncreaseProductPrice(productNameStartsWith: "La ", amount: 400.20M);
// if(resultUpdate.affected == 1)
// {
//     WriteLine($"Increase price succes for ID: {resultUpdate.productId}");
// }
// ListProducts(productsIdToHighlight: new int[] {resultUpdate.productId});

//* Using delete
// WriteLine("Deleting all products that start with La :");
// WriteLine("Press enter to continue...");
// if(ReadKey(intercept: true).Key == ConsoleKey.Enter)
// {
//     int deleted = DeleteProducts(productNameStartsWith: "La ");
//     WriteLine($"{deleted} products were deleted");
// }
// else
// {
//     WriteLine("Delete was canceled");
// }

Product newProd = new()
{
    CategoryId = 6,
    ProductName = "PRODUCTO UWU",
    Cost = 300M,
    Stock = 30
};

var resultAdd = EfficientAdd(newProd);
if (resultAdd.affected == 1)
{
    WriteLine($"Add product successful with ID : {resultAdd.productId}");
}
ListProducts(productsIdToHighlight: new int[] {resultAdd.productId});

//Create and Update mejorado

newProd.Cost = 500M;
newProd.Stock = 50;

var resultUpdate = EfficentUpdate(newProd);
if (resultUpdate.affected == 1)
{
    WriteLine($"Modifications succesful for ID: {resultUpdate.productId}");
}
ListProducts(productsIdToHighlight: new int[] {resultUpdate.productId});

