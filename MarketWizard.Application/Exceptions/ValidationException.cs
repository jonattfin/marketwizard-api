namespace MarketWizard.Application.Exceptions;

public class CustomValidationException(Dictionary<string, string[]> errorsDictionary) : Exception
{
    public  Dictionary<string, string[]> ErrorsDictionary { get; } = errorsDictionary;
}