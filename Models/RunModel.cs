using System.ComponentModel.DataAnnotations;

namespace RunTrackerApp.Models;

public class RunModel
{
    public int? Id { get; set; }
    public string? RunDate { get; set; }
    public float? Length { get; set; }

}
