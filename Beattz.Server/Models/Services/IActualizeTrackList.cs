using Beattz.Server.Models.Services.Domain;

namespace Beattz.Server.Models.Services;

public interface IActualizeTrackList
{
    public Task actualizeTrackListAsync();
}