namespace AspNetCore.VersionInfo.Models
{
    //public class Result
    //{
    //    public bool Success { get; }

    //    public Exception Exception { get; }

    //    private readonly string _errorMessage;
    //    public string ErrorMessage => _errorMessage ?? Exception?.Message;

    //    public Result(bool success = true, string errorMessage = null, Exception exception = null)
    //    {
    //        Success = success;
    //        _errorMessage = errorMessage;
    //        Exception = exception;
    //    }

    //    public static Result Ok() => new(success: true);

    //    public static Result Error(string message) => new(success: false, errorMessage: message);

    //    public static Result Error(Exception error) => new(success: false, exception: error);
    //}


    //public class Result<T>
    //{
    //    public bool Success { get; }
    //    public T Value { get; }

    //    public Exception Exception { get; }

    //    private readonly string _errorMessage;
    //    public string ErrorMessage => _errorMessage ?? Exception?.Message;

    //    private Result(bool success = true, T value = default, string errorMessage = null, Exception exception = null)
    //    {
    //        Success = success;
    //        Value = value;
    //        _errorMessage = errorMessage;
    //        Exception = exception;
    //    }

    //    public static Result<T> Ok(T value) => new(success: true, value: value);

    //    public static Result<T> Error(string errorMessage = null, Exception error = null) => new(success: false, errorMessage: errorMessage, exception: error);

    //    public static implicit operator Result<T>(T value) => Ok(value);

    //    //public static implicit operator Result<T>(Result result) => new(result.Success, default, result.ErrorMessage, result.Exception);
    //}
}
