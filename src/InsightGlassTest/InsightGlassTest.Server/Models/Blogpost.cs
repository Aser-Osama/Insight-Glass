using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsightGlassTest.Server.Models;

[PrimaryKey("BlogId", "BlogUserId")]
[Table("blogpost")]
[Index("BlogUserId", Name = "FK_UserBlogId")]
public partial class Blogpost
{
    [Key]
    [Column("BlogUserID")]
    public string BlogUserId { get; set; } = null!;

    [Key]
    [Column("BlogID")]
    public int BlogId { get; set; }

    [StringLength(255)]
    public string BlogTitle { get; set; } = null!;

    [Column(TypeName = "text")]
    public string BlogBody { get; set; } = null!;

    public DateOnly BlogUploadDate { get; set; }

    public int? BlogNumLikes { get; set; }

    [ForeignKey("BlogUserId")]
    [InverseProperty("Blogposts")]
    public virtual Aspnetuser BlogUser { get; set; } = null!;
}
