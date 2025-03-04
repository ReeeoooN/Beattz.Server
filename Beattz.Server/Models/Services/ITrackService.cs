using Beattz.Server.Models.Services.Domain;

namespace Beattz.Server.Models.Services;

public interface ITrackService
{
    public Task<DownloadTrack> downloadTrackByIdAsync(Guid trackId);
    public Task<Track[]> getAllTracksAsync();
}
