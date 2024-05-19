using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsightGlassTest.Server.Models;

[PrimaryKey("ApplicationJobId", "ApplicationSeekerUserId", "ApplicationCompanyUserId")]
[Table("application")]
[Index("ApplicationCompanyUserId", Name = "FK_CompanyApplicationId")]
[Index("ApplicationSeekerUserId", Name = "FK_SeekerApplicationId")]
public partial class Application
{
    [Key]
    public string ApplicationSeekerUserId { get; set; } = null!;

    [Key]
    public string ApplicationCompanyUserId { get; set; } = null!;

    [Key]
    public int ApplicationJobId { get; set; }

    [StringLength(255)]
    public string ApplicationResume { get; set; } = null!;

    [ForeignKey("ApplicationCompanyUserId")]
    [InverseProperty("Applications")]
    public virtual Company ApplicationCompanyUser { get; set; } = null!;

    [ForeignKey("ApplicationSeekerUserId")]
    [InverseProperty("Applications")]
    public virtual Seeker ApplicationSeekerUser { get; set; } = null!;
}
