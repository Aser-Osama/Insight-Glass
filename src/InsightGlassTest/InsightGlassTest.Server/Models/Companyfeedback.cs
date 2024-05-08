using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsightGlassTest.Server.Models;

[PrimaryKey("FeedbackSeekerUserId", "FeedbackCompanyUserId")]
[Table("companyfeedback")]
[Index("FeedbackCompanyUserId", Name = "FK_CompanyUserId1")]
public partial class Companyfeedback
{
    [Key]
    public string FeedbackSeekerUserId { get; set; } = null!;

    [Key]
    public string FeedbackCompanyUserId { get; set; } = null!;

    public int FeedbackRatingScore { get; set; }

    [Column(TypeName = "text")]
    public string? FeedbackRatingComment { get; set; }

    [ForeignKey("FeedbackCompanyUserId")]
    [InverseProperty("Companyfeedbacks")]
    public virtual Company FeedbackCompanyUser { get; set; } = null!;

    [ForeignKey("FeedbackSeekerUserId")]
    [InverseProperty("Companyfeedbacks")]
    public virtual Seeker FeedbackSeekerUser { get; set; } = null!;
}
