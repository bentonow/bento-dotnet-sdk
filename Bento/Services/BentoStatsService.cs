using System.Threading.Tasks;

namespace Bento.Services;

public class BentoStatsService : IBentoStatsService
{
    private readonly IBentoClient _client;

    public BentoStatsService(IBentoClient client)
    {
        _client = client;
    }

    public Task<BentoResponse<T>> GetSiteStatsAsync<T>()
    {
        return _client.GetAsync<T>("stats/site");
    }

    public Task<BentoResponse<T>> GetSegmentStatsAsync<T>(string segmentId)
    {
        return _client.GetAsync<T>("stats/segment", new { segment_id = segmentId });
    }

    public Task<BentoResponse<T>> GetReportStatsAsync<T>(string reportId)
    {
        return _client.GetAsync<T>("stats/report", new { report_id = reportId });
    }
}