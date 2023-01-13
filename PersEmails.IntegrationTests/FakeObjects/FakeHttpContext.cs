using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace PersEmails.IntegrationTests.FakeObjects
{
    internal class FakeHttpContext : HttpContext
    {
        public FakeHttpContext()
        {
            _request = new FakeHttpRequest(this);
        }

        public override IFeatureCollection Features => throw new NotImplementedException();

        private readonly HttpRequest _request;
        public override HttpRequest Request => _request;

        public override HttpResponse Response => throw new NotImplementedException();

        public override ConnectionInfo Connection => throw new NotImplementedException();

        public override WebSocketManager WebSockets => throw new NotImplementedException();

        public override ClaimsPrincipal User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override IDictionary<object, object?> Items { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private IServiceProvider _requestServices = new FakeServiceProvider();
        public override IServiceProvider RequestServices
        {
            get => _requestServices;
            set => _requestServices = value;
        }

        public override CancellationToken RequestAborted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private string _traceIdentifier = Guid.NewGuid().ToString();
        public override string TraceIdentifier
        {
            get => _traceIdentifier;
            set => _traceIdentifier = value;
        }

        public override ISession Session { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Abort()
        {
            throw new NotImplementedException();
        }
    }
}
