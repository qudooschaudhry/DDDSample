using DDDSample.Customer;
using DDDSample.DataContext;

namespace DDDSample.Model;

public class Customer
{
    public int Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public bool IsPreferred { get; private set; }
    public decimal TotalSales { get; private set; }

    private Customer()
    {
        
    }
    public static Customer Register(string firstName, string lastName)
    {
        if (firstName == null)
            throw new ArgumentNullException(nameof(firstName));
        
        if (lastName == null) 
            throw new ArgumentNullException(nameof(lastName));

        return new Customer()
        {
            FirstName = firstName,
            LastName = lastName,
            IsPreferred = false,
            TotalSales = 0.0m
        };
    }

    public void Upgrade()
    {
        if (TotalSales < 1000.00m)
            throw new CustomerDoesNotQualifyToBePreferredException("This customer does not qualify to be upgraded."); 

        IsPreferred = true;
    }

    public void Buy(decimal amount)
    {
        TotalSales += amount;
    }
}