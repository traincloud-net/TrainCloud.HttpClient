using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace TrainCloud.HttpClient;

public static class HttpClientExtensions
{
    private static JsonSerializerOptions TrainCloudJsonSerializerOptions { get; set; } = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

    private static async Task<TResponse?> ProcessResponseAsync<TResponse>(HttpResponseMessage response, Action<HttpStatusCode>? errorAction = null)
    {
        if (!response.IsSuccessStatusCode)
        {
            if (errorAction is not null)
            {
                errorAction(response.StatusCode);
            }
            return default;
        }

        MemoryStream contentStream = (MemoryStream)await response.Content.ReadAsStreamAsync();

        TResponse? model = await JsonSerializer.DeserializeAsync<TResponse>(contentStream, TrainCloudJsonSerializerOptions);

        return model;
    }

    public static async Task<TResponse?> GetRequestAsync<TResponse>(this System.Net.Http.HttpClient client, 
                                                                    string requestUri, 
                                                                    Action<HttpStatusCode>? errorAction = null)
    {
        try
        {
            using HttpResponseMessage response = await client.GetAsync(requestUri);

            TResponse? model = await ProcessResponseAsync<TResponse>(response, errorAction);

            return model;
        }
        catch
        {
            if (errorAction is not null)
            {
                errorAction(HttpStatusCode.InternalServerError);
            }

            return default;
        }
    }

    public static async Task<TResponse?> PostRequestAsync<TPost, TResponse>(this System.Net.Http.HttpClient client, 
                                                                            string requestUri, 
                                                                            TPost param, 
                                                                            Action<HttpStatusCode>? errorAction = null)
    {
        try
        {
            HttpContent parameterContent = JsonContent.Create(param, typeof(TPost));

            using HttpResponseMessage response = await client.PostAsync(requestUri, parameterContent);

            TResponse? model = await ProcessResponseAsync<TResponse>(response, errorAction);

            return model;
        }
        catch
        {
            if (errorAction is not null)
            {
                errorAction(HttpStatusCode.InternalServerError);
            }

            return default;
        }
    }

    public static async Task<TResponse?> PatchRequestAsync<TPatch, TResponse>(this System.Net.Http.HttpClient client, 
                                                                              string requestUri, 
                                                                              TPatch param, 
                                                                              Action<HttpStatusCode>? errorAction = null)
    {
        try
        {
            HttpContent parameterContent = JsonContent.Create(param, typeof(TPatch));

            using HttpResponseMessage response = await client.PatchAsync(requestUri, parameterContent);

            TResponse? model = await ProcessResponseAsync<TResponse>(response, errorAction);

            return model;
        }
        catch
        {
            if (errorAction is not null)
            {
                errorAction(HttpStatusCode.InternalServerError);
            }

            return default;
        }
    }

    public static async Task<bool> DeleteRequestAsync(this System.Net.Http.HttpClient client, 
                                                      string requestUri, 
                                                      Action<HttpStatusCode>? errorAction = null)
    {
        try
        {
            using HttpResponseMessage response = await client.DeleteAsync(requestUri);
            if (!response.IsSuccessStatusCode)
            {
                if (errorAction is not null)
                {
                    errorAction(response.StatusCode);
                }
                return false;
            }

            return true;
        }
        catch
        {
            if (errorAction is not null)
            {
                errorAction(HttpStatusCode.InternalServerError);
            }

            return default;
        }
    }
}