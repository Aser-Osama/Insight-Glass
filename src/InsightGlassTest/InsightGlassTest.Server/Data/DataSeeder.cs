//using System;
//using System.Collections.Generic;
//using Bogus;
//using InsightGlassTest.Server.Data;
//using InsightGlassTest.Server.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//public class DataSeeder
//{
//    private readonly UserManager<ApplicationUser> _userManager;

//    public DataSeeder(UserManager<ApplicationUser> userManager)
//    {
//        Console.WriteLine("DataSeeder constructor");
//        _userManager = userManager;
//    }
//    public async Task GenerateUsers(UserManager<ApplicationUser> userManager, int count)
//    {
//        var faker = new Faker();
//        var users = new List<ApplicationUser>();

//        using (var sw = new StreamWriter("UserPasswords.txt"))
//        {
//            for (int i = 0; i < count; i++)
//            {
//                var user = new ApplicationUser
//                {
//                    UserName = faker.Internet.Email(),
//                    Email = faker.Internet.Email(),
//                    EmailConfirmed = true,
//                    PhoneNumber = faker.Phone.PhoneNumber(),
//                    PhoneNumberConfirmed = true
//                };

//                var password = faker.Internet.Password();
//                var result = await userManager.CreateAsync(user, password);

//                if (result.Succeeded)
//                {
//                    users.Add(user);
//                    await sw.WriteLineAsync($"{user.UserName}: {password}");
//                }
//            }
//        }
//    }

//    public List<Company> GenerateCompanies(List<ApplicationUser> _users, int count)
//    {
//        var users = _users.GetRange(0, _users.Count);
//        var faker = new Faker<Company>()
//            .RuleFor(s => s.CompanyUserId, f => { var user = users.First(); users.RemoveAt(0); return user.Id; })
//            .RuleFor(s => s.CompanyCity, f => f.Address.City())
//            .RuleFor(c => c.CompanyBiography, f => f.Company.Bs())
//            .RuleFor(c => c.CompanyCity, f => f.Address.City())
//            .RuleFor(c => c.CompanyGovernate, f => f.Address.State())
//            .RuleFor(c => c.CompanyIndustry, f => f.Company.CompanyName())
//            .RuleFor(c => c.CompanySize, f => f.Company.CompanySuffix());

//        return faker.Generate(count);
//    }

//    public List<Jobopening> GenerateJobOpenings(List<Company> companies, int count)
//    {
//        var faker = new Faker<Jobopening>()
//            .RuleFor(j => j.JobCompanyUserId, f => f.PickRandom(companies).CompanyUserId)
//            .RuleFor(j => j.JobTitle, f => f.Name.JobTitle())
//            .RuleFor(j => j.JobEmploymentType, f => f.Name.JobType())
//            .RuleFor(j => j.JobDetails, f => f.Lorem.Paragraph())
//            .RuleFor(j => j.JobSalary, f => f.Random.Decimal(30000, 150000));

//        return faker.Generate(count);
//    }

//    public List<Seeker> GenerateSeekers(List<ApplicationUser> _users, int count)
//    {
//        var users = _users.GetRange(0, _users.Count);
//        var faker = new Faker<Seeker>()
//            .RuleFor(s => s.SeekerUserId, f => { var user = users.First(); users.RemoveAt(0); return user.Id; }).RuleFor(s => s.SeekerCity, f => f.Address.City())
//            .RuleFor(s => s.SeekerGovernate, f => f.Address.State())
//            .RuleFor(s => s.SeekerSavedResume, f => f.Internet.Url())
//            .RuleFor(s => s.SeekerDegree, f => f.Name.JobTitle())
//            .RuleFor(s => s.SeekerUniversity, f => f.Company.CompanyName())
//            .RuleFor(s => s.SeekerDateOfGraduation, f => DateOnly.FromDateTime(f.Date.Past(10)));

//        return faker.Generate(count);
//    }

