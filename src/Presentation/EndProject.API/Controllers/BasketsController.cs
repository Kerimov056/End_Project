using EndProject.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketsController : ControllerBase
{
    private readonly IBasketServices _basketService;

    public BasketsController(IBasketServices basketService)
       =>   _basketService = basketService;

    [HttpPost]
    public async Task<IActionResult> AddBasket([Required][FromQuery] Guid Id)
    {

        await _basketService.AddBasketAsync(Id);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet]
    public async Task<IActionResult> GetBasketProducts()
    {
        return Ok(await _basketService.GetBasketProductsAsync());
    }

    [HttpDelete("Delete-Basket-Product")]
    public async Task<IActionResult> DeleteBasketProduct([Required][FromQuery] Guid Id)
    {
        await _basketService.DeleteBasketAsync(Id);          //Isdiyir aaa Roter'i duzelt
        return Ok();
    }

    [HttpDelete("ProductItem")]
    public async Task<IActionResult> DeleteBasketItemProduct([Required][FromQuery] Guid Id)
    {
        await _basketService.DeleteBasketItemAsync(Id);
        return Ok();
    }

    [HttpGet("Get-Basket-Count")]
    public async Task<IActionResult> GetBasketCount()  //baskete nece cur ferqli product oldugunu gosterir.
    {
        var basketCount = await _basketService.GetBasketCountAsync();
        return Ok(basketCount);
    }
}
