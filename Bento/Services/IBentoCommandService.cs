using System.Collections.Generic;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for executing commands on subscribers via Bento API.
/// Provides functionality for running single commands or batch operations to modify subscriber data.
/// Uses the Run Command endpoint (<see href="https://docs.bentonow.com/subscribers#run-command" />).
/// 
/// Main capabilities:
/// - Execute single commands (add/remove tags, fields, subscribe/unsubscribe, change email)
/// - Execute multiple commands in batch for better performance
/// - Support for all command types: add_tag, add_tag_via_event, remove_tag, add_field, remove_field, subscribe, unsubscribe, change_email
/// 
/// Note: This endpoint has delayed response and is not recommended for most use cases.
/// Consider using batch endpoints or direct subscriber management when possible.
/// </summary>
public interface IBentoCommandService
{
    /// <summary>
    /// Execute a single command to modify subscriber data.
    /// Commands include: add_tag, remove_tag, add_field, remove_field, subscribe, unsubscribe, change_email.
    /// Recommend using <see cref="CommandRequestHelper"/> to create command requests.
    /// </summary>
    /// <typeparam name="T">Type to deserialize the response to</typeparam>
    /// <param name="command">Command to execute on the subscriber</param>
    /// <returns>Generic response containing updated subscriber data</returns>
    Task<BentoResponse<T>> ExecuteCommandAsync<T>(CommandRequest command);
    
    /// <summary>
    /// Execute a single command to modify subscriber data.
    /// Commands include: add_tag, remove_tag, add_field, remove_field, subscribe, unsubscribe, change_email.
    /// Recommend using <see cref="CommandRequestHelper"/> to create command requests.
    /// </summary>
    /// <param name="command">Command to execute on the subscriber</param>
    /// <returns>Updated subscriber data</returns>
    /// <exception cref="BentoException">Thrown when command execution fails</exception>
    Task<SubscriberResponse> ExecuteCommandAsync(CommandRequest command);

    /// <summary>
    /// Execute multiple commands to modify subscriber data in batch.
    /// This is more efficient than multiple single command calls.
    /// Commands include: add_tag, remove_tag, add_field, remove_field, subscribe, unsubscribe, change_email.
    /// </summary>
    /// <typeparam name="T">Type to deserialize the response to</typeparam>
    /// <param name="commands">Collection of commands to execute</param>
    /// <returns>Generic response containing updated subscriber data</returns>
    Task<BentoResponse<T>> ExecuteBatchCommandsAsync<T>(IEnumerable<CommandRequest> commands);
    
    /// <summary>
    /// Execute multiple commands to modify subscriber data in batch.
    /// This is more efficient than multiple single command calls.
    /// Commands include: add_tag, remove_tag, add_field, remove_field, subscribe, unsubscribe, change_email.
    /// </summary>
    /// <param name="commands">Collection of commands to execute</param>
    /// <returns>Updated subscriber data</returns>
    /// <exception cref="BentoException">Thrown when command execution fails</exception>
    Task<SubscriberResponse> ExecuteBatchCommandsAsync(IEnumerable<CommandRequest> commands);
}