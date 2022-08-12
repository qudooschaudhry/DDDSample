using DDDSample.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DDDSample.Customer;


public record CustomerItemDto(int Id, string FirstName, string LastName, bool IsPreferred);

public class GetCustomersQueryHandler
{
    public async Task<IEnumerable<CustomerItemDto>> Handle(CancellationToken cancellationToken)
    {
        await using var dataContext = new CustomerDataContext();

        var customers = await dataContext.Customer.ToListAsync(cancellationToken);

        return customers.Select(c => new CustomerItemDto(c.Id, c.FirstName, c.LastName, c.IsPreferred));

    }
}