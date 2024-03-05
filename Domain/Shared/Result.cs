namespace Domain.Shared;

public class Result
{
    protected Result(bool isSuccess, IEnumerable<Error>? errors)
    {
        if ((isSuccess && errors != null) || (!isSuccess && errors == null))
            throw new ArgumentException("Invalid error", nameof(errors));
        IsSuccess = isSuccess;
        Errors = errors;
    }
    
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public IEnumerable<Error>? Errors { get; }

    public static Result Success()
    {
        return new Result(true, null);
    }

    public static Result Failure(IEnumerable<Error> errors)
    {
        return new Result(false, errors);
    }

    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new Result<TValue>(value, true, null);
    }

    public static Result<TValue> Failure<TValue>(IEnumerable<Error>? errors)
    {
        return new Result<TValue>(default, false, errors);
    }
}

public class Result<TValue>(TValue? value, bool isSuccess, IEnumerable<Error>? errors) : Result(isSuccess, errors)
{
    public TValue? Value => IsSuccess ? value! : throw new InvalidOperationException("The Value of a failure result can't be accessed");

    public static implicit operator Result<TValue>(TValue? value)
    {
        return value is not null ? Success(value) : Failure<TValue>(null);
    }
}

public static class ResultExtensions
{
    public static TValue Match<TValue>(this Result result, Func<TValue> onSuccess, Func<IEnumerable<Error>, TValue> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result.Errors!);
    }
}