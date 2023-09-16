namespace EndProject.Application.Abstraction.Services;

public interface IQRCoderServıces
{
    byte[] GenerateQRCode(string text);
}
