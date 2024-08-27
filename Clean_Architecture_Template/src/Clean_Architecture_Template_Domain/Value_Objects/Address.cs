﻿namespace Clean_Architecture_Template_Domain_Layer.Value_Objects;

public sealed class Address(string city, string streetNo) : IEquatable<Address>
{
    public string City { get; set; } = city ?? throw new ArgumentNullException(nameof(city));
    public string StreetNo { get; set; } = streetNo ?? throw new ArgumentNullException(nameof(streetNo));

    public Address UpdateCity(string newCity) => new(newCity, this.StreetNo);
    public Address UpdatePostalCode(string newPostalCode) => new(this.City, newPostalCode);


    public bool Equals(Address? addressObject)
    {
        if (addressObject is null) return false;

        if (ReferenceEquals(this, addressObject)) return true;

        return City == addressObject.City && StreetNo == addressObject.StreetNo;
    }
    public override int GetHashCode() => HashCode.Combine(City, StreetNo);
}