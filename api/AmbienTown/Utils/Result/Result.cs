using Newtonsoft.Json;
using System;

namespace TimeSheet.Api.Utils.Result
{
    public class Result
    {
        public Result(Result result) : this(result.Success, result.Type, result.Message)
        {

        }

        protected Result() { }

        public Result(bool success, OperationResultType operationResultType)
        {
            Success = success;
            Type = operationResultType;
        }

        public Result(bool success, OperationResultType operationResultType, string message) : this(success, operationResultType)
        {
            Message = message;
        }

        public Result(bool success, OperationResultType operationResultType, Exception exception) : this(success, operationResultType)
        {
            Message = exception.InnerException != null ? exception.InnerException.Message : exception.Message;
        }

        [JsonIgnore]
        public OperationResultType Type { get; protected set; }

        [JsonIgnore]
        public bool Success { get; protected set; }

        [JsonIgnore]
        public bool Failure => !Success;

        public string Message { get; protected set; }
    }

    public class Result<T> : Result
    {
        public Result(Result result) : base(result)
        {

        }

        public Result(bool success, OperationResultType operationResultType) : base(success, operationResultType)
        {
        }

        public Result(bool success, T value, OperationResultType operationResultType) : base(success, operationResultType)
        {
            Value = value;
        }

        public Result(bool success, OperationResultType operationResultType, string message) : base(success, operationResultType, message)
        {

        }

        public Result(bool success, OperationResultType operationResultType, Exception exception) : base(success, operationResultType, exception)
        {

        }

        public Result(OperationResultType operationResultType, Result operationResult)
        {
            Type = operationResultType;
            Success = operationResult.Success;
            Message = operationResult.Message;
        }

        public T Value { get; }
    }
}