//    public List<Application> GenerateApplications(List<Seeker> seekers, List<Jobopening> jobs, int count)
//    {
//        // Ensure we have seekers and jobs available
//        if (seekers == null || seekers.Count == 0)
//        {
//            throw new ArgumentException("The list of seekers is empty or null.");
//        }

//        if (jobs == null || jobs.Count == 0)
//        {
//            throw new ArgumentException("The list of jobs is empty or null.");
//        }

//        // Generate valid pairs from the existing seekers and jobs
//        var validPairs = (from seeker in seekers
//                          from job in jobs
//                          select new { seeker.SeekerUserId, job.JobCompanyUserId, job.JobId }).ToList();

//        // Ensure the count doesn't exceed the number of valid pairs
//        if (count > validPairs.Count)
//        {
//            count = validPairs.Count;
//        }

//        var faker = new Faker<Application>()
//            .RuleFor(a => a.ApplicationResume, f => f.Internet.Url());

//        var generatedApplications = new List<Application>();

//        // Shuffle the validPairs to ensure randomness
//        validPairs = validPairs.OrderBy(x => Guid.NewGuid()).ToList();

//        for (int i = 0; i < count; i++)
//        {
//            var pair = validPairs[i];

//            // Generate an application using the valid pair
//            var application = faker.Generate();
//            application.ApplicationSeekerUserId = pair.SeekerUserId;
//            application.ApplicationCompanyUserId = pair.JobCompanyUserId;
//            application.ApplicationJobId = pair.JobId;

//            generatedApplications.Add(application);
//        }

//        return generatedApplications;
//    }


//    public List<Seekerexperience> GenerateExperiences(List<Seeker> seekers, int count)
//    {
//        var faker = new Faker<Seekerexperience>()
//            .RuleFor(e => e.ExperienceSeekerUserId, f => f.PickRandom(seekers).SeekerUserId)
//            .RuleFor(e => e.ExperienceRole, f => f.Name.JobTitle())
//            .RuleFor(e => e.ExperienceCompanyName, f => f.Company.CompanyName())
//            .RuleFor(e => e.ExperienceStart, f => DateOnly.FromDateTime(f.Date.Past(10)))
//            .RuleFor(e => e.ExperienceEnd, f => DateOnly.FromDateTime(f.Date.Past(1)));
//        return faker.Generate(count);
//    }
//    public List<Companyfeedback> GenerateCompanyFeedbacks(List<Company> companies, List<Seeker> seekers, int count)
//    {
//        var maxCombinations = seekers.Count * companies.Count;
//        if (count > maxCombinations)
//        {
//            count = maxCombinations;
//        }

//        var faker = new Faker<Companyfeedback>()
//            .RuleFor(cf => cf.FeedbackSeekerUserId, f => f.PickRandom(seekers).SeekerUserId)
//            .RuleFor(cf => cf.FeedbackCompanyUserId, f => f.PickRandom(companies).CompanyUserId)
//            .RuleFor(cf => cf.FeedbackRatingScore, f => f.Random.Int(1, 5))
//            .RuleFor(cf => cf.FeedbackRatingComment, f => f.Lorem.Sentence());

//        var generatedFeedbacks = new List<Companyfeedback>();

//        for (int i = 0; i < count; i++)
//        {
//            Companyfeedback feedback;
//            do
//            {
//                feedback = faker.Generate();
//            } while (generatedFeedbacks.Any(cf => cf.FeedbackSeekerUserId == feedback.FeedbackSeekerUserId && cf.FeedbackCompanyUserId == feedback.FeedbackCompanyUserId));

//            generatedFeedbacks.Add(feedback);
//        }

//        return generatedFeedbacks;
//    }

//    public List<Blogpost> GenerateBlogposts(List<ApplicationUser> users, int count)
//    {
//        var faker = new Faker<Blogpost>()
//            .RuleFor(bp => bp.BlogUserId, f => f.PickRandom(users).Id)
//            .RuleFor(bp => bp.BlogId, f => f.IndexFaker)
//            .RuleFor(bp => bp.BlogTitle, f => f.Lorem.Sentence())
//            .RuleFor(bp => bp.BlogBody, f => f.Lorem.Paragraphs())
//            .RuleFor(bp => bp.BlogUploadDate, f => DateOnly.FromDateTime(f.Date.Past(1)))
//            .RuleFor(bp => bp.BlogNumLikes, f => f.Random.Int(0, 100));

