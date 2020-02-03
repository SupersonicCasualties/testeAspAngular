using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetWebApi.Classes
{
    public class ResponseClass
    {
        public string Message { get; set; }
        public int Code { get; set; }
        public BaseClass Data { get; set; }
        public List<BaseClass> MultiData { get; set; }
    }
}