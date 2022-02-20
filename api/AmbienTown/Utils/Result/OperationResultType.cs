namespace TimeSheet.Api.Utils.Result
{
    public enum OperationResultType
    {
        Ok,
        Created,
        NoContent,
        ValidationError,
        NotFound,
        Conflict,
        Error,
        Forbidden,
        Unauthorized
    }
}