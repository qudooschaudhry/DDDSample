using DDDSample.DataContext;

namespace DDDSample.Customer;

public class RegisterCustomerCommand
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

}

public class RegisterCustomerCommandHandler
{
    public async Task Handle(RegisterCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = Model.Customer.Register(command.FirstName, command.LastName);

        await using var dataContext = new CustomerDataContext();

        await dataContext.Customer.AddAsync(customer, cancellationToken);
        await dataContext.SaveChangesAsync(cancellationToken);
    }

}