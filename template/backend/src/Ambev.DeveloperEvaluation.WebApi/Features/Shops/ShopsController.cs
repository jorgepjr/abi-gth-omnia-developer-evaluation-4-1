using Ambev.DeveloperEvaluation.Application.Shops.CreateShop;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Shops.CreateShop;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Shops;

[ApiController]
[ApiExplorerSettings(GroupName = "app"), Tags("2 - Shops")]
[Route("api/[controller]")]
public class ShopsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ShopsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateShop([FromBody] CreateShopRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateShopCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateShopResponse>
        {
            Success = true,
            Message = "Shop created successfully",
            Data = _mapper.Map<CreateShopResponse>(response),
        }); 
    }
}