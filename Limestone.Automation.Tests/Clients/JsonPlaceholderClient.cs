using Limestone.Automation.Tests.Models;
using RestSharp;

namespace Limestone.Automation.Tests.Clients;

public class JsonPlaceholderClient : IDisposable
{
    private readonly RestClient _client;

    public JsonPlaceholderClient(string baseUrl)
    {
        _client = new RestClient(baseUrl);
    }

    public Task<RestResponse<JsonPlaceholderUser>> GetUserAsync(int userId)
    {
        var request = new RestRequest($"users/{userId}");
        return _client.ExecuteGetAsync<JsonPlaceholderUser>(request);
    }

    public Task<RestResponse<JsonPlaceholderPost>> GetPostAsync(int postId)
    {
        var request = new RestRequest($"posts/{postId}");
        return _client.ExecuteGetAsync<JsonPlaceholderPost>(request);
    }

    public void Dispose()
    {
        _client.Dispose();
        GC.SuppressFinalize(this);
    }
}
