namespace StockAPI.Common.Result
{

    public enum ResultStatus
    {
        None,
        NotFound,
        Conflict,
        Validation
    }
    public class Result
    {
        public bool IsSuccess { get; }
        public ResultStatus ResultStatus { get; }
        public string? ErrorMessage { get; }

        protected Result(bool isSuccess, ResultStatus resultStatus, string? errorMessage)
        {
            IsSuccess = isSuccess;
            ResultStatus = resultStatus;
            ErrorMessage = errorMessage;
        }

        public static Result Success() => new(true, ResultStatus.None, null);

        public static Result Failure(ResultStatus resultStatus, string errorMessage) => new(false, resultStatus, errorMessage);
    }

    public class Result<T> : Result
    {
        public T? Data { get; }
        private Result(bool isSuccess, ResultStatus resultStatus, string? errorMessage, T? data)
            : base(isSuccess, resultStatus, errorMessage)
        {
            Data = data;
        }
        public static Result<T> Success(T data) => new(true, ResultStatus.None, null, data);
        public static new Result<T> Failure(ResultStatus resultStatus, string errorMessage) => new(false, resultStatus, errorMessage, default);
    }
}
