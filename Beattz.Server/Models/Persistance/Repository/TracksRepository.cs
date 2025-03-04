using AutoMapper;
using Beattz.Server.Models.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Vostok.Logging.Abstractions;

namespace Beattz.Server.Models.Persistance.Repository;

public class TracksRepository (TracksDbContext db, ILog logger) : ITrackRepository
{
    public async Task<TrackEntity> selectTrackByIdAsync(Guid trackId)
    {
        try
        {
            return await db.tracks.FindAsync(trackId); 
        }
        catch (Exception e)
        {
            logger.Error(e, "Error while selecting track by id");
            throw;
        }
    }

    public async Task<TrackEntity[]> selectAllTracks()
    {
        try
        {
            return await db.tracks.ToArrayAsync();
        }
        catch (Exception e)
        {
            logger.Error(e, "Error while selecting all tracks");
            throw;
        }
    }
}