use insightglassdb;

/*
Scaffold-DbContext -Context ApplicationDBContext -Connection <YourConnectionString> -Force

When using EF Core in a Web API project, you have the following options to update your `ApplicationDBContext` context to include the new tables:

**Option 1: Scaffold-DbContext**

1. Open the Terminal or Command Prompt in Visual Studio.
2. Run the following command, replacing `<YourConnectionString>` with your database connection string:
```
dotnet ef dbcontext scaffold "Server=<YourServer>;Database=<YourDatabase>;User Id=<YourUsername>;Password=<YourPassword>;" Microsoft.EntityFrameworkCore.SqlServer -c ApplicationDBContext -f
```
This will update the existing `ApplicationDBContext` context to include the new tables.

**Option 2: dotnet ef dbcontext scaffold with --update**

1. Open the Terminal or Command Prompt in Visual Studio.
2. Run the following command, replacing `<YourConnectionString>` with your database connection string:
```
dotnet ef dbcontext scaffold "Server=<YourServer>;Database=<YourDatabase>;User Id=<YourUsername>;Password=<YourPassword>;" Microsoft.EntityFrameworkCore.SqlServer -c ApplicationDBContext --update
```
This will update the existing `ApplicationDBContext` context to include the new tables.

**Option 3: Manual Update**

1. Open the `ApplicationDBContext` class file.
2. Add new `DbSet<T>` properties for each new table, where `T` is the entity class for the new table.
3. Update the `OnModelCreating` method to include the new tables and their relationships.

For example, if you added a new table called `Orders`, you would add the following code:
```csharp
public class ApplicationDBContext : DbContext
{
    // ... existing code ...

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ... existing code ...

        modelBuilder.Entity<Order>().ToTable("Orders");
        // Add any additional configurations or relationships for the new table
    }
}
```
After updating the `ApplicationDBContext` context, you should be able to use the new tables in your Web API project.

Remember to rebuild your project and update any references to the new tables in your code.
*/

CREATE TABLE IF NOT EXISTS Seeker (
	SeekerUserId varchar(255) NOT NULL,
    SeekerId INT NOT NULL AUTO_INCREMENT,

    SeekerBiography Text,
    SeekerCity varchar(255),
    SeekerGovernate varchar(255),
    SeekerSavedResume varchar(255), /* will be a link or path */
    SeekerDegree varchar(255),
    SeekerUniversity varchar(255),
    SeekerDateOfGraduation date,
    
    PRIMARY KEY (SeekerId, SeekerUserId),
    CONSTRAINT FK_userId FOREIGN KEY (SeekerUserId) REFERENCES aspnetusers(Id)
);

/*
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Seeker
{
    [Key]
    [Column(Order = 0)]
    [Required]
    [StringLength(255)]
    public string UserId { get; set; }

    [Key]
    [Column(Order = 1)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SeekerId { get; set; }

    [Required]
    [StringLength(255)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(255)]
    public string LastName { get; set; }

    [Required]
    [StringLength(255)]
    public string Email { get; set; }

    [Required]
    [StringLength(255)]
    public string PhoneNum { get; set; }

    public string Biography { get; set; }

    [StringLength(255)]
    public string City { get; set; }

    [StringLength(255)]
    public string Governate { get; set; }

    [StringLength(255)]
    public string SavedResume { get; set; }

    // Navigation property
    public virtual ApplicationUser User { get; set; }
}

public class ApplicationUser
{
    [Key]
    [StringLength(255)]
    public string Id { get; set; }

    // Other properties...

    // Navigation property
    public virtual Seeker Seeker { get; set; }
}

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    public DbSet<Seeker> Seekers { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}
*/

CREATE TABLE IF NOT EXISTS Company (
	CompanyUserId varchar(255) NOT NULL,
    CompanyId INT NOT NULL AUTO_INCREMENT,

    CompanyBiography Text,
    CompanyCity varchar(255),
    CompanyGovernate varchar(255),
    CompanyIndustry  varchar(255),
    CompanySize  varchar(255),
    
    PRIMARY KEY (CompanyId, CompanyUserId),
    CONSTRAINT FK_CompanyuserId FOREIGN KEY (CompanyUserId) REFERENCES aspnetusers(Id)
);

/*
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Company
{
    [Key]
    [Column(Order = 0)]
    [Required]
    [StringLength(255)]
    public string UserId { get; set; }

    [Key]
    [Column(Order = 1)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CompanyId { get; set; }

    public string Biography { get; set; }

    [StringLength(255)]
    public string City { get; set; }

    [StringLength(255)]
    public string Governate { get; set; }

    [StringLength(255)]
    public string Industry { get; set; }

    [StringLength(255)]
    public string Size { get; set; }

    // Navigation property
    public virtual ApplicationUser User { get; set; }
}

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}
*/

CREATE TABLE IF NOT EXISTS UserFollowing (
	ThisUserId varchar(255) NOT NULL,
	FollowsUserId varchar(255) NOT NULL,

    PRIMARY KEY (ThisUserId, FollowsUserId),
    CONSTRAINT FK_ThisUserId FOREIGN KEY (ThisUserId) REFERENCES aspnetusers(Id),
    CONSTRAINT FK_FollowsUserId FOREIGN KEY (FollowsUserId) REFERENCES aspnetusers(Id)
); /*Many to many table, followers table*/

