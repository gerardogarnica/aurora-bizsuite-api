namespace Aurora.Framework;

public readonly record struct Address
{
    public string Street1 { get; }
    public string Street2 { get; }
    public string City { get; }
    public string Zip { get; }
    public string State { get; }
    public string Country { get; }

    private Address(string street1, string street2, string city, string zip, string state, string country)
    {
        Street1 = street1;
        Street2 = street2;
        City = city;
        Zip = zip;
        State = state;
        Country = country;
    }

    public static Result<Address> Create(
        string street1,
        string street2,
        string city,
        string zip,
        string state,
        string country)
    {
        return new Address(street1, street2, city, zip, state, country);
    }
}