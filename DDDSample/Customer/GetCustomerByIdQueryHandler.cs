using DDDSample.DataContext;
using DDDSample.Model;
using Microsoft.EntityFrameworkCore;

namespace DDDSample.Customer;


public record CustomerByIdDto(int Id, string FirstName, string LastName, bool IsPreferred, decimal totalSales);

public class GetCustomerByIdQueryHandler
{
    public async Task<CustomerByIdDto> Handle(int customerId, CancellationToken cancellationToken)
    {
        await using var dataContext = new CustomerDataContext();

        var customer = await dataContext.Customer.FirstOrDefaultAsync(c => c.Id == customerId, cancellationToken);

        if (customer == null)
            throw new CustomerDoesNotExistException($"customer with ID: {customer} does not exist");

        return new CustomerByIdDto(customer.Id, customer.FirstName, customer.LastName, customer.IsPreferred, customer.TotalSales);
    }

}