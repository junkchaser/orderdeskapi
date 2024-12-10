using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class OrderDeskApiClient
{
    private readonly string _baseUrl = "https://app.orderdesk.me/api/v2";
    private readonly HttpClient _httpClient;

    public OrderDeskApiClient(string storeId, string apiKey)
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("ORDERDESK-STORE-ID", storeId);
        _httpClient.DefaultRequestHeaders.Add("ORDERDESK-API-KEY", apiKey);
        _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
    }

    #region **Order Management**

    // **GetSingleOrder**
    public async Task<string> GetSingleOrderAsync(string orderId)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/orders/{orderId}");
        return await response.Content.ReadAsStringAsync();
    }

    // **GetAllOrders**
    public async Task<string> GetAllOrdersAsync()
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/orders");
        return await response.Content.ReadAsStringAsync();
    }

    // **CreateOrder**
    public async Task<string> CreateOrderAsync(Dictionary<string, object> orderData)
    {
        var jsonData = JsonConvert.SerializeObject(orderData);
        var response = await _httpClient.PostAsync($"{_baseUrl}/orders", new StringContent(jsonData, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    // **UpdateOrder**
    public async Task<string> UpdateOrderAsync(string orderId, Dictionary<string, object> orderData)
    {
        var jsonData = JsonConvert.SerializeObject(orderData);
        var response = await _httpClient.PutAsync($"{_baseUrl}/orders/{orderId}", new StringContent(jsonData, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    // **DeleteOrder**
    public async Task<string> DeleteOrderAsync(string orderId)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}/orders/{orderId}");
        return await response.Content.ReadAsStringAsync();
    }

    #endregion

    #region **Connectivity**

    // **ConnectivityTest**
    public async Task<string> ConnectivityTestAsync()
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/test");
        return await response.Content.ReadAsStringAsync();
    }

    // **SubmitOrder** (Implemented via CreateOrder for example)
    public async Task<string> SubmitOrderAsync(Dictionary<string, object> orderData)
    {
        return await CreateOrderAsync(orderData);
    }

    // **CheckOrderStatus** (Implemented via GetSingleOrder for example)
    public async Task<string> CheckOrderStatusAsync(string orderId)
    {
        return await GetSingleOrderAsync(orderId);
    }

    // **CancelOrder** (Implemented via DeleteOrder for example)
    public async Task<string> CancelOrderAsync(string orderId)
    {
        return await DeleteOrderAsync(orderId);
    }

    #endregion

    #region **Inventory Management**

    // **GetSingleInventoryItem**
    public async Task<string> GetSingleInventoryItemAsync(string itemId)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/inventory-items/{itemId}");
        return await response.Content.ReadAsStringAsync();
    }

    // **CreateInventoryItem**
    public async Task<string> CreateInventoryItemAsync(Dictionary<string, object> itemData)
    {
        var jsonData = JsonConvert.SerializeObject(itemData);
        var response = await _httpClient.PostAsync($"{_baseUrl}/inventory-items", new StringContent(jsonData, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }

    #endregion
}
