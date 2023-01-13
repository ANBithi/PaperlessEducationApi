using Api.IServices;
using Api.Repositories;
using Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IDbContext>(x => new DbContext("mongodb://localhost:27017","EducationSystem"));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDepartmentRepository, DepertmentRepository>();
            services.AddScoped<IFacultyRepository, FacultyRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IInstituteRepository, InstituteRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IResultRepository, ResultRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()

            );
            app.UseCors();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
