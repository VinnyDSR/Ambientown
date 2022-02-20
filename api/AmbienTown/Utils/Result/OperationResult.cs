using System;

namespace TimeSheet.Api.Utils.Result
{
    public class OperationResult
    {
        public static Result Conflict() => new Result(false, OperationResultType.Conflict);
        public static Result Conflict(string message) => new Result(false, OperationResultType.Conflict, message);
        public static Result<T> Conflict<T>(string message) => new Result<T>(false, OperationResultType.Conflict, message);
        public static Result<T> Conflict<T>(Result operationResult) => new Result<T>(OperationResultType.Conflict, operationResult);

        public static Result Created() => new Result(true, OperationResultType.Created);
        public static Result<T> Created<T>(T value) => new Result<T>(true, value, OperationResultType.Created);

        public static Result Error() => new Result(false, OperationResultType.Error);
        public static Result Error(string message) => new Result(false, OperationResultType.Error, message);
        public static Result Error(System.Exception exception)
            => new Result(false, OperationResultType.Error, exception);
        public static Result<T> Error<T>(string message) => new Result<T>(false, OperationResultType.Error, message);
        public static Result<T> Error<T>(System.Exception exception) => new Result<T>(false, OperationResultType.Error, exception);
        public static Result<T> Error<T>(Result operationResult) => new Result<T>(OperationResultType.Error, operationResult);

        public static Result Forbidden() => new Result(false, OperationResultType.Forbidden);
        public static Result<T> Forbidden<T>() => new Result<T>(false, OperationResultType.Forbidden);
        public static Result Forbidden(string message) => new Result(false, OperationResultType.Forbidden, message);
        public static Result<T> Forbidden<T>(string message) => new Result<T>(false, OperationResultType.Forbidden, message);

        public static Result NoContent() => new Result(true, OperationResultType.NoContent);
        public static Result<T> NoContent<T>() => new Result<T>(true, OperationResultType.NoContent);

        public static Result NotFound() => new Result(false, OperationResultType.NotFound);
        public static Result NotFound(string message) => new Result(false, OperationResultType.NotFound, message);
        public static Result<T> NotFound<T>() => new Result<T>(false, OperationResultType.NotFound);
        public static Result<T> NotFound<T>(string message) => new Result<T>(false, OperationResultType.NotFound, message);
        public static Result<T> NotFound<T>(Result operationResult) => new Result<T>(OperationResultType.NotFound, operationResult);

        public static Result OK() => new Result(true, OperationResultType.Ok);
        public static Result<T> OK<T>() => new Result<T>(true, OperationResultType.Ok);
        public static Result<T> OK<T>(T value) => new Result<T>(true, value, OperationResultType.Ok);

        public static Result ValidationError() => new Result(false, OperationResultType.ValidationError);
        public static Result ValidationError(string message) => new Result(false, OperationResultType.ValidationError, message);
        public static Result<T> ValidationError<T>(string message) => new Result<T>(false, OperationResultType.ValidationError, message);
        public static Result<T> ValidationError<T>(Result operationResult) => new Result<T>(OperationResultType.ValidationError, operationResult);

        public static Result Unauthorized() => new Result(false, OperationResultType.Unauthorized);
        public static Result<T> Unauthorized<T>() => new Result<T>(false, OperationResultType.Unauthorized);
        public static Result Unauthorized(string message) => new Result(false, OperationResultType.Unauthorized, message);
        public static Result<T> Unauthorized<T>(string message) => new Result<T>(false, OperationResultType.Unauthorized, message);
    }
}
