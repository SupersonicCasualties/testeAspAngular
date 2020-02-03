using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Http;
using System.Web;
using AspNetWebApi.Classes;
using AspNetWebApi.Classes.Response;

namespace AspNetWebApi.Utils
{
    public static class Util
    {
        public static ResponseError ResponseError(HttpRequestMessage request, Exception e)
        {
            var strError = e.InnerException?.InnerException != null ? e.InnerException.InnerException.Message : e.InnerException?.Message;
            return ResponseError(request, strError);
        }

        public static ResponseError ResponseError(HttpRequestMessage request, DbEntityValidationException validation)
        {
            var errors = validation.EntityValidationErrors
                .SelectMany(error => error.ValidationErrors)
                .Aggregate("", (current, err) => current + $"Propriedade: {err.PropertyName}, Erro: {err.ErrorMessage} | ");

            return ResponseError(request, errors);
        }

        public static ResponseError ResponseError(HttpRequestMessage request, string message)
        {
            var error = new ResponseClass()
            {
                Code = 500,
                Message = message
            };
            return new ResponseError(error, request);
        }

        public static ResponseSuccess ResponseSuccess(HttpRequestMessage request, BaseClass cBaseClass, string message)
        {
            var successResponse = new ResponseClass()
            {
                Code = 200,
                Message = message,
                Data = cBaseClass
            };

            return new ResponseSuccess(successResponse, request);
        }

        public static ResponseSuccess ResponseSuccess(HttpRequestMessage request, List<BaseClass> baseClasses, string message)
        {
            var successResponse = new ResponseClass()
            {
                Code = 200,
                Message = message,
                MultiData = baseClasses
            };

            return new ResponseSuccess(successResponse, request);
        }

        public static ResponseSuccess ResponseSuccess(HttpRequestMessage request, string message)
        {
            var successResponse = new ResponseClass()
            {
                Code = 200,
                Message = message,
            };

            return new ResponseSuccess(successResponse, request);
        }
    }
}