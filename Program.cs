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

//string products = @"https://eftechnical.azurewebsites.net/api/products";
//string customer = @"https://eftechnical.azurewebsites.net/api/customer";

//static string getJsonString(string URL)
//{
//    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
//    request.ContentType = "application/json; charset=utf-8";
//    request.PreAuthenticate = true;
//    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
//    using (Stream responseStream = response.GetResponseStream())
//    {
//        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
//        return reader.ReadToEnd();
//    }
//}

//var json = JsonObject.Parse(getJsonString(customer));
//List<Customer_Model> customerJson = JsonSerializer.Deserialize<List<Customer_Model>>(json);

//var json2 = JsonObject.Parse(getJsonString(products));
//List<Product_Model> productJson = JsonSerializer.Deserialize<List<Product_Model>>(json2);



//using (var context = new AppDbContext())
//{
//    context.Customers.AddRange(customerJson);
//    context.Products.AddRange(productJson);
//    context.SaveChanges();
//}


//Console.WriteLine("Data saved successfully.");


//Product_Model customerJson = JsonSerializer.Deserialize<Product_Model>(getJsonString(customer));
//Product_Model productsJson = JsonSerializer.Deserialize<Product_Model>(getJsonString(products));
//Console.WriteLine(customerJson);
