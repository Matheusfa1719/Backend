public class Result<T>
{
    public T? Value { get; set; }
    public bool Success { get; set; }
    public string? Error { get; set; }

    public bool IsSuccess => Success;
    public bool IsError => !Success;

    public static Result<T> Ok(T value)
    {
        return new Result<T>
        {
            Value = value,
            Success = true,
        };
    }

    public static Result<T> Fail(string error)
    {
        return new Result<T>
        {
            Success = false,
            Error = error,
        };
    }

}