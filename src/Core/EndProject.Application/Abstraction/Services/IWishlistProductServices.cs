namespace EndProject.Application.Abstraction.Services;

public interface IWishlistProductServices
{
    Task RemoveAsync(Guid id);
}
