using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

    public class StatusResult
    {
        public string Message { get; set; }
        public StatusCode Code { get; set; }

        /// <summary>
        /// Creates an Error StatusResult object
        /// </summary>
        /// <param name="msg">The error msg</param>
        /// <returns>StatusResult</returns>
        public static StatusResult Error(string msg)
        {
            return Create(StatusCode.Error, String.IsNullOrEmpty(msg) ? "An Error has occured" : msg);
        }

        /// <summary>
        /// Creates an Error StatusResult object with the default text
        /// </summary>
        /// <param name="msg">The error msg</param>
        /// <returns>StatusResult</returns>
        public static StatusResult Error()
        {
            return Error(null);
        }

        /// <summary>
        /// Creates a Success StatusResult object with the default text
        /// </summary>
        /// <param name="msg">The error msg</param>
        /// <returns>StatusResult</returns>
        public static StatusResult Success()
        {
            return Success(null);
        }

        /// <summary>
        /// Creates a Success StatusResult object
        /// </summary>
        /// <param name="msg">The success msg</param>
        /// <returns>StatusResult</returns>
        public static StatusResult Success(string msg)
        {
            return Create(StatusCode.OK, String.IsNullOrEmpty(msg) ? "OK" : msg);
        }

        /// <summary>
        /// Factory method to create a StatusResult
        /// </summary>
        /// <param name="c">The status code</param>
        /// <param name="msg">The success msg</param>
        /// <returns>StatusResult</returns>
        public static StatusResult Create(StatusCode c, string msg)
        {
            return new StatusResult() { Code = c, Message = msg };
        }
    }
}