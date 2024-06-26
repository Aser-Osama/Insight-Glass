﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsightGlassTest.Server.Models;

[Table("aspnetusers")]
[Index("NormalizedEmail", Name = "EmailIndex")]
[Index("NormalizedUserName", Name = "UserNameIndex", IsUnique = true)]
public partial class Aspnetuser
{
    [Key]
    public string Id { get; set; } = null!;

    [StringLength(256)]
    public string? UserName { get; set; }

    [StringLength(256)]
    public string? NormalizedUserName { get; set; }

    [StringLength(256)]
    public string? Email { get; set; }

    [StringLength(256)]
    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    [MaxLength(6)]
    public DateTime? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }
    public bool? LoggedInBefore { get; set; }
    public string? UserType { get; set; }


    [InverseProperty("User")]
    public virtual ICollection<Aspnetuserclaim> Aspnetuserclaims { get; set; } = new List<Aspnetuserclaim>();

    [InverseProperty("User")]
    public virtual ICollection<Aspnetuserlogin> Aspnetuserlogins { get; set; } = new List<Aspnetuserlogin>();

    [InverseProperty("User")]
    public virtual ICollection<Aspnetusertoken> Aspnetusertokens { get; set; } = new List<Aspnetusertoken>();

    [InverseProperty("BlogUser")]
    public virtual ICollection<Blogcomment> Blogcomments { get; set; } = new List<Blogcomment>();

    [InverseProperty("BlogUser")]
    public virtual ICollection<Blogpost> Blogposts { get; set; } = new List<Blogpost>();

    [InverseProperty("CompanyUser")]
    public virtual Company? Company { get; set; }

    [InverseProperty("SeekerUser")]
    public virtual Seeker? Seeker { get; set; }

    [ForeignKey("ThisUserId")]
    [InverseProperty("ThisUsers")]
    public virtual ICollection<Aspnetuser> FollowsUsers { get; set; } = new List<Aspnetuser>();

    [ForeignKey("UserId")]
    [InverseProperty("Users")]
    public virtual ICollection<Aspnetrole> Roles { get; set; } = new List<Aspnetrole>();

    [ForeignKey("FollowsUserId")]
    [InverseProperty("FollowsUsers")]
    public virtual ICollection<Aspnetuser> ThisUsers { get; set; } = new List<Aspnetuser>();
}
