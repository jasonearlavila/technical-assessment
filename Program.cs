using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using ProductCustomerDataFetcher.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

class Program
{
    const string ProductsUrl = @"https://eftechnical.azurewebsites.net/api/products";
    const string CustomersUrl = @"https://eftechnical.azurewebsites.net/api/customer";

    static void Main(string[] args)
    {
        try
        {
            var customerJson = GetJsonData<List<Customer_Model>>(CustomersUrl);
            var productJson = GetJsonData<List<Product_Model>>(ProductsUrl);

            SaveDataToDatabase(customerJson, productJson);
            Console.WriteLine("Data saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private static T GetJsonData<T>(string url)
    {
        var jsonString = GetJsonString(url);
        return JsonSerializer.Deserialize<T>(jsonString);
    }

    private static string GetJsonString(string url)
    {
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.ContentType = "application/json; charset=utf-8";
        request.PreAuthenticate = true;

        using (var response = (HttpWebResponse)request.GetResponse())
        using (var responseStream = response.GetResponseStream())
        using (var reader = new StreamReader(responseStream, Encoding.UTF8))
        {
            return reader.ReadToEnd();
        }
    }

    private static void SaveDataToDatabase(List<Customer_Model> customers, List<Product_Model> products)
    {
        using (var context = new AppDbContext())
        {
            context.Customers.AddRange(customers);
            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
