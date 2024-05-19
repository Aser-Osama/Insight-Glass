using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsightGlassTest.Server.Models;

[PrimaryKey("LoginProvider", "ProviderKey")]
[Table("aspnetuserlogins")]
[Index("UserId", Name = "IX_AspNetUserLogins_UserId")]
public partial class Aspnetuserlogin
{
    [Key]
    public string LoginProvider { get; set; } = null!;

    [Key]
    public string ProviderKey { get; set; } = null!;

    public string? ProviderDisplayName { get; set; }

    public string UserId { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Aspnetuserlogins")]
    public virtual Aspnetuser User { get; set; } = null!;
}
