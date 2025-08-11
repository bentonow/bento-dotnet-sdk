using System.Collections.Generic;

namespace Bento.Models;

/// <summary>
/// Request model for running commands on subscribers.
/// Used with the Run Command API endpoint to modify subscriber data.
/// See <see href="https://docs.bentonow.com/subscribers#run-command" /> for details.
/// 
/// Note: It's recommended to use <see cref="CommandRequestHelper"/> methods instead of direct object creation
/// for better type safety and proper data formatting.
/// </summary>
/// <param name="Command">The command to execute (required). Valid values: add_tag, add_tag_via_event, remove_tag, add_field, remove_field, subscribe, unsubscribe, change_email</param>
/// <param name="Email">Email address of the subscriber to modify (required)</param>
/// <param name="Query">Query data for the command (optional for some commands). Can be a string for simple values or an object for complex data like add_field</param>
public record CommandRequest(
    string Command,
    string Email,
    object? Query = null
);

/// <summary>
/// Batch command request for executing multiple commands at once
/// </summary>
public record BatchCommandRequest
{
    /// <summary>
    /// Array of commands to execute
    /// </summary>
    public IEnumerable<CommandRequest> Command { get; init; } = new List<CommandRequest>();
}

/// <summary>
/// Enumeration of available command types for type safety.
/// These correspond to the command strings accepted by the Bento API.
/// 
/// Note: For actual usage, prefer <see cref="CommandRequestHelper"/> methods
/// which handle proper string conversion and parameter formatting automatically.
/// </summary>
public enum CommandType
{
    /// <summary>
    /// Add a tag to subscriber.
    /// API command: "add_tag"
    /// </summary>
    AddTag,
    
    /// <summary>
    /// Add a tag via event to subscriber.
    /// API command: "add_tag_via_event"
    /// </summary>
    AddTagViaEvent,
    
    /// <summary>
    /// Remove a tag from subscriber.
    /// API command: "remove_tag"
    /// </summary>
    RemoveTag,
    
    /// <summary>
    /// Add a custom field to subscriber.
    /// API command: "add_field"
    /// Requires Query as object with 'key' and 'value' properties.
    /// </summary>
    AddField,
    
    /// <summary>
    /// Remove a custom field from subscriber.
    /// API command: "remove_field"
    /// </summary>
    RemoveField,
    
    /// <summary>
    /// Subscribe the subscriber (opt them in).
    /// API command: "subscribe"
    /// No Query parameter required.
    /// </summary>
    Subscribe,
    
    /// <summary>
    /// Unsubscribe the subscriber (opt them out).
    /// API command: "unsubscribe"
    /// No Query parameter required.
    /// </summary>
    Unsubscribe,
    
    /// <summary>
    /// Change subscriber's email address.
    /// API command: "change_email"
    /// Query should contain the new email address.
    /// </summary>
    ChangeEmail
}

/// <summary>
/// Constants for command strings used by the Bento API.
/// These correspond to the actual string values expected by the API endpoints.
/// </summary>
public static class CommandStrings
{
    /// <summary>Add a tag to subscriber</summary>
    public const string AddTag = "add_tag";
    
    /// <summary>Add a tag via event to subscriber</summary>
    public const string AddTagViaEvent = "add_tag_via_event";
    
    /// <summary>Remove a tag from subscriber</summary>
    public const string RemoveTag = "remove_tag";
    
    /// <summary>Add a custom field to subscriber</summary>
    public const string AddField = "add_field";
    
    /// <summary>Remove a custom field from subscriber</summary>
    public const string RemoveField = "remove_field";
    
    /// <summary>Subscribe the subscriber</summary>
    public const string Subscribe = "subscribe";
    
    /// <summary>Unsubscribe the subscriber</summary>
    public const string Unsubscribe = "unsubscribe";
    
    /// <summary>Change subscriber's email address</summary>
    public const string ChangeEmail = "change_email";
}

/// <summary>
/// Helper class for creating command requests with type safety and proper data formatting.
/// Provides convenient methods for all supported command types.
/// 
/// This is the recommended way to create CommandRequest objects instead of direct instantiation,
/// as it ensures proper command names, data types, and Query object structure.
/// </summary>
public static class CommandRequestHelper
{
    /// <summary>
    /// Create an add_tag command request
    /// </summary>
    public static CommandRequest AddTag(string email, string tag) =>
        new(CommandStrings.AddTag, email, tag);

    /// <summary>
    /// Create an add_tag_via_event command request
    /// </summary>
    public static CommandRequest AddTagViaEvent(string email, string tag) =>
        new(CommandStrings.AddTagViaEvent, email, tag);

    /// <summary>
    /// Create a remove_tag command request
    /// </summary>
    public static CommandRequest RemoveTag(string email, string tag) =>
        new(CommandStrings.RemoveTag, email, tag);

    /// <summary>
    /// Create an add_field command request
    /// </summary>
    public static CommandRequest AddField(string email, string key, object value) =>
        new(CommandStrings.AddField, email, new { key, value });

    /// <summary>
    /// Create a remove_field command request
    /// </summary>
    public static CommandRequest RemoveField(string email, string field) =>
        new(CommandStrings.RemoveField, email, field);

    /// <summary>
    /// Create a subscribe command request
    /// </summary>
    public static CommandRequest Subscribe(string email) =>
        new(CommandStrings.Subscribe, email);

    /// <summary>
    /// Create an unsubscribe command request
    /// </summary>
    public static CommandRequest Unsubscribe(string email) =>
        new(CommandStrings.Unsubscribe, email);

    /// <summary>
    /// Create a change_email command request
    /// </summary>
    public static CommandRequest ChangeEmail(string currentEmail, string newEmail) =>
        new(CommandStrings.ChangeEmail, currentEmail, newEmail);
}
