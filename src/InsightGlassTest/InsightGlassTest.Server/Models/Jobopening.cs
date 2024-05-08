using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsightGlassTest.Server.Models;

[PrimaryKey("JobId", "JobCompanyUserId")]
[Table("jobopening")]
[Index("JobCompanyUserId", Name = "FK_JobCompanyUserId")]
public partial class Jobopening
{
    [Key]
    public string JobCompanyUserId { get; set; } = null!;

    [Key]
    public int JobId { get; set; }

    [StringLength(255)]
    public string JobTitle { get; set; } = null!;

    [StringLength(255)]
    public string? JobEmploymentType { get; set; }

    [Column(TypeName = "text")]
    public string? JobDetails { get; set; }

    [Precision(10, 0)]
    public decimal? JobSalary { get; set; }

    [ForeignKey("JobCompanyUserId")]
    [InverseProperty("Jobopenings")]
    public virtual Company JobCompanyUser { get; set; } = null!;
}
