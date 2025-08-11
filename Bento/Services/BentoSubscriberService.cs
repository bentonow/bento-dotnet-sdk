using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Implementation of subscriber management service for Bento API.
/// Provides functionality for creating, finding, searching, importing subscribers and running commands.
/// See <see href="https://docs.bentonow.com/subscribers" /> for API documentation.
/// </summary>
public class BentoSubscriberService : IBentoSubscriberService
{
    private readonly IBentoClient _client;

    /// <summary>
    /// Initializes a new instance of the BentoSubscriberService
    /// </summary>
    /// <param name="client">Bento HTTP client for API communication</param>
    /// <exception cref="ArgumentNullException">Thrown when client is null</exception>
    public BentoSubscriberService(IBentoClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    /// <inheritdoc />
    public Task<BentoResponse<T>> FindSubscriberAsync<T>(string email, string? uuid = null)
    {
        if (string.IsNullOrWhiteSpace(email)) 
            throw new ArgumentNullException(nameof(email));

        var queryParams = new { email = Uri.EscapeDataString(email), uuid = !string.IsNullOrWhiteSpace(uuid) ? Uri.EscapeDataString(uuid) : null };
        
        return _client.GetAsync<T>("fetch/subscribers", queryParams);
    }

    /// <inheritdoc />
    public async Task<SubscriberResponse> FindSubscriberAsync(string email, string? uuid = null)
    {
        var response = await FindSubscriberAsync<SubscriberResponse>(email, uuid);
        if (response.Success && response.Data != null)
        {
            return response.Data;
        }
        throw new BentoException(response.Error ?? "Find subscriber failed", response.StatusCode);
    }

    /// <inheritdoc />
    public Task<BentoResponse<T>> FindSubscriberAsync<T>(FindSubscriberRequest request)
    {
        if (request == null) 
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.Email) && string.IsNullOrWhiteSpace(request.Uuid))
            throw new ArgumentException("Either Email or Uuid must be provided", nameof(request));

