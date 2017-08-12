using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerManagement.Tests
{
    public class StubHttpMessageHandler : HttpMessageHandler
    {
        private readonly Dictionary<Uri, HttpResponseMessage> _responseMessages = new Dictionary<Uri, HttpResponseMessage>();

        public HttpRequestMessage LastHttpRequestMessage { get; private set; }

        private Exception _exceptionToThrow;

        public Dictionary<Uri, HttpResponseMessage> ResponseMessages
        {
            get { return _responseMessages; }
        }

        public HttpResponseMessage WithJson<T>(string uri, T content, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return WithJson(new Uri(uri, UriKind.RelativeOrAbsolute), content, statusCode);
        }

        public HttpResponseMessage WithJson<T>(Uri uri, T content, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var httpResponseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new ObjectContent<T>(content, new JsonMediaTypeFormatter()),
                StatusCode = statusCode
            };

            _responseMessages.Add(uri, httpResponseMessage);
            return httpResponseMessage;
        }

        public HttpResponseMessage WithStatusCode(string uri, HttpStatusCode statusCode)
        {
            return WithStatusCode(new Uri(uri, UriKind.RelativeOrAbsolute), statusCode);
        }

        public HttpResponseMessage WithStatusCode(Uri uri, HttpStatusCode statusCode)
        {
            var httpResponseMessage = new HttpResponseMessage(statusCode);
            _responseMessages.Add(uri, httpResponseMessage);

            return httpResponseMessage;
        }

        public void WithResponse(Uri uri, HttpResponseMessage response)
        {
            _responseMessages.Add(uri, response);
        }

        public void WithException(Exception ex)
        {
            _exceptionToThrow = ex;
        }

        public void WithException<T>() where T : Exception, new()
        {
            _exceptionToThrow = new T();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastHttpRequestMessage = request;

            if (_exceptionToThrow != null)
            {
                throw _exceptionToThrow;
            }

            if (!_responseMessages.ContainsKey(request.RequestUri))
            {
                throw new InvalidOperationException(
                    string.Format("Response set-up required with 'WithJson' for {0}", request.RequestUri));
            }

            var response = _responseMessages[request.RequestUri];

            response.RequestMessage = request;
            return Task.FromResult(response);
        }
    }
}