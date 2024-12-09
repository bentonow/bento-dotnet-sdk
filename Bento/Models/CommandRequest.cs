namespace Bento.Models;

public record CommandRequest(
    string Command,
    string Email,
    string Query
);

public enum CommandType
{
    AddTag,
    AddTagViaEvent,
    RemoveTag,
    AddField,
    RemoveField,
    Subscribe,
    Unsubscribe,
    ChangeEmail
}