using System;
using System.Collections.Generic;
using System.Linq;

namespace BottleRocket.Models
{

    /// <summary>
    /// Enumeration used to represent all of the possible status code that the server will return the clients so the client can react appropriately
    /// </summary>
    public enum StatusCode
    {
        OK = 0,
        Error
    }

    /// <summary>
    /// StatusResult will be the container class the will return to the client. It's a Generic class thus we can set whatever result we want.
    /// If no particulare result is returned then leave Result as null
    /// </summary>
    public class StatusResult<T>
    {
        public string Message { get; set; }
        public StatusCode Code { get; set; }
        public T Result { get; set; }

        /// <summary>
        /// Creates an Error StatusResult object
        /// </summary>
        /// <param name="msg">The error msg</param>
        /// <returns>StatusResult</returns>
        public static StatusResult<T> Error(string msg)
        {
            return Create(StatusCode.Error, String.IsNullOrEmpty(msg) ? "An Error has occured" : msg);
        }

        /// <summary>
        /// Creates an Error StatusResult object with the default text
        /// </summary>
        /// <param name="msg">The error msg</param>
        /// <returns>StatusResult</returns>
        public static StatusResult<T> Error()
        {
            return Error(null);
        }

        /// <summary>
        /// Creates a Success StatusResult object with the default text
        /// </summary>
        /// <param name="msg">The error msg</param>
        /// <returns>StatusResult</returns>
        public static StatusResult<T> Success()
        {
            return Success(null);
        }

        /// <summary>
        /// Creates a Success StatusResult object
        /// </summary>
        /// <param name="msg">The success msg</param>
        /// <returns>StatusResult</returns>
        public static StatusResult<T> Success(string msg)
        {
            return Create(StatusCode.OK, String.IsNullOrEmpty(msg) ? "OK" : msg);
        }

        /// <summary>
        /// Creates a Success StatusResult object
        /// </summary>
        /// <param name="obj">The result object</param>
        /// <returns>StatusResult</returns>
        public static StatusResult<T> Success(T obj)
        {
            return Success(null, obj);
        }

        /// <summary>
        /// Creates a Success StatusResult object
        /// </summary>
        /// <param name="msg">The success msg</param>
        /// <param name="obj">The result object</param>
        /// <returns>StatusResult</returns>
        public static StatusResult<T> Success(string msg, T obj)
        {
            var sr = Create(StatusCode.OK, String.IsNullOrEmpty(msg) ? "OK" : msg);
            sr.Result = obj;
            return sr;
        }

        /// <summary>
        /// Factory method to create a StatusResult
        /// </summary>
        /// <param name="c">The status code</param>
        /// <param name="msg">The success msg</param>
        /// <returns>StatusResult</returns>
        public static StatusResult<T> Create(StatusCode c, string msg)
        {
            return new StatusResult<T>() { Code = c, Message = msg };
        }

    }
}