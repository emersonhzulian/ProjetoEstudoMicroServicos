using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateAsyncScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }

        }

        private static void SeedData(AppDbContext context)
        {
            if(!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding data");
                
                context.Platforms.AddRange(
                    new Platform() {Name = "Dot net", Publisher = "Microsoft", Cost = "Free", Description = "A" },
                    new Platform() {Name = "Sql", Publisher = "Microsoft", Cost = "Free", Description = "B" },
                    new Platform() {Name = "X", Publisher = "XX", Cost = "XXX", Description = "C" },
                    new Platform() {Name = "Y", Publisher = "YY", Cost = "YYY", Description = "D" }
                );

                Console.WriteLine("--> Finished seeding data");

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}