using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Bento.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bento;

public class BentoClient : IBentoClient
{
    private readonly HttpClient _httpClient;
    private readonly BentoOptions _options;
    private readonly ILogger<BentoClient> _logger;

    public BentoClient(HttpClient httpClient, IOptions<BentoOptions> options, ILogger<BentoClient> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;

        _httpClient.BaseAddress = new Uri("https://app.bentonow.com/api/v1/");

        var credentials = Convert.ToBase64String(
            Encoding.ASCII.GetBytes($"{_options.PublishableKey}:{_options.SecretKey}")
        );

        // Set all required headers
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", credentials);
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd($"bento-dotnet-{_options.SiteUuid}");
    }

    public async Task<BentoResponse<T>> GetAsync<T>(string endpoint, object? queryParams = null)
    {
        var url = BuildUrl(endpoint, queryParams);
        var fullUrl = new Uri(_httpClient.BaseAddress!, url).ToString();
        _logger.LogDebug("Making GET request to: {Url}", fullUrl);
        _logger.LogTrace("Request headers: {Headers}", 
            string.Join(", ", _httpClient.DefaultRequestHeaders.Select(h => $"{h.Key}:{string.Join(",", h.Value)}")));

        var response = await _httpClient.GetAsync(url);
        return await ProcessResponseAsync<T>(response);
    }

    public async Task<BentoResponse<T>> PostAsync<T>(string endpoint, object? data = null)
    {
        var url = BuildUrl(endpoint, null);
        var fullUrl = new Uri(_httpClient.BaseAddress!, url).ToString();
        _logger.LogDebug("Making POST request to: {Url}", fullUrl);
        _logger.LogTrace("Request headers: {Headers}", 
            string.Join(", ", _httpClient.DefaultRequestHeaders.Select(h => $"{h.Key}:{string.Join(",", h.Value)}")));

        var content = data != null
            ? new StringContent(
                JsonSerializer.Serialize(data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                }),
                Encoding.UTF8,
                "application/json")
            : null;

        if (content != null) 
        {
            var requestBody = await content.ReadAsStringAsync();
            _logger.LogTrace("Request body: {Body}", requestBody);
        }

        var response = await _httpClient.PostAsync(url, content);
        return await ProcessResponseAsync<T>(response);
    }

    private string BuildUrl(string endpoint, object? queryParams)
    {
        var queryList = new List<string>();

        if (queryParams != null)
        {
            var parameters = queryParams.ToDictionary()
                .Where(kvp => kvp.Value != null)
                .Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value!.ToString())}");

            queryList.AddRange(parameters);
        }

        // Always append site_uuid
        queryList.Add($"site_uuid={WebUtility.UrlEncode(_options.SiteUuid)}");

        var queryString = queryList.Any() ? "?" + string.Join("&", queryList) : "";

        return $"{endpoint}{queryString}";
    }

    private async Task<BentoResponse<T>> ProcessResponseAsync<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        _logger.LogTrace("Response content: {Content}", content);
        
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request failed with status {StatusCode}: {Error}", response.StatusCode, content);
            return new BentoResponse<T>
            {
                Success = false,
                Error = content,
                StatusCode = response.StatusCode
            };
        }

        var result = JsonSerializer.Deserialize<T>(content);
        _logger.LogDebug("Request completed successfully with status {StatusCode}", response.StatusCode);
        return new BentoResponse<T>
        {
            Success = true,
            Data = result,
            StatusCode = response.StatusCode
        };
    }
}