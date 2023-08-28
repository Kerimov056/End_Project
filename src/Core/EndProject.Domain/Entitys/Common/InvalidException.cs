namespace EndProject.Domain.Entitys.Common;

public class InvalidException : Exception
{
    public InvalidException(string message) : base(message) { }

}