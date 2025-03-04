using System.ComponentModel.DataAnnotations;

namespace Beattz.Server.Models.Persistance.Entities;

public class TrackEntity
{
    [Key]
    public Guid id { get; set; }
    public string link { get; set; }
    public string filename { get; set; }
}