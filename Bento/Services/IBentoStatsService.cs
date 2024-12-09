using Bento.Models;

namespace Bento.Services;

public interface IBentoStatsService
{
    Task<BentoResponse<T>> GetSiteStatsAsync<T>();
    Task<BentoResponse<T>> GetSegmentStatsAsync<T>(string segmentId);
    Task<BentoResponse<T>> GetReportStatsAsync<T>(string reportId);
}