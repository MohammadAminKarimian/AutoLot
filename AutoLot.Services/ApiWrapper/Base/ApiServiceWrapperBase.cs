﻿namespace AutoLot.Services.ApiWrapper.Base;

public abstract class ApiServiceWrapperBase<TEntity> : IApiServiceWrapperBase<TEntity> where TEntity : BaseEntity, new()
{
    protected readonly HttpClient Client;
    protected readonly ApiServiceSettings ApiSettings;
    protected readonly string ApiVersion;
    private readonly string _endPoint;

    protected ApiServiceWrapperBase(HttpClient client, IOptionsMonitor<ApiServiceSettings> apiSettingsMonitor, string endPoint)
    {
        Client = client;
        ApiSettings = apiSettingsMonitor.CurrentValue;
        _endPoint = endPoint;
        ApiVersion = ApiSettings.ApiVersion;
        Client.BaseAddress = new Uri(ApiSettings.Uri);
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        string authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(
        $"{apiSettingsMonitor.CurrentValue.UserName}:{apiSettingsMonitor.CurrentValue.Password}"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Base", authToken);
    }

    internal async Task<HttpResponseMessage> PostAsJsonAsync(string uri, string json)
    {
        // Link to a list of all MediaTypes (MIME Types: Multipurpose Internet Mail Extensions)
        // https://www.sitepoint.com/mime-types-complete-list/
        return await Client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
    }

    internal async Task<HttpResponseMessage> PutAsJsonAsync(string uri, string json)
    {
        return await Client.PutAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
    }

    internal async Task<HttpResponseMessage> DeleteAsJsonAsync(string uri, string json)
    {
        HttpRequestMessage request = new HttpRequestMessage()
        {
            RequestUri = new Uri(uri),
            Method = HttpMethod.Delete,
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        return await Client.SendAsync(request);
    }

    public async Task<IList<TEntity>> GetAllEntitiesAsync()
    {
        var response = await Client.GetAsync($"{ApiSettings.Uri}{_endPoint}?v={ApiVersion}");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<IList<TEntity>>();
        //var result = await JsonSerializer.DeserializeAsync<IList<TEntity>>(await response.Content.ReadAsStreamAsync());
        return result;
    }

    public async Task<TEntity> GetEntityAsync(int id)
    {
        var response = await Client.GetAsync($"{ApiSettings.Uri}{_endPoint}/{id}?v={ApiVersion}");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<TEntity>();
        return result;
    }

    public async Task<TEntity> AddEntityAsync(TEntity entity)
    {
        var response = await PostAsJsonAsync($"{ApiSettings.Uri}{_endPoint}?v={ApiVersion}",
        JsonSerializer.Serialize(entity));
        if (response == null)
        {
            throw new Exception("Unable to communicate with the service");
        }
        var location = response.Headers?.Location?.OriginalString;
        return await response.Content.ReadFromJsonAsync<TEntity>() ?? await GetEntityAsync(entity.Id);
    }

    public async Task<TEntity> UpdateEntityAsync(TEntity entity)
    {
        var response = await PutAsJsonAsync($"{ApiSettings.Uri}{_endPoint}/{entity.Id}?v={ApiVersion}",
            JsonSerializer.Serialize(entity));
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TEntity>() ?? await GetEntityAsync(entity.Id);
    }

    public async Task DeleteEntityAsync(TEntity entity)
    {
        var response = await DeleteAsJsonAsync($"{ApiSettings.Uri}{_endPoint}/{entity.Id}?v={ApiVersion}",
            JsonSerializer.Serialize(entity));
        response.EnsureSuccessStatusCode();
    }
}