/*Here is the equivalent Entity Framework Core configuration for the `Following` table:
```
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class Following
{
    public string ThisUserId { get; set; }
    public string FollowsUserId { get; set; }

    public ApplicationUser ThisUser { get; set; }
    public ApplicationUser FollowsUser { get; set; }
}

public class FollowingConfiguration : IEntityTypeConfiguration<Following>
{
    public void Configure(EntityTypeBuilder<Following> builder)
    {
        builder.HasKey(f => new { f.ThisUserId, f.FollowsUserId });

        builder.HasOne(f => f.ThisUser)
            .WithMany()
            .HasForeignKey(f => f.ThisUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(f => f.FollowsUser)
            .WithMany()
            .HasForeignKey(f => f.FollowsUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
```
And in your DbContext:
```
public class MyDbContext : DbContext
{
    public DbSet<Following> Followings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FollowingConfiguration());
    }
}
```
Note that I assumed you have an `ApplicationUser` entity that corresponds to the `aspnetusers` table. You may need to adjust the navigation properties and foreign key configurations accordingly.
*/

CREATE TABLE IF NOT EXISTS CompanyFeedback ( /* A user can only rate a company once */
	FeedbackSeekerId int NOT NULL,
	FeedbackCompanyId int NOT NULL,
	FeedbackRatingScore int NOT NULL,
    FeedbackRatingComment text,
    PRIMARY KEY (FeedbackSeekerId, FeedbackCompanyId),
    CONSTRAINT FK_SeekerId FOREIGN KEY (FeedbackSeekerId) REFERENCES Seeker(SeekerId),
    CONSTRAINT FK_CompanyId FOREIGN KEY (FeedbackCompanyId) REFERENCES Company(CompanyId)
);

CREATE TABLE IF NOT EXISTS SeekerExperience ( /* A user can have multiple records of experience*/
	ExperienceSeekerId int NOT NULL,
	ExperienceId int NOT NULL auto_increment,
	ExperienceCompanyName varchar(255) NOT NULL,
    ExperienceRole varchar(255),
    ExperienceStart date,
    ExperienceEnd date,
    ExperienceDetails text,
    PRIMARY KEY (ExperienceId, ExperienceSeekerId),
    CONSTRAINT FK_SeekerExpId FOREIGN KEY (ExperienceSeekerId) REFERENCES Seeker(SeekerId)
);

CREATE TABLE IF NOT EXISTS JobOpening ( /* A user can have multiple records of experience*/
	JobCompanyId int NOT NULL,
	JobId int NOT NULL auto_increment,
    
	JobTitle varchar(255) NOT NULL,
    JobEmploymentType varchar(255),
    JobDetails text,
    JobSalary decimal,
    
    PRIMARY KEY (JobId, JobCompanyId),
    CONSTRAINT FK_JobCompanyId FOREIGN KEY (JobCompanyId) REFERENCES Company(CompanyId)
);

CREATE TABLE IF NOT EXISTS Application ( /* A user can only rate a company once */
	ApplicationSeekerId int NOT NULL,
	ApplicationCompanyId int NOT NULL,
    ApplicationJobId int NOT NULL auto_increment,
	ApplicationResume varchar(255) not null,
    
    
    PRIMARY KEY (ApplicationJobId, ApplicationSeekerId, ApplicationCompanyId),
    CONSTRAINT FK_SeekerApplicationId FOREIGN KEY (ApplicationSeekerId) REFERENCES Seeker(SeekerId),
    CONSTRAINT FK_CompanyApplicationId FOREIGN KEY (ApplicationCompanyId) REFERENCES Company(CompanyId),
    CONSTRAINT FK_ApplicationJobId FOREIGN KEY (ApplicationJobId) REFERENCES JobOpening(JobId)
);

CREATE TABLE IF NOT EXISTS BlogPost ( /* A user can only rate a company once */
	BlogUserID varchar(255) NOT null,
    BlogID int NOT NULL auto_increment,
	
    BlogTitle varchar(255) NOT null,
	BlogBody text NOT null,
	BlogUploadDate date not null,
    BlogNumLikes int,

    PRIMARY KEY (BlogID, BlogUserID),
    CONSTRAINT FK_UserBlogId FOREIGN KEY (BlogUserID) REFERENCES aspnetusers(Id)
);

CREATE TABLE IF NOT EXISTS BlogComment ( /* A user can only rate a company once */
	BlogUserID varchar(255) NOT null,
    BlogID int NOT NULL,
	BlogCommentID int NOT NULL auto_increment,

	CommentBody text NOT null,
	CommentUploadDate date not null,
    CommentUpVote int,
    CommentDownVote int,

    PRIMARY KEY (BlogCommentID, BlogUserID, BlogID),
    CONSTRAINT FK_BlogCommentId FOREIGN KEY (BlogID) REFERENCES BlogPost(BlogID),
    CONSTRAINT FK_UserCommentId FOREIGN KEY (BlogUserID) REFERENCES aspnetusers(Id)
);
