using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using Bento.Extensions;
using Microsoft.Extensions.Options;

namespace Bento;

public class BentoClient : IBentoClient
{
    private readonly HttpClient _httpClient;
    private readonly BentoOptions _options;

    public BentoClient(HttpClient httpClient, IOptions<BentoOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;

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
        Console.WriteLine($"Full Request URL: {fullUrl}");
        Console.WriteLine(
            $"Request Headers: {string.Join(", ", _httpClient.DefaultRequestHeaders.Select(h => $"{h.Key}:{string.Join(",", h.Value)}"))}");

        var response = await _httpClient.GetAsync(url);
        return await ProcessResponseAsync<T>(response);
    }

    public async Task<BentoResponse<T>> PostAsync<T>(string endpoint, object? data = null)
    {
        var url = BuildUrl(endpoint, null);
        var fullUrl = new Uri(_httpClient.BaseAddress!, url).ToString();
        Console.WriteLine($"Full Request URL: {fullUrl}");
        Console.WriteLine(
            $"Request Headers: {string.Join(", ", _httpClient.DefaultRequestHeaders.Select(h => $"{h.Key}:{string.Join(",", h.Value)}"))}");

        var content = data != null
            ? new StringContent(
                JsonSerializer.Serialize(data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                }),
                Encoding.UTF8,
                "application/json")
            : null;

        if (content != null) Console.WriteLine($"Request Body: {await content.ReadAsStringAsync()}");

        var response = await _httpClient.PostAsync(url, content);
        return await ProcessResponseAsync<T>(response);
    }

    private string BuildUrl(string endpoint, object? queryParams)
    {
        var queryString = "";
        if (queryParams != null)
        {
            var parameters = queryParams.ToDictionary()
                .Where(kvp => kvp.Value != null)
                .Select(kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value?.ToString())}");
            
            if (parameters.Any())
            {
                queryString = "?" + string.Join("&", parameters);
            }
        }

        // Add site_uuid parameter
        var separator = queryString.Contains("?") ? "&" : "?";
        queryString += $"{separator}site_uuid={HttpUtility.UrlEncode(_options.SiteUuid)}";
        
        return $"{endpoint}{queryString}";
    }

    private async Task<BentoResponse<T>> ProcessResponseAsync<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Full Response Content: {content}");
        if (!response.IsSuccessStatusCode)
            return new BentoResponse<T>
            {
                Success = false,
                Error = content,
                StatusCode = response.StatusCode
            };

        var result = JsonSerializer.Deserialize<T>(content);
        return new BentoResponse<T>
        {
            Success = true,
            Data = result,
            StatusCode = response.StatusCode
        };
    }
}