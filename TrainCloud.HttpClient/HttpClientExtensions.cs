using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace TrainCloud.HttpClient;

public static class HttpClientExtensions
{
    private static JsonSerializerOptions TrainCloudJsonSerializerOptions { get; set; } = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

    public delegate void OnError(int statusCode);

    public static void AuthorizeClient(this System.Net.Http.HttpClient client, string token)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    private static async Task<TResponse?> ProcessResponseAsync<TResponse>(HttpResponseMessage response, OnError? onErrorCallback = null)
    {
        if (!response.IsSuccessStatusCode)
        {
            if (onErrorCallback is not null)
            {
                onErrorCallback((int)response.StatusCode);
            }
            return default;
        }

        MemoryStream contentStream = (MemoryStream)await response.Content.ReadAsStreamAsync();

        TResponse? model = await JsonSerializer.DeserializeAsync<TResponse>(contentStream, TrainCloudJsonSerializerOptions);

        return model;
    }

    public static async Task<TResponse?> GetRequestAsync<TResponse>(this System.Net.Http.HttpClient client, string requestUri, OnError? onErrorCallback = null)
    {
        try
        {
            using HttpResponseMessage response = await client.GetAsync(requestUri);

            TResponse? model = await ProcessResponseAsync<TResponse>(response, onErrorCallback);

            return model;
        }
        catch
        {
            if (onErrorCallback is not null)
            {
                onErrorCallback(500);
            }

            return default;
        }
    }

    public static async Task<TResponse?> PostRequestAsync<TPost, TResponse>(this System.Net.Http.HttpClient client, string requestUri, TPost param, OnError? onErrorCallback = null)
    {
        try
        {
            HttpContent parameterContent = JsonContent.Create(param, typeof(TPost));

            using HttpResponseMessage response = await client.PostAsync(requestUri, parameterContent);

            TResponse? model = await ProcessResponseAsync<TResponse>(response, onErrorCallback);

            return model;
        }
        catch
        {
            if (onErrorCallback is not null)
            {
                onErrorCallback(500);
            }

            return default;
        }
    }

    public static async Task<TResponse?> PatchRequestAsync<TPatch, TResponse>(this System.Net.Http.HttpClient client, string requestUri, TPatch param, OnError? onErrorCallback = null)
    {
        try
        {
            HttpContent parameterContent = JsonContent.Create(param, typeof(TPatch));

            using HttpResponseMessage response = await client.PatchAsync(requestUri, parameterContent);

            TResponse? model = await ProcessResponseAsync<TResponse>(response, onErrorCallback);

            return model;
        }
        catch
        {
            if (onErrorCallback is not null)
            {
                onErrorCallback(500);
            }

            return default;
        }
    }

    public static async Task<bool> DeleteRequestAsync(this System.Net.Http.HttpClient client, string requestUri, OnError? onErrorCallback = null)
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
        catch
        {
            if (onErrorCallback is not null)
            {
                onErrorCallback(500);
            }

            return default;
        }
    }
}