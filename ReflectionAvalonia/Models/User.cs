using System.Text.Json.Serialization;

namespace ReflectionAvalonia.Models.User
{
    public class User(

        int id, string name,
        string username, string email,
        Address address, string phone,
        string website, Company company)
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = id;
        [JsonPropertyName("name")]
        public string Name { get; set; } = name;
        [JsonPropertyName("username")]
        public string Username { get; set; } = username;
        [JsonPropertyName("email")]
        public string Email { get; set; } = email;
        [JsonPropertyName("address")]
        public Address Address { get; set; } = address;
        [JsonPropertyName("phone")]
        public string Phone { get; set; } = phone;
        [JsonPropertyName("website")]
        public string Website { get; set; } = website;
        [JsonPropertyName("company")]
        public Company Company { get; set; } = company;
    }
    public class Geo(string lat, string lng)
    {
        [JsonPropertyName("lat")]
        public string Lat { get; set; } = lat;
        [JsonPropertyName("lng")]
        public string Lng { get; set; } = lng;
    }

    public class Company(string name, string catchPhrase, string bs)
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = name;
        [JsonPropertyName("catchPhrase")]
        public string CatchPhrase { get; set; } = catchPhrase;
        [JsonPropertyName("bs")]
        public string Bs { get; set; } = bs;
    }

    public class Address(string street, string suite, string city, string zipcode, Geo geo)
    {
        [JsonPropertyName("street")]
        public string Street { get; set; } = street;
        [JsonPropertyName("suite")]
        public string Suite { get; set; } = suite;
        [JsonPropertyName("city")]
        public string City { get; set; } = city;
        [JsonPropertyName("zipcode")]
        public string Zipcode { get; set; } = zipcode;
        [JsonPropertyName("geo")]
        public Geo Geo { get; set; } = geo;
    }
}
