using AutoMapper;
using Beattz.Server.Models.Persistance.Repository;
using Beattz.Server.Models.Services.Domain;
using Vostok.Logging.Abstractions;

namespace Beattz.Server.Models.Services;

public class TrackService (ILog logger, ITrackRepository trackRepository, IMapper mapper, IWebHostEnvironment environment) : ITrackService
{
    public async Task<DownloadTrack> downloadTrackByIdAsync(Guid trackId)
    {
        try
        {
            var track = mapper.Map<DownloadTrack>(await trackRepository.selectTrackByIdAsync(trackId));
            var path = Path.Combine(environment.WebRootPath, Path.Combine("TrackBox", track.filename));
            track.file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return track;
        }
        catch (DirectoryNotFoundException e)
        {
            logger.Error(e, "Could not find track");
            throw;
        }
        catch (Exception e)
        {
            logger.Error(e, "Could not download track");
            throw;
        }
    }

    public async Task<Track[]> getAllTracksAsync()
    {
        try
        {
            return mapper.Map<Track[]>(await trackRepository.selectAllTracks());
        }
        catch (Exception e)
        {
            logger.Error(e, "Error while getting all tracks");
            throw;
        }
    }
}