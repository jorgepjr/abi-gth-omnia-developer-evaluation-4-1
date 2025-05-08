using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers;

[ApiController]
[ApiExplorerSettings(GroupName = "app")]
[Route("api/[controller]")]
public class CustomersController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CustomersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateCustomerCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateCustomerResponse>
        {
            Success = true,
            Message = "Customer created successfully",
            Data = _mapper.Map<CreateCustomerResponse>(response),
        }); 
    }
}