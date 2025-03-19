using Kontur.IDevOps.Core.Application.Schedule.Crontab;

namespace Beattz.Server.Models.Services;

public class CrontabSync (ILoggerFactory loggerFactory, IServiceScopeFactory serviceScopeFactory) : CrontabScheduledService (loggerFactory)
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IActualizeTrackList actualizeTrackList = scope.ServiceProvider.GetRequiredService<IActualizeTrackList>();
        IUploadTrackService uploadTrackService = scope.ServiceProvider.GetRequiredService<IUploadTrackService>();
        await actualizeTrackList.actualizeTrackListAsync();
        await uploadTrackService.uploadTrackFromFolderAsync();
    }

    protected override string ScheduleString => "0 * * * * *";
}