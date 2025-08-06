using System.Collections.Generic;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

public interface IBentoCommandService
{
    Task<BentoResponse<T>> ExecuteCommandAsync<T>(CommandRequest command);
    Task<BentoResponse<T>> ExecuteBatchCommandsAsync<T>(IEnumerable<CommandRequest> commands);
}