using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

public class BentoCommandService : IBentoCommandService
{
    private readonly IBentoClient _client;

    public BentoCommandService(IBentoClient client)
    {
        _client = client;
    }

    public Task<BentoResponse<T>> ExecuteCommandAsync<T>(CommandRequest command)
    {
        return ExecuteBatchCommandsAsync<T>(new[] { command });
    }

    public Task<BentoResponse<T>> ExecuteBatchCommandsAsync<T>(IEnumerable<CommandRequest> commands)
    {
        var request = new
        {
            command = commands.Select(c => new
            {
                command = c.Command,
                email = c.Email,
                query = c.Query
            })
        };

        return _client.PostAsync<T>("fetch/commands", request);
    }
}