using DDDSample.DataContext;
using DDDSample.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample.Customer;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ILogger<CustomersController> _logger;
    private readonly RegisterCustomerCommandHandler _registerCustomerCommandHandler;
    private readonly GetCustomersQueryHandler _getCustomersQueryHandler;
    private readonly GetCustomerByIdQueryHandler _getCustomerByIdQueryHandler;
    private readonly UpgradeCustomerCommandHandler _upgradeCustomerCommandHandler;
    private readonly BuyCommandHandler _buyCommandHandler;
    public CustomersController(
        ILogger<CustomersController> logger,
        RegisterCustomerCommandHandler registerCustomerCommandHandler,
        GetCustomersQueryHandler getCustomersQueryHandler,
        GetCustomerByIdQueryHandler getCustomerByIdQueryHandler, 
        UpgradeCustomerCommandHandler upgradeCustomerCommandHandler, 
        BuyCommandHandler buyCommandHandler)
    {
        _logger = logger;
        _registerCustomerCommandHandler = registerCustomerCommandHandler;
        _getCustomersQueryHandler = getCustomersQueryHandler;
        _getCustomerByIdQueryHandler = getCustomerByIdQueryHandler;
        _upgradeCustomerCommandHandler = upgradeCustomerCommandHandler;
        _buyCommandHandler = buyCommandHandler;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await _getCustomersQueryHandler.Handle(cancellationToken));
        }
        catch
        {
            return Problem();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await _getCustomerByIdQueryHandler.Handle(id, cancellationToken));
        }
        catch (CustomerDoesNotExistException)
        {
            return NotFound();
        }
        catch 
        {
            return Problem();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(RegisterCustomerCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _registerCustomerCommandHandler.Handle(command, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "there was an error.");
            return Problem();
        }
    }

    [HttpPut("upgrade")]
    public async Task<IActionResult> Put(UpgradeCustomerCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _upgradeCustomerCommandHandler.Handle(command, cancellationToken);
            return Ok();
        }
        catch (CustomerDoesNotQualifyToBePreferredException)
        {
            return BadRequest();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "there was an error.");
            return Problem();
        }
    }

    [HttpPut("buy")]
    public async Task<IActionResult> Put(BuyCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _buyCommandHandler.Handle(command, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "there was an error.");
            return Problem();
        }
    }
}