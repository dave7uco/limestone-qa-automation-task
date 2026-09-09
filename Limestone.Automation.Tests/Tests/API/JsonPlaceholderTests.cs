using System.Net;
using Limestone.Automation.Tests.Clients;
using Limestone.Automation.Tests.Configuration;

namespace Limestone.Automation.Tests.Tests.API;

[TestFixture]
public class JsonPlaceholderTests
{
    [Test]
    public async Task GetUserReturnsExpectedUser()
    {
        using var client = new JsonPlaceholderClient(TestConfiguration.JsonPlaceholderBaseUrl);

        var response = await client.GetUserAsync(1);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Data, Is.Not.Null);
            Assert.That(response.Data!.Id, Is.EqualTo(1));
            Assert.That(response.Data.Name, Is.Not.Null.And.Not.Empty);
            Assert.That(response.Data.Username, Is.Not.Null.And.Not.Empty);
            Assert.That(response.Data.Email, Is.Not.Null.And.Not.Empty);
        });
    }

    [Test]
    public async Task GetPostReturnsExpectedPost()
    {
        using var client = new JsonPlaceholderClient(TestConfiguration.JsonPlaceholderBaseUrl);

        var response = await client.GetPostAsync(1);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Data, Is.Not.Null);
            Assert.That(response.Data!.Id, Is.EqualTo(1));
            Assert.That(response.Data.UserId, Is.GreaterThan(0));
            Assert.That(response.Data.Title, Is.Not.Null.And.Not.Empty);
            Assert.That(response.Data.Body, Is.Not.Null.And.Not.Empty);
        });
    }
}
