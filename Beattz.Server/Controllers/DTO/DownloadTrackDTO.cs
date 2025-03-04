namespace Beattz.Server.Controllers.DTO;

public class DownloadTrackDTO
{
    public Guid id { get; set; }
    public string link { get; set; }
    public string filename { get; set; }
    public FileStream file { get; set; }
}