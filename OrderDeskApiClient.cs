using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class OrderDeskApiClient
{
    private readonly HttpClient _client;
    private const string BaseUrl = "https://app.orderdesk.me/api/v2/";

    public OrderDeskApiClient(string apiKey, string storeId)
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri(BaseUrl);
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _client.DefaultRequestHeaders.Add("ORDERDESK-API-KEY", apiKey);
        _client.DefaultRequestHeaders.Add("ORDERDESK-STORE-ID", storeId);
    }

    #region Connectivity

    public async Task<bool> ConnectivityTestAsync()
    {
        var response = await _client.GetAsync("authentication-test");
        return response.IsSuccessStatusCode;
    }

    #endregion

    #region Orders

    public async Task<string> GetSingleOrderAsync(string orderId)
    {
        var response = await _client.GetAsync($"orders/{orderId}");
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetAllOrdersAsync()
    {
        var response = await _client.GetAsync("orders");
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> CreateOrderAsync(string orderData)
    {
        var content = new StringContent(orderData, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("orders", content);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> UpdateOrderAsync(string orderId, string orderData)
    {
        var content = new StringContent(orderData, Encoding.UTF8, "application/json");
        var response = await _client.PutAsync($"orders/{orderId}", content);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> DeleteOrderAsync(string orderId)
    {
        var response = await _client.DeleteAsync($"orders/{orderId}");
        return await response.Content.ReadAsStringAsync();
    }

    #endregion

    #region Inventory Items

    public async Task<string> GetSingleInventoryItemAsync(string itemId)
    {
        var response = await _client.GetAsync($"inventory-items/{itemId}");
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> CreateInventoryItemAsync(string itemData)
    {
        var content = new StringContent(itemData, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("inventory-items", content);
        return await response.Content.ReadAsStringAsync();
    }

    #endregion

    ~OrderDeskApiClient()
    {
        _client.Dispose();
    }
}
