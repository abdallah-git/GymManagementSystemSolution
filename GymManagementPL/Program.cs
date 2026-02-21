using GymManagmentBLL;
using GymManagmentBLL.Services.AttachmentService;
using GymManagmentBLL.Services.Classes;
using GymManagmentBLL.Services.Interfaces;
using GymMangementDAL.Data.Contexts;
using GymMangementDAL.DataSeed;
using GymMangementDAL.Entities;
using GymMangementDAL.Repositories.Classes;
using GymMangementDAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
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
            builder.Services.AddScoped<IMembershipRepository, MembershipRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();

            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));
            builder.Services.AddScoped<IAnalyticservice, Analyticservice>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<ITrainerService,TranierService>();
            builder.Services.AddScoped<IPlanService, PlanServices>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            builder.Services.AddScoped<MembershipService, MembershipService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Config =>
            {

                Config.Password.RequiredLength = 6;
                Config.Password.RequireLowercase = true;
                Config.Password.RequireUppercase = true;
                Config.User.RequireUniqueEmail = true;


            }).AddEntityFrameworkStores<GymDbcontext>();

            builder.Services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/Account/Login";
                opt.AccessDeniedPath = "/Account/AccessDenied";
            });
            builder.Services.AddScoped<IAccountService, AccountService>(); 


            //builder.Services.AddIdentityCore<ApplicationUser>()
            //    .AddEntityFrameworkStores<GymDbcontext>();



            var app = builder.Build();




            #region Dataseed  and migrate database 

         using var scoped = app.Services.CreateScope();

            var dbcontext = scoped.ServiceProvider.GetRequiredService<GymDbcontext>();
            var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var pendindg = dbcontext.Database.GetPendingMigrations(); 

            if (pendindg?.Any() ?? false) 
            {
                dbcontext.Database.Migrate(); 
            }

            GymDbcontextDataSedding.SeedData(dbcontext);

            IdentityDbcontextSeeding.SeedData(roleManager, userManager);

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}")
                .WithStaticAssets();

            app.Run();
           


           
        }
    }
}
