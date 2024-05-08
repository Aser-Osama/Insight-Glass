using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsightGlassTest.Server.Models;

[PrimaryKey("ExperienceId", "ExperienceSeekerUserId")]
[Table("seekerexperience")]
[Index("ExperienceSeekerUserId", Name = "FK_SeekerExpId1")]
public partial class Seekerexperience
{
    [Key]
    public string ExperienceSeekerUserId { get; set; } = null!;

    [Key]
    public int ExperienceId { get; set; }

    [StringLength(255)]
    public string ExperienceCompanyName { get; set; } = null!;

    [StringLength(255)]
    public string? ExperienceRole { get; set; }

    public DateOnly? ExperienceStart { get; set; }

    public DateOnly? ExperienceEnd { get; set; }

    [Column(TypeName = "text")]
    public string? ExperienceDetails { get; set; }

    [ForeignKey("ExperienceSeekerUserId")]
    [InverseProperty("Seekerexperiences")]
    public virtual Seeker ExperienceSeekerUser { get; set; } = null!;
}
