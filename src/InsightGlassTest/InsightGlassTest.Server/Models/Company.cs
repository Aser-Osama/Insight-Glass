using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsightGlassTest.Server.Models;

[Table("company")]
public partial class Company
{
    [Key]
    public string CompanyUserId { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? CompanyBiography { get; set; }

    [StringLength(255)]
    public string? CompanyCity { get; set; }

    [StringLength(255)]
    public string? CompanyGovernate { get; set; }

    [StringLength(255)]
    public string? CompanyIndustry { get; set; }

    [StringLength(255)]
    public string? CompanySize { get; set; }

    [InverseProperty("ApplicationCompanyUser")]
    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    [ForeignKey("CompanyUserId")]
    [InverseProperty("Company")]
    public virtual Aspnetuser? CompanyUser { get; set; } = null!;

    [InverseProperty("FeedbackCompanyUser")]
    public virtual ICollection<Companyfeedback> Companyfeedbacks { get; set; } = new List<Companyfeedback>();

    [InverseProperty("JobCompanyUser")]
    public virtual ICollection<Jobopening> Jobopenings { get; set; } = new List<Jobopening>();
}
