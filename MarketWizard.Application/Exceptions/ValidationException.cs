namespace MarketWizard.Application.Exceptions;

public class CustomValidationException(Dictionary<string, string[]> errorsDictionary) : ApplicationException
{
    public  Dictionary<string, string[]> ErrorsDictionary { get; } = errorsDictionary;
}