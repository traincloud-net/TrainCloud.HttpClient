using System.Net.Http.Json;
using System.Text.Json;

namespace TrainCloud.HttpClient;


public static class HttpClientExtensions
{
    private static JsonSerializerOptions TrainCloudJsonSerializerOptions { get; set; } = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

    public delegate void OnError(int statusCode);

    public static async Task<TResponse?> GetJsonAsync<TResponse>(this HttpClient client, string requestUri, OnError? onErrorCallback = null)
    {
        try
        {
            using HttpResponseMessage response = await client.GetAsync(requestUri);
            if (!response.IsSuccessStatusCode)
            {
                if (onErrorCallback is not null)
                {
                    onErrorCallback((int)response.StatusCode);
                }
                return default;
            }

            Stream contentStream = await response.Content.ReadAsStreamAsync();
            TResponse? model = await JsonSerializer.DeserializeAsync<TResponse>(contentStream, TrainCloudJsonSerializerOptions);

            return model;
        }
        catch (Exception ex)
        {
            return default;
        }
    }

    public static async Task<TResponse?> PostJsonAsync<TPost, TResponse>(this HttpClient client, string requestUri, TPost param, OnError? onErrorCallback = null)
    {
        try
        {
            HttpContent parameterContent = JsonContent.Create(param, typeof(TPost));

            using HttpResponseMessage response = await client.PostAsync(requestUri, parameterContent);
            if (!response.IsSuccessStatusCode)
            {
                if (onErrorCallback is not null)
                {
                    onErrorCallback((int)response.StatusCode);
                }
                return default;
            }

            Stream contentStream = await response.Content.ReadAsStreamAsync();
            TResponse? model = await JsonSerializer.DeserializeAsync<TResponse>(contentStream, TrainCloudJsonSerializerOptions);

            return model;
        }
        catch (Exception ex)
        {
            return default;
        }
    }

    public static async Task<TResponse?> PatchJsonAsync<TPatch, TResponse>(this HttpClient client, string requestUri, TPatch param, OnError? onErrorCallback = null)
    {
        try
        {
            HttpContent parameterContent = JsonContent.Create(param, typeof(TPatch));

            using HttpResponseMessage response = await client.PatchAsync(requestUri, parameterContent);
            if (!response.IsSuccessStatusCode)
            {
                if (onErrorCallback is not null)
                {
                    onErrorCallback((int)response.StatusCode);
                }
                return default;
            }

            Stream contentStream = await response.Content.ReadAsStreamAsync();
            TResponse? model = await JsonSerializer.DeserializeAsync<TResponse>(contentStream, TrainCloudJsonSerializerOptions);

            return model;
        }
        catch (Exception ex)
        {
            return default;
        }
    }

    public static async Task<bool> DeleteAsyncX(this HttpClient client, string requestUri, OnError? onErrorCallback = null)
    {
        try
        {
            using HttpResponseMessage response = await client.DeleteAsync(requestUri);
            if (!response.IsSuccessStatusCode)
            {
                if (onErrorCallback is not null)
                {
                    onErrorCallback((int)response.StatusCode);
                }
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}