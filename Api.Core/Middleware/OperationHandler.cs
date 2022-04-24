namespace Api.Core.Middleware
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class OperationHandler : DelegatingHandler
    {
        public const string XOperationIdHeader = "X-OPERATION-ID";
        private readonly IOperationScoped _operationScoped;

        public OperationHandler(IOperationScoped operationScoped) =>
            _operationScoped = operationScoped;

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add(XOperationIdHeader, _operationScoped.OperationId);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
