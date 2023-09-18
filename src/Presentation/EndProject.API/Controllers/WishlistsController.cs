using EndProject.Application.Abstraction.Services;
using EndProjet.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WishlistsController : ControllerBase
{
    private readonly IWishlistServices _wishlistServices;

    public WishlistsController(IWishlistServices wishlistServices)
      =>  _wishlistServices = wishlistServices;

    [HttpPost]
    public async Task<IActionResult> AddBasket([Required][FromQuery] Guid Id, [Required][FromQuery] string AppUserId)
    {
        await _wishlistServices.AddWishlistAsync(Id, AppUserId);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet]
    public async Task<IActionResult> GetWishlistProducts([Required][FromQuery] string AppUserId)
    {
        return Ok(await _wishlistServices.GetWishlistProductsAsync(AppUserId));
    }

    [HttpDelete("Delete-Wishlist-Product")]
    public async Task<IActionResult> DeleteWishlistProduct([Required][FromQuery] Guid Id, [Required][FromQuery] string AppUserId)
    {
        await _wishlistServices.DeleteWishlistAsync(Id, AppUserId);          
        return Ok();
    }

    [HttpDelete("ProductItem")]
    public async Task<IActionResult> DeleteWishlistItemProduct([Required][FromQuery] Guid Id, [Required][FromQuery] string AppUserId)
    {
        await _wishlistServices.DeleteWishlistItemAsync(Id, AppUserId);
        return Ok();
    }

    [HttpGet("Get-Wishlist-Count")]
    public async Task<IActionResult> GetWishlistCount([Required][FromQuery] string AppUserId)  
    {
        var basketCount = await _wishlistServices.GetWishlistCountAsync(AppUserId);
        return Ok(basketCount);
    }
}
