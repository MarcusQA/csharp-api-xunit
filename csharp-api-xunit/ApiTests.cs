using Newtonsoft.Json;
using System.Net;
using static csharp_api_xunit.Helpers.HttpRequests;

namespace csharp_api_xunit
{
    public class ApiTests : BaseApiTests
    {
        [Fact]
        public async Task GetPost_ShouldReturnCorrectTitle()
        {
            var response = await MakeHttpRequestAsync(HttpMethod.Get, $"{_defaultBaseUri}/posts/1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string responseJson = await response.Content.ReadAsStringAsync();

            dynamic jsonDeserialised = JsonConvert.DeserializeObject<dynamic>(responseJson)!;
            var actualTitle = jsonDeserialised.title.ToString();

            Assert.Equal("sunt aut facere repellat provident occaecati excepturi optio reprehenderit", actualTitle);
        }

        [Fact]
        public async Task CreatePost_ShouldReturnCorrectTitle()
        {
            headers.Add("Content-Type", "application/json");

            var jsonBody = """{ "title": "foo", "body": "bar", "userId": 1 }""";

            var response = await MakeHttpRequestAsync(HttpMethod.Post, $"{_defaultBaseUri}/posts", headers, jsonBody);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            string responseJson = await response.Content.ReadAsStringAsync();

            dynamic jsonDeserialised = JsonConvert.DeserializeObject<dynamic>(responseJson)!;
            var actualTitle = jsonDeserialised.title.ToString();

            Assert.Equal("fooXXX", actualTitle);
        }

        [Fact]
        public async Task UpdatePost_ShouldReturnCorrectTitle()
        {
            headers.Add("Content-Type", "application/json");

            var jsonBody = """{ "title": "foo", "body": "bar", "userId": 1 }""";

            var response = await MakeHttpRequestAsync(HttpMethod.Put, $"{_defaultBaseUri}/posts/1", headers, jsonBody);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string responseJson = await response.Content.ReadAsStringAsync();

            dynamic jsonDeserialised = JsonConvert.DeserializeObject<dynamic>(responseJson)!;
            var actualTitle = jsonDeserialised.title.ToString();

            Assert.Equal("foo", actualTitle);
        }

        [Fact]
        public async Task PatchPost_ShouldReturnCorrectTitle()
        {
            headers.Add("Content-Type", "application/json");

            var jsonBody = """{ "title": "foo" }""";

            var response = await MakeHttpRequestAsync(HttpMethod.Patch, $"{_defaultBaseUri}/posts/1", headers, jsonBody);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string responseJson = await response.Content.ReadAsStringAsync();

            dynamic jsonDeserialised = JsonConvert.DeserializeObject<dynamic>(responseJson)!;
            var actualTitle = jsonDeserialised.title.ToString();

            Assert.Equal("foo", actualTitle);
        }

        [Fact]
        public async Task DeletePost_ShouldReturnCorrectTitle()
        {
            var response = await MakeHttpRequestAsync(HttpMethod.Delete, $"{_defaultBaseUri}/posts/1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string responseJson = await response.Content.ReadAsStringAsync();
            Assert.Equal("{}", responseJson);
        }
    }
}