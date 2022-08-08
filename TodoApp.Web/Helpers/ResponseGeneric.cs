using TodoApp.Core.DBEntities;
using System;
using System.Net;

namespace TodoApp.Web.Helpers
{
    public class ResponseGeneric : IResponseGeneric
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public dynamic Result { get; set; }

        public ResponseGeneric Success(string message = "Successfull", dynamic result = null, Log log = null)
        {
            return new ResponseGeneric()
            {
                Message = message,
                Result = result,
                Code = log != null ? log.Id.ToString() : Convert.ToInt16(HttpStatusCode.OK).ToString()
            };

        }

        public ResponseGeneric Error(string message = "Something went wrong", dynamic result = null, Log log = null)
        {
            return new ResponseGeneric()
            {
                Message = message,
                Result = result,
                Code = log != null ? log.Id.ToString() : Convert.ToInt16(HttpStatusCode.BadRequest).ToString()
            };

        }



    }
}
