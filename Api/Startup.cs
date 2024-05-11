using Api.Commons;
using Api.DTOs;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers().AddNewtonsoftJson();
           

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
            services.AddAuthorization();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEntityId, DocumentDbGuid>();
            services.AddScoped<IDateTime, MachineDateTime>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INotificationService, NotificationService>();

            string connectionString = Configuration.GetSection("MongoSettings").GetSection("Connection").Value;
            string database = Configuration.GetSection("MongoSettings").GetSection("DatabaseName").Value;


            services.AddScoped<IDbContext>(x => new DbContext(connectionString, database));
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
            services.AddScoped<IUserActivityRepository, UserActivityRepositoy>();
            services.AddScoped<IExamRepository, ExamRepository>();
            services.AddScoped<IRequestUserService, RequestUserService>();
            services.AddScoped<IPasswordMetadataRepository, PasswordMetadataRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IServiceSecretValidationService, ServiceSecretValidationService>();


            LiveUpdateConfigDTO liveUpdateConfig = new LiveUpdateConfigDTO();
            Configuration.GetSection("LiveUpdate").Bind(liveUpdateConfig);
            services.AddSingleton(liveUpdateConfig);
        }

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
