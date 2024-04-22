using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class CatalogContextSeed
{
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
        bool checkBrands = productCollection.Find(b => true).Any();
        string fileName = "products.json";
        string directoryPath = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData");
        string filePath = Path.Combine(directoryPath, fileName);

        if (!checkBrands)
        {
            if (File.Exists(filePath))
            {
                var brandsData = File.ReadAllText(filePath);
                var brands = JsonSerializer.Deserialize<List<Product>>(brandsData);

                if (brands != null)
                {
                    foreach (var item in brands)
                    {
                        productCollection.InsertOne(item); // InsertOneAsync yerine InsertOne kullanabilirsiniz.
                    }
                }
            }
            else
            {
                Console.WriteLine($"Dosya bulunamadý: {filePath}");
                // Ýlgili iþlemleri buraya ekleyebilirsiniz, örneðin dosyanýn bulunamamasý durumunda loglama yapabilir veya hata iletisi gösterebilirsiniz.
            }
        }
    }
}