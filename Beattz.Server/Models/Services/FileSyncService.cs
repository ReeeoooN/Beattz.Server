using Beattz.Server.Models.Persistance.Entities;
using Beattz.Server.Models.Persistance.Repository;
using Vostok.Logging.Abstractions;

namespace Beattz.Server.Models.Services;

public class FileSyncService (ILog logger, ITrackRepository trackRepository, IWebHostEnvironment environment) : IActualizeTrackList
{
    private readonly string[] files = Directory.GetFiles(Path.Combine(environment.WebRootPath, "TrackBox"), "*.mp3");
    
    public async Task actualizeTrackListAsync()
    {
        TrackEntity[] existingTracks = await trackRepository.selectAllTracks();
        try
        {
            logger.Info("start actualizing track list");
            var trackList = existingTracks.ToList();
            foreach (var file in files)
            {
                if (trackList.Any(track => track.filename == file))
                {
                    trackList.Remove(trackList.FirstOrDefault(track => track.filename == file));
                }
            }

            if (trackList.Any())
            {
                await trackRepository.removeTracksAsync(trackList.ToArray());
            }
        }
        catch (Exception e)
        {
            logger.Error(e, "Error while sync track list");
            throw;
        }
    }
}