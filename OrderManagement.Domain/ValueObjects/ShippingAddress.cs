using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.ValueObjects
{
    public class ShippingAddress
    {
        public string Country { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public int Postcode { get; private set; }

        public ShippingAddress(string country, string city, string street, int postcode)
        {
            if (string.IsNullOrEmpty(country) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(street))
                throw new ArgumentNullException("Address fields cannot be null");

            if (postcode <= 0)
                throw new ArgumentOutOfRangeException("Postcode must be positive and greater than zero");

            Country = country;
            City = city;
            Street = street;
            Postcode = postcode;
        }

        public override string ToString()
        {
            return $"Address - {Country}, {City}, {Street}: {Postcode}";
        }

        public override bool Equals(object? obj) =>
            obj is ShippingAddress shippingAddress 
            && Country == shippingAddress.Country
            && City == shippingAddress.City
            && Street == shippingAddress.Street
            && Postcode == shippingAddress.Postcode;

        public override int GetHashCode()
        {
            return Country.GetHashCode() + City.GetHashCode() 
                + Street.GetHashCode() + Postcode.GetHashCode();
        }
    }
}
