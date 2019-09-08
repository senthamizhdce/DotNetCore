using HttpClientFactorySample.GitHub;
using HttpClientFactorySample.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DotNetCore.Controllers
{
    //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-2.2
    //https://www.talkingdotnet.com/3-ways-to-use-httpclientfactory-in-asp-net-core-2-1/

    [Route("api/[controller]")]
    [ApiController]
    public class HTTPClientController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly GitHubService _gitHubService;
        private readonly UnreliableEndpointCallerService _unreliableEndpointCallerService;

        public IEnumerable<GitHubBranch> Branches { get; private set; }
        public IEnumerable<GitHubPullRequest> PullRequests { get; private set; }
        public IEnumerable<GitHubIssue> LatestIssues { get; private set; }

        public bool GetBranchesError { get; private set; }
        public bool GetPullRequestsError { get; private set; }
        public bool GetIssuesError { get; private set; }

        public HTTPClientController(IHttpClientFactory clientFactory, GitHubService gitHubService, UnreliableEndpointCallerService unreliableEndpointCallerService)
        {
            _clientFactory = clientFactory;
            _gitHubService = gitHubService;
            _unreliableEndpointCallerService = unreliableEndpointCallerService;
        }

        [HttpGet("BasicUsage")]
        public async Task<ActionResult> BasicUsage()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://api.github.com/repos/aspnet/AspNetCore.Docs/branches");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Branches = await response.Content
                    .ReadAsAsync<IEnumerable<GitHubBranch>>();
            }
            else
            {
                GetBranchesError = true;
                Branches = Array.Empty<GitHubBranch>();
            }
            return Ok(Branches);
        }

        [HttpGet("NamedClient")]
        public async Task<ActionResult> NamedClient()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "repos/aspnet/AspNetCore.Docs/pulls");

            var client = _clientFactory.CreateClient("github");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                PullRequests = await response.Content
                    .ReadAsAsync<IEnumerable<GitHubPullRequest>>();
            }
            else
            {
                GetPullRequestsError = true;
                PullRequests = Array.Empty<GitHubPullRequest>();
            }
            return Ok(PullRequests);
        }

        [HttpGet("TypedClient")]
        public async Task<ActionResult> TypedClient()
        {
            try
            {
                LatestIssues = await _gitHubService.GetAspNetDocsIssues();
            }
            catch (HttpRequestException)
            {
                GetIssuesError = true;
                LatestIssues = Array.Empty<GitHubIssue>();
            }
            return Ok(LatestIssues);
        }
        [Route("unreliable-consumer")]
        public async Task<IActionResult> UnreliableEndpointConsumer()
        {
            // Builds a URI to what we will imagine is an external endpoint that is unreliable. For this sample we are hosting our own unreliable endpoint to demonstrate!

            var url = Url.Action("UnreliableEndpoint", "HTTPClient");

            var uriBuilder = new UriBuilder
            {
                Scheme = HttpContext.Request.Scheme,
                Host = HttpContext.Request.Host.Host,
                Port = HttpContext.Request.Host.Port ?? 80,
                Path = url
            };

            // call the typed client that has been registered in ConfigureServices

            var status = await _unreliableEndpointCallerService.GetDataFromUnreliableEndpoint(uriBuilder.Uri.ToString());

            return Ok(status);
        }

        [Route("unreliable")]
        public IActionResult UnreliableEndpoint()
        {
            var second = DateTime.UtcNow.Second;

            // about 50% of the time this will fail
            return second % 2 != 0 ? Ok() : StatusCode((int)HttpStatusCode.ServiceUnavailable);
        }

    }
}