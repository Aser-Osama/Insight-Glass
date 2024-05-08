using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InsightGlassTest.Server.Models;

[PrimaryKey("BlogCommentId", "BlogUserId", "BlogId")]
[Table("blogcomment")]
[Index("BlogId", Name = "FK_BlogCommentId")]
[Index("BlogUserId", Name = "FK_UserCommentId")]
public partial class Blogcomment
{
    [Key]
    [Column("BlogUserID")]
    public string BlogUserId { get; set; } = null!;

    [Key]
    [Column("BlogID")]
    public int BlogId { get; set; }

    [Key]
    [Column("BlogCommentID")]
    public int BlogCommentId { get; set; }

    [Column(TypeName = "text")]
    public string CommentBody { get; set; } = null!;

    public DateOnly CommentUploadDate { get; set; }

    public int? CommentUpVote { get; set; }

    public int? CommentDownVote { get; set; }

    [ForeignKey("BlogUserId")]
    [InverseProperty("Blogcomments")]
    public virtual Aspnetuser BlogUser { get; set; } = null!;
}
