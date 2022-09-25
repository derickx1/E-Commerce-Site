using Microsoft.AspNetCore.Mvc.Testing;
using Project3.Model;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Project3.Test
{
    public class ControllerTests
    {
        private readonly string mainUrl = "https://localhost:7208";
        string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxNCIsIm5iZiI6MTY2MDU5Mzg4NCwiZXhwIjoxNjYwNjgwMjg0LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjE4LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcyMTgvIn0.MGyJNG5PNjpgBynj6nugXQumWE_mDsx4NX5sBdPvM7Q";
        
        [Fact]
        public async void RegisterUser_Success()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            string uri = mainUrl + "/login";
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, uri);

            msg.Headers.Authorization = new AuthenticationHeaderValue("Basic", "VGVzdENhc2UxOnBhc3NUZXN0MQ==");

            string json = "{\"name\":TestCase,\"address\":\"Some Address\"}";
            msg.Content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.SendAsync(msg);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void AddCustomer_Success()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            string uri = mainUrl + "/customer";
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, uri);
            msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            string json = "{\"name\":\"Dummy\",\"address\":\"wonderland\",\"username\":\"Dummy2\",\"password\":\"dummy2\"}";
            msg.Content = new StringContent(json, Encoding.UTF8, "application/json");

            string content = await msg.Content.ReadAsStringAsync();
            HttpResponseMessage response = await httpClient.SendAsync(msg);
            Assert.True(response.IsSuccessStatusCode);
        }


        [Fact]
        public async void GetCustomer_IdInt_Success()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            string uri = mainUrl + "/customer/1";
            var expected = "{\"id\":1,\"name\":\"Dummy\",\"shipping_address\":\"wonderland\"}";

            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, uri);
            msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await httpClient.SendAsync(msg);
            string actual = await response.Content.ReadAsStringAsync();

            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact]
        public async void ListOrders_Id1()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            string uri = mainUrl + "/orders/1";
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, uri);
            msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            string expected = "[]";

            HttpResponseMessage response = await httpClient.SendAsync(msg);
            string actual = await response.Content.ReadAsStringAsync();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void MakePurchase_ProductId1_CustomerId1()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            string uri = mainUrl + "/orders/1&1";
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, uri);
            msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await httpClient.SendAsync(msg);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void GetProductReviews_ItemId1_Success()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            string uri = mainUrl + "/review/item/1";
            string expected = "[]";

            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, uri);
            msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await httpClient.SendAsync(msg);
            string actual = await response.Content.ReadAsStringAsync();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void GetCustomerReviews_CustomerId1_Success()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            string uri = mainUrl + "/review/customer/1";
            string expected = "[]";

            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, uri);
            msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await httpClient.SendAsync(msg);
            string actual = await response.Content.ReadAsStringAsync();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void AddReview_Success()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            string uri = mainUrl + "/review/1/1";
            Review fakeReview = new Review(0, 1, 1, DateTime.Now, "Some Text", 0);
            string json = JsonSerializer.Serialize(fakeReview);

            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, uri);
            msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            msg.Content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.SendAsync(msg);

            Assert.True(response.IsSuccessStatusCode);

        }

        [Fact]
        public async void GetJewelryList_Success()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            string uri = mainUrl + "/store";
            string expected = "[]";

            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, uri);
            msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await httpClient.SendAsync(msg);
            string actual = await response.Content.ReadAsStringAsync();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void GetJewelryList_Unauthorized()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            string uri = mainUrl + "/store";

            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await httpClient.SendAsync(msg);

            Assert.True(response.StatusCode.Equals(HttpStatusCode.Unauthorized));
        }

        [Fact]
        public async void ListCustomerTransactions_CustomerId1_Success()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            string uri = mainUrl + "/transactions?CustomerID=1";
            string expected = "[]";

            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, uri);
            msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await httpClient.SendAsync(msg);
            string actual = await response.Content.ReadAsStringAsync();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void ListTransactions_Success()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            string uri = mainUrl + "/transactions";
            string expected = "[]";

            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, uri);
            msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await httpClient.SendAsync(msg);
            string actual = await response.Content.ReadAsStringAsync();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void AddTransaction_Success()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            string uri = mainUrl + "/transactions?CustomerID=1&OrderID=1&ItemID=1";

            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, uri);
            msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await httpClient.SendAsync(msg);

            Assert.True(response.IsSuccessStatusCode);
        }

    }
}