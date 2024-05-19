using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsightGlassTest.Server.Models;

[Table("seeker")]
public partial class Seeker
{
    [Key]
    public string SeekerUserId { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? SeekerBiography { get; set; }

    [StringLength(255)]
    public string? SeekerCity { get; set; }

    [StringLength(255)]
    public string? SeekerGovernate { get; set; }

    [StringLength(255)]
    public string? SeekerSavedResume { get; set; }

    [StringLength(255)]
    public string? SeekerDegree { get; set; }

    [StringLength(255)]
    public string? SeekerUniversity { get; set; }

    public DateOnly? SeekerDateOfGraduation { get; set; }

    [InverseProperty("ApplicationSeekerUser")]
    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    [InverseProperty("FeedbackSeekerUser")]
    public virtual ICollection<Companyfeedback> Companyfeedbacks { get; set; } = new List<Companyfeedback>();

    [ForeignKey("SeekerUserId")]
    [InverseProperty("Seeker")]
    public virtual Aspnetuser? SeekerUser { get; set; } = null!;

    [InverseProperty("ExperienceSeekerUser")]
    public virtual ICollection<Seekerexperience> Seekerexperiences { get; set; } = new List<Seekerexperience>();
}
