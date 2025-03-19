using Beattz.Server.Models.Persistance.Entities;
using Beattz.Server.Models.Services.Domain;

namespace Beattz.Server.Models.Persistance.Repository;

public interface ITrackRepository
{
    public Task<TrackEntity> selectTrackByIdAsync(Guid trackId);
    public Task<TrackEntity[]> selectAllTracks();
    
    public Task<TrackEntity> insertTrackAsync(TrackEntity track);
    
    public Task<TrackEntity[]> removeTracksAsync(TrackEntity[] tracks);
}