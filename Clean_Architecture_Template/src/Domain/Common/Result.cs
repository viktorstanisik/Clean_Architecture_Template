using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class Result<T>
    {
        public T? Data { get; private set; }
        public bool Success { get; private set; }
        public string? ErrorMessage { get; private set; }

        private Result(T? data, bool success, string? errorMessage)
        {
            Data = data;
            Success = success;
            ErrorMessage = errorMessage;
        }

        public static Result<T> CreateSuccess(T data) =>
            new Result<T>(data, true, null);
        
        public static Result<T> CreateFailure(string errorMessage) =>
                     new Result<T>(default, false, errorMessage);        
    }

}
