using Beattz.Server.Models.Persistance.Entities;
using Beattz.Server.Models.Persistance.Repository;
using Vostok.Logging.Abstractions;

namespace Beattz.Server.Models.Services;

public class TrackImportService (ILog logger, ITrackRepository trackRepository, IWebHostEnvironment environment) : IUploadTrackService
{ 
    private readonly string[] files = Directory.GetFiles(Path.Combine(environment.WebRootPath, "TrackBox"), "*.mp3");
    
    public async Task uploadTrackFromFolderAsync()
    {
        TrackEntity[] existingTracks = await trackRepository.selectAllTracks();
        try
        {
            logger.Info("starting track import process");
            foreach (var file in files)
            {
                if (!existingTracks.ToList().Any(track => track.filename == file))
                {
                    await trackRepository.insertTrackAsync(new TrackEntity
                    {
                        id = Guid.NewGuid(),
                        filename = file,
                        link = string.Empty,
                    });
                }
            }
        }
        catch (Exception e)
        {
            logger.Error(e, "Error while uploading tracks from folder");
            throw;
        }
    }
}