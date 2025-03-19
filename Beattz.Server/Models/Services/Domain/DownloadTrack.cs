namespace Beattz.Server.Models.Services.Domain;

public class DownloadTrack
{
    public Guid id { get; set; }
    public string link { get; set; }
    public string filename { get; set; }
    public Stream file { get; set; }
}