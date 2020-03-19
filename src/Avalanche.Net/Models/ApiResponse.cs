using System.Collections.Generic;

namespace Avalanche.Net.Models
{
    public class ApiResponseBag<T> : ApiResponseBase<Dictionary<string, T>>
    {

    }

    public class ApiResponse<T> : ApiResponseBase<T>
    {

    }
}