using System.Net;

namespace EndProjet.Persistance.Exceptions;

public class DublicatedException : Exception, IBaseException
{
    public int StatusCode { get; set; }
    public string CustomMessage { get; set; }

    public DublicatedException(string message):base(message)
    {
        StatusCode = (int)HttpStatusCode.BadRequest;
        CustomMessage = message;
    }
}
