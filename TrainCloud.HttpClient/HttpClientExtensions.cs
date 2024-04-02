using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace TrainCloud.HttpClient;

public static class HttpClientExtensions
{
    private static JsonSerializerOptions TrainCloudJsonSerializerOptions { get; set; } = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

    private static async Task<TResponse?> ProcessResponseAsync<TResponse>(HttpResponseMessage response, Action<HttpStatusCode>? httpStatusAction = null)
    {
        if (!response.IsSuccessStatusCode)
        {
            if (httpStatusAction is not null)
            {
                httpStatusAction(response.StatusCode);
            }
            return default;
        }

        Stream contentStream = await response.Content.ReadAsStreamAsync();

        TResponse? model = await JsonSerializer.DeserializeAsync<TResponse>(contentStream, TrainCloudJsonSerializerOptions);

        return model;
    }

    public static async Task<TResponse?> GetRequestAsync<TResponse>(this System.Net.Http.HttpClient client, 
                                                                    string requestUri, 
                                                                    Action<HttpStatusCode>? httpStatusAction = null,
                                                                    Action<Exception>? exceptionAction = null)
    {
        try
        {
            using HttpResponseMessage response = await client.GetAsync(requestUri);

            if (httpStatusAction is not null)
            {
                httpStatusAction(response.StatusCode);
            }

            TResponse? model = await ProcessResponseAsync<TResponse>(response, httpStatusAction);

            return model;
        }
        catch (Exception ex)
        {
            if (exceptionAction is not null)
            {
                exceptionAction(ex);
            }

            return default;
        }
    }

    public static async Task<TResponse?> PostRequestAsync<TPost, TResponse>(this System.Net.Http.HttpClient client, 
                                                                            string requestUri, 
                                                                            TPost param, 
                                                                            Action<HttpStatusCode>? httpStatusAction = null,
                                                                            Action<Exception>? exceptionAction = null)
    {
        try
        {
            HttpContent parameterContent = JsonContent.Create(param, typeof(TPost));

            using HttpResponseMessage response = await client.PostAsync(requestUri, parameterContent);

            if (httpStatusAction is not null)
            {
                httpStatusAction(response.StatusCode);
            }

            TResponse? model = await ProcessResponseAsync<TResponse>(response, httpStatusAction);

            return model;
        }
        catch (Exception ex)
        {
            if (exceptionAction is not null)
            {
                exceptionAction(ex);
            }

            return default;
        }
    }

    public static async Task PostRequestAsync<TPost>(this System.Net.Http.HttpClient client,
                                                     string requestUri,
                                                     TPost param,
                                                     Action<HttpStatusCode>? httpStatusAction = null,
                                                     Action<Exception>? exceptionAction = null)
    {
        try
        {
            HttpContent parameterContent = JsonContent.Create(param, typeof(TPost));

            using HttpResponseMessage response = await client.PostAsync(requestUri, parameterContent);

            if (httpStatusAction is not null)
            {
                httpStatusAction(response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            if (exceptionAction is not null)
            {
                exceptionAction(ex);
            }
        }
    }

    public static async Task<TResponse?> PatchRequestAsync<TPatch, TResponse>(this System.Net.Http.HttpClient client, 
                                                                              string requestUri, 
                                                                              TPatch param, 
                                                                              Action<HttpStatusCode>? httpStatusAction = null,
                                                                              Action<Exception>? exceptionAction = null)
    {
        try
        {
            HttpContent parameterContent = JsonContent.Create(param, typeof(TPatch));

            using HttpResponseMessage response = await client.PatchAsync(requestUri, parameterContent);

            if (httpStatusAction is not null)
            {
                httpStatusAction(response.StatusCode);
            }

            TResponse? model = await ProcessResponseAsync<TResponse>(response, httpStatusAction);

            return model;
        }
        catch (Exception ex)
        {
            if (exceptionAction is not null)
            {
                exceptionAction(ex);
            }

            return default;
        }
    }

    public static async Task PatchRequestAsync<TPatch>(this System.Net.Http.HttpClient client,
                                                       string requestUri,
                                                       TPatch param,
                                                       Action<HttpStatusCode>? httpStatusAction = null,
                                                       Action<Exception>? exceptionAction = null)
    {
        try
        {
            HttpContent parameterContent = JsonContent.Create(param, typeof(TPatch));

            using HttpResponseMessage response = await client.PatchAsync(requestUri, parameterContent);

            if (httpStatusAction is not null)
            {
                httpStatusAction(response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            if (exceptionAction is not null)
            {
                exceptionAction(ex);
            }
        }
    }

    public static async Task<TResponse?> PutRequestAsync<TPut, TResponse>(this System.Net.Http.HttpClient client,
                                                                          string requestUri,
                                                                          TPut param,
                                                                          Action<HttpStatusCode>? httpStatusAction = null,
                                                                          Action<Exception>? exceptionAction = null)
    {
        try
        {
            HttpContent parameterContent = JsonContent.Create(param, typeof(TPut));

            using HttpResponseMessage response = await client.PutAsync(requestUri, parameterContent);

            if (httpStatusAction is not null)
            {
                httpStatusAction(response.StatusCode);
            }

            TResponse? model = await ProcessResponseAsync<TResponse>(response, httpStatusAction);

            return model;
        }
        catch (Exception ex)
        {
            if (exceptionAction is not null)
            {
                exceptionAction(ex);
            }

            return default;
        }
    }

    public static async Task PutRequestAsync<TPut>(this System.Net.Http.HttpClient client,
                                                   string requestUri,
                                                   TPut param,
                                                   Action<HttpStatusCode>? httpStatusAction = null,
                                                   Action<Exception>? exceptionAction = null)
    {
        try
        {
            HttpContent parameterContent = JsonContent.Create(param, typeof(TPut));

            using HttpResponseMessage response = await client.PutAsync(requestUri, parameterContent);

            if (httpStatusAction is not null)
            {
                httpStatusAction(response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            if (exceptionAction is not null)
            {
                exceptionAction(ex);
            }
        }
    }

    public static async Task<bool> DeleteRequestAsync(this System.Net.Http.HttpClient client, 
                                                      string requestUri, 
                                                      Action<HttpStatusCode>? httpStatusAction = null,
                                                      Action<Exception>? exceptionAction = null)
    {
        try
        {
            using HttpResponseMessage response = await client.DeleteAsync(requestUri);

            if (httpStatusAction is not null)
            {
                httpStatusAction(response.StatusCode);
            }

            return response.IsSuccessStatusCode;
        }
        catch(Exception ex)
        {
            if(exceptionAction is not null)
            {
                exceptionAction(ex);
            }

            return false;
        }
    }
}