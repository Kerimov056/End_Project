using System.Net;

namespace EndProjet.Persistance.Exceptions;

public class NotFoundException : Exception, IBaseException
{
    public int StatusCode { get ; set ; }
    public string CustomMessage { get; set; }

    public NotFoundException(string message):base(message)
    {
        StatusCode = (int)HttpStatusCode.BadRequest;
        CustomMessage = message;
    }
}
