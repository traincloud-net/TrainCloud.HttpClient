using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace TrainCloud.HttpClient;

/// <summary>
/// 
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>
    /// 
    /// </summary>
    private static JsonSerializerOptions TrainCloudJsonSerializerOptions { get; set; } = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="response"></param>
    /// <param name="httpStatusAction"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="client"></param>
    /// <param name="requestUri"></param>
    /// <param name="httpStatusAction"></param>
    /// <returns></returns>
    public static async Task<TResponse?> GetRequestAsync<TResponse>(this System.Net.Http.HttpClient client, 
                                                                    string requestUri, 
                                                                    Action<HttpStatusCode>? httpStatusAction = null)
    {
        try
        {
            using HttpResponseMessage response = await client.GetAsync(requestUri);

            TResponse? model = await ProcessResponseAsync<TResponse>(response, httpStatusAction);
            if (httpStatusAction is not null)
            {
                httpStatusAction(response.StatusCode);
            }

            return model;
        }
        catch
        {
            if (httpStatusAction is not null)
            {
                httpStatusAction(HttpStatusCode.InternalServerError);
            }

            return default;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPost"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="client"></param>
    /// <param name="requestUri"></param>
    /// <param name="param"></param>
    /// <param name="httpStatusAction"></param>
    /// <returns></returns>
    public static async Task<TResponse?> PostRequestAsync<TPost, TResponse>(this System.Net.Http.HttpClient client, 
                                                                            string requestUri, 
                                                                            TPost param, 
                                                                            Action<HttpStatusCode>? httpStatusAction = null)
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
        catch
        {
            if (httpStatusAction is not null)
            {
                httpStatusAction(HttpStatusCode.InternalServerError);
            }

            return default;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPatch"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="client"></param>
    /// <param name="requestUri"></param>
    /// <param name="param"></param>
    /// <param name="httpStatusAction"></param>
    /// <returns></returns>
    public static async Task<TResponse?> PatchRequestAsync<TPatch, TResponse>(this System.Net.Http.HttpClient client, 
                                                                              string requestUri, 
                                                                              TPatch param, 
                                                                              Action<HttpStatusCode>? httpStatusAction = null)
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
        catch
        {
            if (httpStatusAction is not null)
            {
                httpStatusAction(HttpStatusCode.InternalServerError);
            }

            return default;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPut"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="client"></param>
    /// <param name="requestUri"></param>
    /// <param name="param"></param>
    /// <param name="httpStatusAction"></param>
    /// <returns></returns>
    public static async Task<TResponse?> PutRequestAsync<TPut, TResponse>(this System.Net.Http.HttpClient client,
                                                                          string requestUri,
                                                                          TPut param,
                                                                          Action<HttpStatusCode>? httpStatusAction = null)
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
        catch
        {
            if (httpStatusAction is not null)
            {
                httpStatusAction(HttpStatusCode.InternalServerError);
            }

            return default;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <param name="requestUri"></param>
    /// <param name="httpStatusAction"></param>
    /// <returns></returns>
    public static async Task<bool> DeleteRequestAsync(this System.Net.Http.HttpClient client, 
                                                      string requestUri, 
                                                      Action<HttpStatusCode>? httpStatusAction = null)
    {
        try
        {
            using HttpResponseMessage response = await client.DeleteAsync(requestUri);
            if (httpStatusAction is not null)
            {
                httpStatusAction(response.StatusCode);
            }

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }
        catch
        {
            if (httpStatusAction is not null)
            {
                httpStatusAction(HttpStatusCode.InternalServerError);
            }

            return default;
        }
    }
}