//        return faker.Generate(count);
//    }

//    public List<Blogcomment> GenerateBlogcomments(List<Blogpost> blogposts, List<ApplicationUser> users, int count)
//    {
//        // Generate valid pairs from the existing blogposts and users
//        var validPairs = (from blogpost in blogposts
//                          from user in users
//                          select new { BlogUserId = user.Id, BlogId = blogpost.BlogId }).ToList();

//        // Ensure the count doesn't exceed the number of valid pairs
//        if (count > validPairs.Count)
//        {
//            count = validPairs.Count;
//        }

//        var faker = new Faker<Blogcomment>()
//            .RuleFor(bc => bc.BlogCommentId, f => f.IndexFaker)
//            .RuleFor(bc => bc.CommentBody, f => f.Lorem.Sentence())
//            .RuleFor(bc => bc.CommentUploadDate, f => DateOnly.FromDateTime(f.Date.Past(1)))
//            .RuleFor(bc => bc.CommentUpVote, f => f.Random.Int(0, 50))
//            .RuleFor(bc => bc.CommentDownVote, f => f.Random.Int(0, 50));

//        var generatedComments = new List<Blogcomment>();

//        // Shuffle the validPairs to ensure randomness
//        validPairs = validPairs.OrderBy(x => Guid.NewGuid()).ToList();

//        for (int i = 0; i < count; i++)
//        {
//            var pair = validPairs[i];

//            // Generate a comment using the valid pair
//            var comment = faker.Generate();
//            comment.BlogUserId = pair.BlogUserId;
//            comment.BlogId = pair.BlogId;

//            generatedComments.Add(comment);
//        }

//        return generatedComments;
//    }



//    public async Task SeedDatabase(idbcontext context, ApplicationDbContext applicationDb)
//    {
//        //await GenerateUsers(_userManager, 450);

//        var users = await applicationDb.Users.AsNoTracking()
//            .OrderBy(x => x.Id)
//            .Take(300)
//            .ToListAsync();
//        Console.WriteLine(users.Count);
//        //var companyUsers = users[0..100];
//        //var seekerUsers = users[101..300];
//        var seekers = context.Seekers.ToList();
//        var companies = context.Companies.ToList();

//        //var companies = GenerateCompanies(companyUsers, 90);DONE 
//        //var seekers = GenerateSeekers(seekerUsers, 190); DONE
//       //var jobs = GenerateJobOpenings(companies, 200); DONE
//       //var experiences = GenerateExperiences(seekers, 300); DONE
//       //var feedbacks = GenerateCompanyFeedbacks(companies, seekers, 200); DONE
//       //var blogposts = GenerateBlogposts(users, 50); DONE


//       // var jobs = context.Jobopenings.ToList();
//        //var applications = GenerateApplications(seekers, jobs, 1000);

//        var blogposts = context.Blogposts.ToList();
//        var blogcomments = GenerateBlogcomments(blogposts, users, 200);

//        //context.Companies.AddRange(companies); DONE
//        //context.Seekers.AddRange(seekers); DONE
//        //context.Jobopenings.AddRange(jobs); DONE
//        //context.Seekerexperiences.AddRange(experiences); DONE
//        //context.Companyfeedbacks.AddRange(feedbacks); DONE
//        //context.Blogposts.AddRange(blogposts); DONE

//        //context.Applications.AddRange(applications);
//        context.Blogcomments.AddRange(blogcomments);

//        try
//        {
//            await context.SaveChangesAsync();
//        }
//        catch (DbUpdateException ex)
//        {
//            Console.WriteLine($"Error: {ex.InnerException?.Message}");
//            Console.WriteLine("Database failed \n\n\n\n\n\n");
//            // Handle exceptions and possibly retry
//        }
//        finally { await context.SaveChangesAsync(); }

//    }
//}

