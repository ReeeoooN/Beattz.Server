using Beattz.Server.Models.Persistance.Entities;
using Vostok.Logging.Abstractions;

namespace Beattz.Server.Models.Persistance.Repository;

public class FakeTracksRepository (ILog logger) : ITrackRepository
{
    private static List<TrackEntity> tracks = new List<TrackEntity>
    {
        new TrackEntity
        {
            id = Guid.Parse("8b1fefdb-46c0-4e39-b571-bd7711acfd55"),
            filename = "test_track.mp3",
            link = "test_track.mp3",
        },
        new TrackEntity
        {
            id = Guid.Parse("1c84de1a-83eb-455f-becb-ac645cc6a805"),
            filename = "test_track2.mp3",
            link = "test_track2.mp3",
        }
    };
    public Task<TrackEntity> selectTrackByIdAsync(Guid trackId)
    {
        try
        {
             return Task.FromResult(tracks.FirstOrDefault(track=> track.id == trackId));
        }
        catch (Exception e)
        {
            logger.Error(e, "Error selecting track");
            throw;
        }
    }

    public Task<TrackEntity[]> selectAllTracks()
    {
        try
        {
            return Task.FromResult(tracks.ToArray());
        }
        catch (Exception e)
        {
            logger.Error(e, "Error selecting tracks");
            throw;
        }
    }

    public Task<TrackEntity> insertTrackAsync(TrackEntity track)
    {
        tracks.Add(track);
        return Task.FromResult(track);
    }

    public Task<TrackEntity[]> removeTracksAsync(TrackEntity[] removedTracks)
    {
        foreach (var track in removedTracks)
        {
            tracks.Remove(track);
        }
        return Task.FromResult(removedTracks);
    }
}