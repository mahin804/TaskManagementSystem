using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Shared
{
    public interface IApiResponse
    {
        ResultType ResultType { get; set; }
        string Message { get; set; }
    }
    public class FetchApiExeResult<T> : IApiResponse
    {
        public ResultType ResultType { get; set; }
        public string Message { get; set; }
        public ResultData<T> Result { get; set; }
    }

    public class ResultData<T>
    {
        public T Data { get; set; }
    }
    public enum ResultType
    {
        Success,
        PSQLError,
        ValidationException,
        NotFound,
        NoData,
        Error,
        UnAuthorized,
        Forbidden
    }
}
