using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsightGlassTest.Server.Models;

[PrimaryKey("UserId", "LoginProvider", "Name")]
[Table("aspnetusertokens")]
public partial class Aspnetusertoken
{
    [Key]
    public string UserId { get; set; } = null!;

    [Key]
    public string LoginProvider { get; set; } = null!;

    [Key]
    public string Name { get; set; } = null!;

    public string? Value { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Aspnetusertokens")]
    public virtual Aspnetuser User { get; set; } = null!;
}