        return FindSubscriberAsync<T>(request.Email ?? string.Empty, request.Uuid);
    }

    /// <inheritdoc />
    public async Task<SubscriberResponse> FindSubscriberAsync(FindSubscriberRequest request)
    {
        var response = await FindSubscriberAsync<SubscriberResponse>(request);
        if (response.Success && response.Data != null)
        {
            return response.Data;
        }
        throw new BentoException(response.Error ?? "Find subscriber failed", response.StatusCode);
    }

    /// <inheritdoc />
    public Task<BentoResponse<T>> CreateSubscriberAsync<T>(SubscriberRequest subscriber)
    {
        if (subscriber == null) 
            throw new ArgumentNullException(nameof(subscriber));
        
        if (string.IsNullOrWhiteSpace(subscriber.Email))
            throw new ArgumentException("Email is required", nameof(subscriber));

        var request = new { subscriber = new { email = subscriber.Email } };
        return _client.PostAsync<T>("fetch/subscribers", request);
    }

    /// <inheritdoc />
    public async Task<SubscriberResponse> CreateSubscriberAsync(SubscriberRequest subscriber)
    {
        var response = await CreateSubscriberAsync<SubscriberResponse>(subscriber);
        if (response.Success && response.Data != null)
        {
            return response.Data;
        }
        throw new BentoException(response.Error ?? "Create subscriber failed", response.StatusCode);
    }

    /// <inheritdoc />
    public Task<BentoResponse<T>> ImportSubscribersAsync<T>(IEnumerable<SubscriberRequest> subscribers)
    {
        if (subscribers == null) 
            throw new ArgumentNullException(nameof(subscribers));

        var subscribersList = subscribers.ToList();
        
        if (subscribersList.Count == 0)
            throw new ArgumentException("At least one subscriber is required", nameof(subscribers));
            
        if (subscribersList.Count > 1000)
            throw new ArgumentException("Maximum 1000 subscribers per request", nameof(subscribers));

        foreach (var sub in subscribersList)
        {
            if (string.IsNullOrWhiteSpace(sub.Email))
                throw new ArgumentException("All subscribers must have valid email addresses", nameof(subscribers));
        }

        var request = new
        {
            subscribers = subscribersList.Select(s => new
            {
                email = s.Email,
                first_name = s.FirstName,
                last_name = s.LastName,
                tags = s.Tags != null ? string.Join(",", s.Tags) : null,
                remove_tags = s.RemoveTags != null ? string.Join(",", s.RemoveTags) : null,
                fields = s.Fields
            })
        };

        return _client.PostAsync<T>("batch/subscribers", request);
    }

    /// <inheritdoc />
    public async Task<ImportSubscribersResponse> ImportSubscribersAsync(IEnumerable<SubscriberRequest> subscribers)
    {
        var response = await ImportSubscribersAsync<ImportSubscribersResponse>(subscribers);
        if (response.Success && response.Data != null)
        {
            return response.Data;
        }
        throw new BentoException(response.Error ?? "Import subscribers failed", response.StatusCode);
    }

    /// <inheritdoc />
    public Task<BentoResponse<T>> SearchSubscribersAsync<T>(SearchSubscribersRequest request)
    {
        if (request == null) 
            throw new ArgumentNullException(nameof(request));

        if (request.Page < 1)
            throw new ArgumentException("Page must be greater than 0", nameof(request));

        var requestBody = new
        {
            page = request.Page,
            created_at = request.CreatedAt != null ? new { gt = request.CreatedAt.Gt, lt = request.CreatedAt.Lt } : null,
            updated_at = request.UpdatedAt != null ? new { gt = request.UpdatedAt.Gt, lt = request.UpdatedAt.Lt } : null,
            last_event_at = request.LastEventAt != null ? new { gt = request.LastEventAt.Gt, lt = request.LastEventAt.Lt } : null,
            unsubscribed_at = request.UnsubscribedAt != null ? new { gt = request.UnsubscribedAt.Gt, lt = request.UnsubscribedAt.Lt } : null
        };

        // According to docs, this should be GET with request body, but we'll use POST for better compatibility
        return _client.PostAsync<T>("fetch/search", requestBody);
    }

    /// <inheritdoc />
    public async Task<SearchSubscribersResponse> SearchSubscribersAsync(SearchSubscribersRequest request)
    {
        var response = await SearchSubscribersAsync<SearchSubscribersResponse>(request);
        if (response.Success && response.Data != null)
        {
            return response.Data;
        }
        throw new BentoException(response.Error ?? "Search subscribers failed", response.StatusCode);
    }

    /// <inheritdoc />
    public Task<BentoResponse<T>> RunCommandAsync<T>(CommandRequest command)
    {
        if (command == null) 
            throw new ArgumentNullException(nameof(command));
            
        if (string.IsNullOrWhiteSpace(command.Command))
            throw new ArgumentException("Command is required", nameof(command));
            
        if (string.IsNullOrWhiteSpace(command.Email))
            throw new ArgumentException("Email is required", nameof(command));

        var request = new { command = new[] { command } };
        return _client.PostAsync<T>("fetch/commands", request);
    }

    /// <inheritdoc />
    public async Task<SubscriberResponse> RunCommandAsync(CommandRequest command)
    {
        var response = await RunCommandAsync<SubscriberResponse>(command);
        if (response.Success && response.Data != null)
        {
            return response.Data;
        }
        throw new BentoException(response.Error ?? "Run command failed", response.StatusCode);
    }

    /// <inheritdoc />
    public Task<BentoResponse<T>> RunCommandsAsync<T>(IEnumerable<CommandRequest> commands)
    {
        if (commands == null) 
            throw new ArgumentNullException(nameof(commands));

        var commandsList = commands.ToList();
        
        if (commandsList.Count == 0)
            throw new ArgumentException("At least one command is required", nameof(commands));

        foreach (var cmd in commandsList)
        {
            if (string.IsNullOrWhiteSpace(cmd.Command))
                throw new ArgumentException("All commands must have valid Command property", nameof(commands));
                
            if (string.IsNullOrWhiteSpace(cmd.Email))
                throw new ArgumentException("All commands must have valid Email property", nameof(commands));
        }

        var request = new { command = commandsList };
        return _client.PostAsync<T>("fetch/commands", request);
    }

    /// <inheritdoc />
    public async Task<SubscriberResponse> RunCommandsAsync(IEnumerable<CommandRequest> commands)
    {
        var response = await RunCommandsAsync<SubscriberResponse>(commands);
        if (response.Success && response.Data != null)
        {
            return response.Data;
        }
        throw new BentoException(response.Error ?? "Run commands failed", response.StatusCode);
    }
}