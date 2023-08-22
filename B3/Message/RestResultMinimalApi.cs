using ErrorOr;
namespace B3.Message;
public class RestResultMinimalApi
    {
        public static IResult CreateApiResponse(Exception ex)
        {
            var r = new RestResult<Exception>
            {
                IsSuccess = false,
                Errors = ex.Message.Split(";").ToList(),
                Data = ex
            };
            return Results.Json(r, null, null, 500);
        }

        public class RestResult<T>
        {
            public bool IsSuccess { get; set; }
            public List<string> Errors { get; set; } = new();
            public T? Data { get; set; }
        }

        public struct RestResult
        {
            public bool IsSuccess { get; set; }
            public List<string> Error { get; set; }
        }
        public static IResult CreateApiResponse<T>(ErrorOr<T> result)
        {
            return result.IsError ? CreateErrorApiResponse(result) : CreateSucessApiResponse(result);
        }

        private static IResult CreateSucessApiResponse<T>(ErrorOr<T> result)
        {
            var r = new RestResult<T>
            {
                IsSuccess = true,
                Data = !result.IsError ? result.Value : default
            };
            return Results.Json(r, null, null, StatusCodes.Status200OK);
        }

        private static IResult CreateErrorApiResponse<T>(ErrorOr<T> result)
        {
            var r = new RestResult<T>
            {
                IsSuccess = false,
                Errors = result.Errors.Select(x => x.Description).ToList()
            };

            if (result.Errors.Any(x => x.Type == ErrorType.Unexpected))
                return Results.Json(r, null, null, StatusCodes.Status500InternalServerError);
            if (result.Errors.Any(x => x.Type == ErrorType.Failure))
                return Results.Json(r, null, null, StatusCodes.Status500InternalServerError);
            if (result.Errors.Any(x => x.Type == ErrorType.Conflict))
                return Results.Json(r, null, null, StatusCodes.Status409Conflict);

            return Results.Json(r, null, null,
                result.Errors.Any(x => x.Type == ErrorType.NotFound)
                    ? StatusCodes.Status404NotFound
                    : StatusCodes.Status400BadRequest);
        }
    }