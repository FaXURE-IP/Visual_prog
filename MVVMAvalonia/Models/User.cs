using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MVVMAvalonia.Models.Users
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("address")]
        public Address Address { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("website")]
        public string Website { get; set; }
    }
    internal class Company
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("catchPhrase")]
        public string CatchPhrase { get; set; }
        [JsonPropertyName("bs")]
        public string Bs { get; set; }
    }
    public class Geo
    {
        [JsonPropertyName("lat")]
        public double Lat { get; set; }
        [JsonPropertyName("lng")]
        public double Lng { get; set; }
    }
    public class Address
    {
        [JsonPropertyName("street")]
        public string Street { get; set; }
        [JsonPropertyName("suite")]
        public string Suite { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("zipcode")]
        public string Zipcode { get; set; }
        [JsonPropertyName("geo")]
        public Geo Geo { get; set; }
    }
}
