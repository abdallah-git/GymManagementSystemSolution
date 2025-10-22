using GymManagmentBLL;
using GymManagmentBLL.Services.Classes;
using GymManagmentBLL.Services.Interfaces;
using GymMangementDAL.Data.Contexts;
using GymMangementDAL.DataSeed;
using GymMangementDAL.Entities;
using GymMangementDAL.Repositories.Classes;
using GymMangementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementPL

{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymDbcontext>(options =>
            {
                // options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); 
            });


            // builder.Services.AddScoped<GenaricRepository<Member>, GenaricRepository<Member>>(); 
            // builder.Services.AddScoped<GenaricRepository<Trainer>, GenaricRepository<Trainer>>();
            // builder.Services.AddScoped<GenaricRepository<Plan>, GenaricRepository<Plan>>();


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISessionRepository, SessionReposiotry>();
            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));
            builder.Services.AddScoped<IAnalyticservice, Analyticservice>();




            var app = builder.Build();




            #region Dataseed  and migrate database 

         using var scoped = app.Services.CreateScope();

            var dbcontext = scoped.ServiceProvider.GetService<GymDbcontext>();

            var pendindg = dbcontext.Database.GetPendingMigrations(); 

            if (pendindg?.Any() ?? false) 
            {
                dbcontext.Database.Migrate(); 
            }

            GymDbcontextDataSedding.SeedData(dbcontext); 

            #endregion 





            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
           


           
        }
    }
}
