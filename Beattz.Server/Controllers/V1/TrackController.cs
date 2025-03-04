using AutoMapper;
using Beattz.Server.Controllers.DTO;
using Beattz.Server.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Vostok.Logging.Abstractions;

namespace Beattz.Server.Controllers.V1;

public class TrackController (ILog logger, ITrackService trackService, IMapper mapper) : ControllerBase
{
    [HttpGet("/api/tracks/{trackId}")]
    public async Task<IActionResult> downloadTrack([FromRoute] Guid trackId)
    {
        try
        {
            var downloadedTrack = mapper.Map<DownloadTrackDTO>(await trackService.downloadTrackByIdAsync(trackId));
            return File(downloadedTrack.file, "audio/mp3", downloadedTrack.filename);
        }
        catch (FileNotFoundException e)
        {
            return NotFound($"Track {trackId} not found {e.Message}");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("/api/tracks/")]
    public async Task<IActionResult> getAllTracks()
    {
        try
        {
            return Ok(mapper.Map<TrackDTO[]>(await trackService.getAllTracksAsync()));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}