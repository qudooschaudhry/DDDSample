using DDDSample.DataContext;
using DDDSample.Model;
using Microsoft.EntityFrameworkCore;

namespace DDDSample.Customer;

public class BuyCommand
{
    public int CustomerId { get; set; }
    public decimal Amount { get; set; }
}

public class BuyCommandHandler
{
    public async Task Handle(BuyCommand command, CancellationToken cancellationToken)
    {
        await using var dataContext = new CustomerDataContext();

        var customer = await dataContext.Customer.FirstOrDefaultAsync(c => c.Id == command.CustomerId, cancellationToken);

        if (customer == null)
            throw new CustomerDoesNotExistException($"customer with ID: {customer} does not exist");

        customer.Buy(command.Amount);
        await dataContext.SaveChangesAsync(cancellationToken);
    }
}