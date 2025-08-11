using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Implementation of command execution service for Bento API.
/// Provides functionality for running single commands or batch operations to modify subscriber data.
/// See <see href="https://docs.bentonow.com/subscribers#run-command" /> for API documentation.
/// </summary>
public class BentoCommandService : IBentoCommandService
{
    private readonly IBentoClient _client;

    /// <summary>
    /// Initializes a new instance of the BentoCommandService
    /// </summary>
    /// <param name="client">Bento HTTP client for API communication</param>
    /// <exception cref="ArgumentNullException">Thrown when client is null</exception>
    public BentoCommandService(IBentoClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    /// <inheritdoc />
    public Task<BentoResponse<T>> ExecuteCommandAsync<T>(CommandRequest command)
    {
        if (command == null) 
            throw new ArgumentNullException(nameof(command));
            
        return ExecuteBatchCommandsAsync<T>(new[] { command });
    }
    
    /// <inheritdoc />
    public async Task<SubscriberResponse> ExecuteCommandAsync(CommandRequest command)
    {
        var response = await ExecuteCommandAsync<SubscriberResponse>(command);
        if (response.Success && response.Data != null)
        {
            return response.Data;
        }
        throw new BentoException(response.Error ?? "Execute command failed", response.StatusCode);
    }

    /// <inheritdoc />
    public Task<BentoResponse<T>> ExecuteBatchCommandsAsync<T>(IEnumerable<CommandRequest> commands)
    {
        if (commands == null) 
            throw new ArgumentNullException(nameof(commands));

        var commandsList = commands.ToList();
        
        if (commandsList.Count == 0)
            throw new ArgumentException("At least one command is required", nameof(commands));

        // Validate all commands
        foreach (var cmd in commandsList)
        {
            if (cmd == null)
                throw new ArgumentException("Commands collection cannot contain null elements", nameof(commands));
                
            if (string.IsNullOrWhiteSpace(cmd.Command))
                throw new ArgumentException("All commands must have valid Command property", nameof(commands));
                
            if (string.IsNullOrWhiteSpace(cmd.Email))
                throw new ArgumentException("All commands must have valid Email property", nameof(commands));
        }

        var request = new
        {
            command = commandsList.Select(c => new
            {
                command = c.Command,
                email = c.Email,
                query = c.Query
            })
        };

        return _client.PostAsync<T>("fetch/commands", request);
    }
    
    /// <inheritdoc />
    public async Task<SubscriberResponse> ExecuteBatchCommandsAsync(IEnumerable<CommandRequest> commands)
    {
        var response = await ExecuteBatchCommandsAsync<SubscriberResponse>(commands);
        if (response.Success && response.Data != null)
        {
            return response.Data;
        }
        throw new BentoException(response.Error ?? "Execute batch commands failed", response.StatusCode);
    }
}