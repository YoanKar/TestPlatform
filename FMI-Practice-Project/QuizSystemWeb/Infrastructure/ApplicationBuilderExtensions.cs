namespace QuizSystemWeb.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using QuizSystemWeb.Data;
    using QuizSystemWeb.Data.Entities;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using static Areas.Admin.AdministratorConstants;

    public static class ApplicationBuilderExtensions
    {
        

        public static IApplicationBuilder PrepareDatabase(
           this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);
            SeedAnswerSignificances(services);
            SeedQuestionTypes(services);
            SeedAdministrator(services);
            SeedUser(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            data.Database.Migrate();
        }

        private static void SeedAnswerSignificances(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            if (data.AnswerSignificances.Any())
            {
                return;
            }

            data.AnswerSignificances.AddRange(new[]
             {
                new AnswerSignificance()
                {
                    Name="Yes",
                    Value=true
                },
                new AnswerSignificance()
                {
                    Name="No",
                    Value=false
                }
              });

            data.SaveChanges();

        }

        private static void SeedQuestionTypes(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            if (data.QuestionsTypes.Any())
            {
                return;
            }

            data.QuestionsTypes.AddRange(new[]
             {
                new QuestionType()
                {
                    TypeName="Closed"
                },
                new QuestionType()
                {
                    TypeName="Opened"
                }
              });

            data.SaveChanges();

        }

            private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var adminRole = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(adminRole);

                    const string adminEmail = "admin@admin.com";

                    const string adminPassword = "123456";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FullName = "Admin"

                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, adminRole.Name);

                })
                .GetAwaiter()
                .GetResult();
        }


        private static void SeedUser(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();

            Task
              .Run(async () =>
              {
                  const string userEmail = "test@test.com";

                  if (await userManager.FindByEmailAsync(userEmail) != null)
                  {
                      return;
                  }

                  const string userPassword = "123456";

                  var user = new User
                  {
                      Email = userEmail,
                      UserName = userEmail,
                      FullName = "TestUser"

                  };

                  await userManager.CreateAsync(user, userPassword);


              })
              .GetAwaiter()
              .GetResult();
        }
    }
}
