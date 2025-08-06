using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

public interface IBentoFieldService
{
    Task<BentoResponse<T>> GetFieldsAsync<T>();
    Task<BentoResponse<T>> CreateFieldAsync<T>(FieldRequest field);
}