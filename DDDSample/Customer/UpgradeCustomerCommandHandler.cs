using DDDSample.DataContext;
using DDDSample.Model;
using Microsoft.EntityFrameworkCore;

namespace DDDSample.Customer;

public class UpgradeCustomerCommand
{
    public int CustomerId { get; set; }
}

public class UpgradeCustomerCommandHandler
{
    public async Task Handle(UpgradeCustomerCommand command, CancellationToken cancellationToken)
    {
        await using var dataContext = new CustomerDataContext();

        var customer = await dataContext.Customer.FirstOrDefaultAsync(c => c.Id == command.CustomerId, cancellationToken);

        if (customer == null)
            throw new CustomerDoesNotExistException($"customer with ID: {customer} does not exist");

        customer.Upgrade();

        await dataContext.SaveChangesAsync(cancellationToken);
    }
}