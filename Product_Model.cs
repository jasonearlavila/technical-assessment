using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductCustomerDataFetcher.Models
{
    public class Product_Model
    {
        [JsonPropertyName("id")]
        public int id { get; set; }
        [JsonPropertyName("title")]
        public string title { get; set; }
        [JsonPropertyName("price")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal price { get; set; }
        [JsonPropertyName("description")]
        public string description { get; set; }
        [JsonPropertyName("category")]
        public string category { get; set; }
    }
}
