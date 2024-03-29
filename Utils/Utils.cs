﻿namespace lms_b.Utils;

public interface IValidator<T, E>
{
    Result<T, E>  Validate();
}

public class Result<T, E> 
{
    public T? Value { get; private set; }
    public E? Error { get; private set; }
    public bool IsOk { get; private set; }
    public bool IsErr => !IsOk;

    private Result()
    {
        IsOk = true;
    }

    private Result(T value)
    {
        Value = value;
        IsOk = true;
    }

    private Result(E error)
    {
        Error = error;
        IsOk = false;
    }

    public static Result<T, E> Ok(T value)
    {
        return new Result<T, E>(value);
    }

    public static Result<T, E> Ok()
    {
        return new Result<T, E>();
    }

    public static Result<T, E> Err(E error)
    {
        return new Result<T, E>(error);
    }
}

public class Utils
{

}
