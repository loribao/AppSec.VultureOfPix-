namespace AppSec.Domain.OV;
public record Result<TValue, TError>
{
    public readonly TValue? Value;
    public readonly TError? Error;

    private bool _isSuccess;
    public bool IsSuccess => _isSuccess;
    private Result(TValue value)
    {
        _isSuccess = true;
        Value = value;
        Error = default;
    }

    private Result(TError error)
    {
        _isSuccess = false;
        Value = default;
        Error = error;
    }

    //happy path
    public static implicit operator Result<TValue, TError>(TValue value) => new Result<TValue, TError>(value);

    //error path
    public static implicit operator Result<TValue, TError>(TError error) => new Result<TValue, TError>(error);

    public Result<TValue, TError> Match(Func<TValue, Result<TValue, TError>> success, Func<TError, Result<TValue, TError>> failure)
    {
        if (_isSuccess)
        {
            return success(Value!);
        }
        return failure(Error!);
    }
    public void Deconstruct(
      out TValue? value,
      out TError? error
    ) => (value, error) = (Value, Error);
    public void Deconstruct(
      out TValue? value,
      out TError? error,
      out bool isSuccess
    ) => (value, error, isSuccess) = (Value, Error, IsSuccess);
}
