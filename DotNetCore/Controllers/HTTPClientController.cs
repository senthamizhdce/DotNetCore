using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        public IEnumerable<GitHubPullRequest> PullRequests { get; private set; }

        public bool GetPullRequestsError { get; private set; }

        public bool HasPullRequests => PullRequests.Any();

        public HTTPClientController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<GitHubPullRequest>> OnGet()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "repos/aspnet/AspNetCore.Docs/pulls");

            var client = _clientFactory.CreateClient("github");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                PullRequests = await response.Content.ReadAsAsync<IEnumerable<GitHubPullRequest>>();
            }
            else
            {
                GetPullRequestsError = true;
                PullRequests = Array.Empty<GitHubPullRequest>();
            }

            return PullRequests;
        }
    }
}