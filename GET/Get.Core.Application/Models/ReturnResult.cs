using System;
using System.Collections.Generic;
using System.Text;

namespace GET.Core.Application.Models
{
    public enum StatusCode
    {
        Status200OK = 200,
        Status400BadRequest = 400,
        Status401Unauthorized = 401,
        Status404NotFound = 404,
        Status500InternalServerError = 500,
        Status503ServiceUnavailable = 503
    }

    public class ReturnResult<T>
    {
        #region Constructors
        public ReturnResult()
        {
        }

        public ReturnResult(bool isSuccess, StatusCode httpCode, T data, List<string> errorList) : this()
        {
            this.IsSuccess = isSuccess;
            this.HttpCode = httpCode;
            this.Data = data;
            this.ErrorList = errorList;
        }

        #endregion
        public bool IsSuccess { get; set; }
        public StatusCode HttpCode { get; set; }
        public T Data { get; set; }
        public List<string> ErrorList { get; set; } = new List<string>();

        /// <summary>
        /// Set success result with data
        /// </summary>
        /// <param name="data"></param>
        public void Success(T data)
        {
            IsSuccess = true;
            HttpCode = StatusCode.Status200OK;
            Data = data;
        }

        /// <summary>
        /// Set 500 Internal Server Error result
        /// </summary>
        /// <param name="errorList"></param>
        /// <param name="data"></param>
        public void ServerError(List<string> errorList, T data)
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status500InternalServerError;
            ErrorList = errorList;
            Data = data;
        }

        /// <summary>
        /// Set 500 Internal Server Error result
        /// </summary>
        /// <param name="errorList"></param>
        /// <param name="data"></param>
        public void ServerError(string errorMessage, T data)
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status500InternalServerError;
            ErrorList = new List<string>() { errorMessage };
            Data = data;
        }

        /// <summary>
        /// Set 500 Internal Server Error result
        /// </summary>
        /// <param name="errorList"></param>
        public void ServerError(List<string> errorList)
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status500InternalServerError;
            ErrorList = errorList;
        }

        /// <summary>
        /// Set 500 Internal Server Error result
        /// </summary>
        /// <param name="errorMessage"></param>
        public void ServerError(string errorMessage)
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status500InternalServerError;
            ErrorList = new List<string>() { errorMessage };
        }

        /// <summary>
        /// Set 500 Internal Server Error result with default error message
        /// </summary>
        public void DefaultServerError()
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status500InternalServerError;
            ErrorList = new List<string>() { "UnExpected Error during Processing Your Request.Please Contact System Administrator." };
        }

        /// <summary>
        /// Set 404 Not Found result
        /// </summary>
        /// <param name="errorList"></param>
        /// <param name="data"></param>
        public void NotFound(List<string> errorList, T data)
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status404NotFound;
            ErrorList = errorList;
            Data = data;
        }

        /// <summary>
        /// Set 404 Not Found result
        /// </summary>
        /// <param name="errorList"></param>
        public void NotFound(List<string> errorList)
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status404NotFound;
            ErrorList = errorList;
        }

        /// <summary>
        /// Set 404 Not Found result
        /// </summary>
        /// <param name="errorMessage"></param>
        public void NotFound(string errorMessage)
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status404NotFound;
            ErrorList = new List<string>() { errorMessage };
        }

        /// <summary>
        /// Set 404 Not Found result
        /// </summary>
        /// <param name="data"></param>
        /// <param name="errorMessage"></param>
        public void NotFound(T data, string errorMessage)
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status404NotFound;
            ErrorList = new List<string>() { errorMessage };
            Data = data;
        }

        /// <summary>
        /// Set 404 Not Found result with default message
        /// </summary>
        public void DefaultNotFound()
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status404NotFound;
            ErrorList = new List<string>() { "Required Data Not Found" };
        }

        /// <summary>
        /// Set 400 Bad Request result
        /// </summary>
        /// <param name="errorList"></param>
        /// <param name="data"></param>
        public void BadRequest(List<string> errorList, T data)
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status400BadRequest;
            ErrorList = errorList;
            Data = data;
        }

        /// <summary>
        /// Set 400 Bad Request result
        /// </summary>
        /// <param name="errorList"></param>
        public void BadRequest(List<string> errorList)
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status400BadRequest;
            ErrorList = errorList;
        }

        /// <summary>
        /// Set 400 Bad Request result
        /// </summary>
        /// <param name="errorMessage"></param>
        public void BadRequest(string errorMessage)
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status400BadRequest;
            ErrorList = new List<string>() { errorMessage };
        }

        /// <summary>
        /// Set 400 Bad Request result
        /// </summary>
        /// <param name="data"></param>
        /// <param name="errorMessage"></param>
        public void BadRequest(T data, string errorMessage)
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status400BadRequest;
            ErrorList = new List<string>() { errorMessage };
            Data = data;
        }
        /// <summary>
        /// Set 400 Bad Request result
        /// </summary>
        /// <param name="data"></param>
        /// <param name="errorMessage"></param>
        public void BadRequest(T data, List<string> errorList)
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status400BadRequest;
            ErrorList = errorList;
            Data = data;
        }

        /// <summary>
        /// Set 401 Unauthorized result
        /// </summary>
        /// <param name="errorList"></param>
        /// <param name="data"></param>
        public void Unauthorized(List<string> errorList, T data)
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status401Unauthorized;
            ErrorList = errorList;
            Data = data;
        }

        /// <summary>
        /// Set 401 Unauthorized result
        /// </summary>
        /// <param name="errorList"></param>
        public void Unauthorized(List<string> errorList)
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status401Unauthorized;
            ErrorList = errorList;
        }

        /// <summary>
        /// Set 401 Unauthorized result
        /// </summary>
        /// <param name="errorMessage"></param>
        public void Unauthorized(string errorMessage)
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status401Unauthorized;
            ErrorList = new List<string>() { errorMessage };
        }

        /// <summary>
        /// Set 503 Service Unavailable
        /// </summary>
        /// <param name="errorList"></param>
        /// <param name="data"></param>
        public void ServiceUnavailable(List<string> errorList, T data)
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status503ServiceUnavailable;
            ErrorList = errorList;
            Data = data;
        }

        /// <summary>
        /// Set 503 Service Unavailable
        /// </summary>
        /// <param name="errorList"></param>
        /// <param name="data"></param>
        public void ServiceUnavailable(List<string> errorList)
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status503ServiceUnavailable;
            ErrorList = errorList;
        }

        /// <summary>
        /// Set 503 Service Unavailable
        /// </summary>
        /// <param name="errorMessage"></param>
        public void ServiceUnavailable(string errorMessage)
        {
            IsSuccess = false;
            HttpCode = StatusCode.Status503ServiceUnavailable;
            ErrorList = new List<string>() { errorMessage };
        }
    }
}
