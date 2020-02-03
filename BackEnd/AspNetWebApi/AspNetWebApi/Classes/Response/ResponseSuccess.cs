using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AspNetWebApi.Classes.Response
{
    public class ResponseSuccess : IHttpActionResult
    {
        ResponseClass _success;
        HttpRequestMessage _request;

        public ResponseSuccess(ResponseClass success, HttpRequestMessage request)
        {
            _success = success;
            _request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new ObjectContent<ResponseClass>(_success, new JsonMediaTypeFormatter()),
                RequestMessage = _request
            };
            return Task.FromResult(response);
        }
    }
}