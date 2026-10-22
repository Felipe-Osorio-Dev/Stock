using Microsoft.AspNetCore.Mvc;
using StockAPI.Dtos.Product;

namespace StockAPI.Common.Result
{
    public static class ResultExtensions
    {
        public static ObjectResult ToErrorResult(this ControllerBase controller, Result result)
        {
            var status = result.ResultStatus switch
            {
                ResultStatus.Validation => StatusCodes.Status400BadRequest,
                ResultStatus.NotFound => StatusCodes.Status404NotFound,
                ResultStatus.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return controller.StatusCode(status, new ApiErrorResponseDto(result.ErrorMessage ?? "Erro Inesperado!", status));
        }
    }
}
