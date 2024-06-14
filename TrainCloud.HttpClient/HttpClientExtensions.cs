using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace TrainCloud.HttpClient;

/// <summary>
/// This class extension provides a little more build out functionality for the HttpClient
/// Send and receive POCOs with all de/serialization and Status/Error handling wrapped.
/// The methods get used by the applications and the Api Test projects to make it work the same way in Dev/Test and Prod.
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>
    /// Global De/Serializer options for all Http requests in all TrainCloud projects
    /// </summary>
    private static JsonSerializerOptions TrainCloudJsonSerializerOptions { get; } = new() 
    { 
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Deserializes the Http response body into the requested type TResponse
    /// </summary>
    /// <typeparam name="TResponse">The requested type, to deserialize into.</typeparam>
    /// <param name="response">The Http response</param>
    /// <returns>The deserialized object as TResponse</returns>
    private static async Task<TResponse?> DeserializeResponseBodyAsync<TResponse>(HttpResponseMessage response)
    {
        Stream contentStream = await response.Content.ReadAsStreamAsync();

        TResponse? model = await JsonSerializer.DeserializeAsync<TResponse>(contentStream, TrainCloudJsonSerializerOptions);

        return model;
    }

    /// <summary>
    /// GETSs the ressource in requestUri and returns it as TResponse
    /// </summary>
    /// <typeparam name="TResponse">The Model type of the received ressource</typeparam>
    /// <param name="client">Provides a class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.</param>
    /// <param name="requestUri">The Uri of the ressource to work with.</param>
    /// <param name="onHttpStatus">An action, which is raised after completing the request. Contains the actual Http Status code</param>
    /// <param name="onException">An action, which is raised after an exception occurrs</param>
    /// <returns>TResponse</returns>
    public static async Task<TResponse?> GetRequestAsync<TResponse>(this System.Net.Http.HttpClient client, 
                                                                    string requestUri, 
                                                                    Action<HttpStatusCode>? onHttpStatus = null,
                                                                    Action<Exception>? onException = null)
    {
        try
        {
            using HttpResponseMessage response = await client.GetAsync(requestUri);

            if (onHttpStatus is not null)
            {
                onHttpStatus(response.StatusCode);
            }

            if (!response.IsSuccessStatusCode)
            {
                return default;
            }

            TResponse? model = await DeserializeResponseBodyAsync<TResponse>(response);

            return model;
        }
        catch (Exception ex)
        {
            if (onException is not null)
            {
                onException(ex);
            }

            return default;
        }
    }

    /// <summary>
    /// GETSs the ressource in requestUri and returns it as string
    /// </summary>
    /// <param name="client">Provides a class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.</param>
    /// <param name="requestUri">The Uri of the ressource to work with.</param>
    /// <param name="onHttpStatus">An action, which is raised after completing the request. Contains the actual Http Status code</param>
    /// <param name="onException">An action, which is raised after an exception occurrs</param>
    /// <returns>The response as string</returns>
    public static async Task<string?> GetAsStringRequestAsync(this System.Net.Http.HttpClient client,
                                                              string requestUri,
                                                              Action<HttpStatusCode>? onHttpStatus = null,
                                                              Action<Exception>? onException = null)
    {
        using HttpResponseMessage httpResponse = await client.GetAsync(requestUri);

        if (onHttpStatus is not null)
        {
            onHttpStatus(httpResponse.StatusCode);
        }

        string? responseString = await httpResponse.Content.ReadAsStringAsync();

        return responseString;
    }


    /// <summary>
    /// POSTSs the param at the ressource in requestUri and returns a TResponse
    /// </summary>
    /// <typeparam name="TPost">The type if the object which is sent as request body (param)</typeparam>
    /// <typeparam name="TResponse">The Model type of the received ressource</typeparam>
    /// <param name="client">Provides a class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.</param>
    /// <param name="requestUri">The Uri of the ressource to work with.</param>
    /// <param name="param">The object which is sent as request body</param>
    /// <param name="onHttpStatus">An action, which is raised after completing the request. Contains the actual Http Status code</param>
    /// <param name="onException">An action, which is raised after an exception occurrs</param>
    /// <returns>TResponse</returns>
    public static async Task<TResponse?> PostRequestAsync<TPost, TResponse>(this System.Net.Http.HttpClient client, 
                                                                            string requestUri, 
                                                                            TPost param, 
                                                                            Action<HttpStatusCode>? onHttpStatus = null,
                                                                            Action<Exception>? onException = null)
    {
        try
        {
            HttpContent parameterContent = JsonContent.Create(param, typeof(TPost));

            using HttpResponseMessage response = await client.PostAsync(requestUri, parameterContent);

            if (onHttpStatus is not null)
            {
                onHttpStatus(response.StatusCode);
            }

            if(!response.IsSuccessStatusCode)
            {
                return default;
            }

            TResponse? model = await DeserializeResponseBodyAsync<TResponse>(response);

            return model;
        }
        catch (Exception ex)
        {
            if (onException is not null)
            {
                onException(ex);
            }

            return default;
        }
    }

    /// <summary>
    ///  POSTs the param at the ressource in requestUri
    /// </summary>
    /// <typeparam name="TPost">The type if the object which is sent as request body (param)</typeparam>
    /// <param name="client">Provides a class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.</param>
    /// <param name="requestUri">The Uri of the ressource to work with.</param>
    /// <param name="param">The object which is sent as request body</param>
    /// <param name="onHttpStatus">An action, which is raised after completing the request. Contains the actual Http Status code</param>
    /// <param name="onException">An action, which is raised after an exception occurrs</param>
    public static async Task PostRequestAsync<TPost>(this System.Net.Http.HttpClient client,
                                                     string requestUri,
                                                     TPost param,
                                                     Action<HttpStatusCode>? onHttpStatus = null,
                                                     Action<Exception>? onException = null)
    {
        try
        {
            HttpContent parameterContent = JsonContent.Create(param, typeof(TPost));

            using HttpResponseMessage response = await client.PostAsync(requestUri, parameterContent);

            if (onHttpStatus is not null)
            {
                onHttpStatus(response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            if (onException is not null)
            {
                onException(ex);
            }
        }
    }

    /// <summary>
    /// PATCHes the param at the ressource in requestUri and returns a TResponse
    /// </summary>
    /// <typeparam name="TPatch">The type if the object which is sent as request body (param)</typeparam>
    /// <typeparam name="TResponse">The Model type of the received ressource</typeparam>
    /// <param name="client">Provides a class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.</param>
    /// <param name="requestUri">The Uri of the ressource to work with.</param>
    /// <param name="param">The object which is sent as request body</param>
    /// <param name="onHttpStatus">An action, which is raised after completing the request. Contains the actual Http Status code</param>
    /// <param name="onException">An action, which is raised after an exception occurrs</param>
    /// <returns>TResponse</returns>
    public static async Task<TResponse?> PatchRequestAsync<TPatch, TResponse>(this System.Net.Http.HttpClient client, 
                                                                              string requestUri, 
                                                                              TPatch param, 
                                                                              Action<HttpStatusCode>? onHttpStatus = null,
                                                                              Action<Exception>? onException = null)
    {
        try
        {
            HttpContent parameterContent = JsonContent.Create(param, typeof(TPatch));

            using HttpResponseMessage response = await client.PatchAsync(requestUri, parameterContent);

            if (onHttpStatus is not null)
            {
                onHttpStatus(response.StatusCode);
            }

            if (!response.IsSuccessStatusCode)
            {
                return default;
            }

            TResponse? model = await DeserializeResponseBodyAsync<TResponse>(response);

            return model;
        }
        catch (Exception ex)
        {
            if (onException is not null)
            {
                onException(ex);
            }

            return default;
        }
    }

    /// <summary>
    /// PATCHes the param at the ressource in requestUri
    /// </summary>
    /// <typeparam name="TPatch">The type if the object which is sent as request body (param)</typeparam>
    /// <param name="client">Provides a class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.</param>
    /// <param name="requestUri">The Uri of the ressource to work with.</param>
    /// <param name="param">The object which is sent as request body</param>
    /// <param name="onHttpStatus">An action, which is raised after completing the request. Contains the actual Http Status code</param>
    /// <param name="onException">An action, which is raised after an exception occurrs</param>
    public static async Task PatchRequestAsync<TPatch>(this System.Net.Http.HttpClient client,
                                                       string requestUri,
                                                       TPatch param,
                                                       Action<HttpStatusCode>? onHttpStatus = null,
                                                       Action<Exception>? onException = null)
    {
        try
        {
            HttpContent parameterContent = JsonContent.Create(param, typeof(TPatch));

            using HttpResponseMessage response = await client.PatchAsync(requestUri, parameterContent);

            if (onHttpStatus is not null)
            {
                onHttpStatus(response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            if (onException is not null)
            {
                onException(ex);
            }
        }
    }

    /// <summary>
    /// PUTs the param at the ressource in requestUri and returns a TResponse
    /// </summary>
    /// <typeparam name="TPut"></typeparam>
    /// <typeparam name="TResponse">The Model type of the received ressource</typeparam>
    /// <param name="client">Provides a class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.</param>
    /// <param name="requestUri">The Uri of the ressource to work with.</param>
    /// <param name="param">The object which is sent as request body</param>
    /// <param name="onHttpStatus">An action, which is raised after completing the request. Contains the actual Http Status code</param>
    /// <param name="onException">An action, which is raised after an exception occurrs</param>
    /// <returns>TResponse</returns>
    public static async Task<TResponse?> PutRequestAsync<TPut, TResponse>(this System.Net.Http.HttpClient client,
                                                                          string requestUri,
                                                                          TPut param,
                                                                          Action<HttpStatusCode>? onHttpStatus = null,
                                                                          Action<Exception>? onException = null)
    {
        try
        {
            HttpContent parameterContent = JsonContent.Create(param, typeof(TPut));

            using HttpResponseMessage response = await client.PutAsync(requestUri, parameterContent);

            if (onHttpStatus is not null)
            {
                onHttpStatus(response.StatusCode);
            }

            if (!response.IsSuccessStatusCode)
            {
                return default;
            }

            TResponse? model = await DeserializeResponseBodyAsync<TResponse>(response);

            return model;
        }
        catch (Exception ex)
        {
            if (onException is not null)
            {
                onException(ex);
            }

            return default;
        }
    }

    /// <summary>
    /// PUTs the param at the ressource in requestUri
    /// </summary>
    /// <typeparam name="TPut">The type if the object which is sent as request body (param)</typeparam>
    /// <param name="client">Provides a class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.</param>
    /// <param name="requestUri">The Uri of the ressource to work with.</param>
    /// <param name="param">The object which is sent as request body</param>
    /// <param name="onHttpStatus">An action, which is raised after completing the request. Contains the actual Http Status code</param>
    /// <param name="onException">An action, which is raised after an exception occurrs</param>
    public static async Task PutRequestAsync<TPut>(this System.Net.Http.HttpClient client,
                                                   string requestUri,
                                                   TPut param,
                                                   Action<HttpStatusCode>? onHttpStatus = null,
                                                   Action<Exception>? onException = null)
    {
        try
        {
            HttpContent parameterContent = JsonContent.Create(param, typeof(TPut));

            using HttpResponseMessage response = await client.PutAsync(requestUri, parameterContent);

            if (onHttpStatus is not null)
            {
                onHttpStatus(response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            if (onException is not null)
            {
                onException(ex);
            }
        }
    }

    /// <summary>
    /// Deletes the ressource at the Uri specified in requestUri
    /// </summary>
    /// <param name="client">Provides a class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.</param>
    /// <param name="requestUri">The Uri of the ressource to delete.</param>
    /// <param name="onHttpStatus">An action, which is raised after completing the request. Contains the actual Http Status code</param>
    /// <param name="onException">An action, which is raised after an exception occurrs</param>
    /// <returns>True if success (Http 204 - NoContent), False if anything else</returns>
    public static async Task<bool> DeleteRequestAsync(this System.Net.Http.HttpClient client, 
                                                      string requestUri, 
                                                      Action<HttpStatusCode>? onHttpStatus = null,
                                                      Action<Exception>? onException = null)
    {
        try
        {
            using HttpResponseMessage response = await client.DeleteAsync(requestUri);

            if (onHttpStatus is not null)
            {
                onHttpStatus(response.StatusCode);
            }

            return response.IsSuccessStatusCode;
        }
        catch(Exception ex)
        {
            if(onException is not null)
            {
                onException(ex);
            }

            return false;
        }
    }
}