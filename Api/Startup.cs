using Api.Commons;
using Api.Controllers;
using Api.IServices;
using Api.Repositories;
using Api.Security;
using Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

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
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])
                                )
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsSuperAdmin", policy => policy.RequireClaim("IsAdmin", "true"));
                options.AddPolicy("TanentUser", policy => policy.Requirements.Add(new TanentUserRequirment()));
                foreach (var policyName in PolicyConstants.PolicyConstantList)
                {
                    options.AddPolicy(policyName,
                        policy => policy.Requirements.Add(new PermissionRequirement(policyName)));
                }
            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEntityId, DocumentDbGuid>();
            services.AddScoped<IDateTime, MachineDateTime>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IDbContext>(x => new DbContext("mongodb://localhost:27017", "EducationSystem"));
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
            services.AddScoped<IReactionRepository, ReactionRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IUserInteractionRepository, UserInteractionRepositoy>();
            services.AddScoped<IRequestUserService, RequestUserService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
