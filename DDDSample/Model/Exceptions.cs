namespace DDDSample.Model;

public sealed class CustomerDoesNotExistException : Exception
{
    public CustomerDoesNotExistException(string message) : base(message)
    {
        
    }
}

public sealed class CustomerDoesNotQualifyToBePreferredException : Exception
{
    public CustomerDoesNotQualifyToBePreferredException(string message) : base(message)
    {

    }
}