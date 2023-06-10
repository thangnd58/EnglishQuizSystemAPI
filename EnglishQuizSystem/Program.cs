using EnglishQuizSystem.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Text;

namespace EnglishQuizSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddOData(option => option.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100).AddRouteComponents("odata", GetEdmModel()));
            builder.Services.AddCors(
                    p => p.AddPolicy("MyCors", build =>
                    {
                        build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    })
                );

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true
                };
            });
            builder.Services.AddAuthorization();

            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddDbContext<EnglishQuizSystemContext>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthorization();
            app.UseAuthentication();
            
            app.UseCors("MyCors");
            app.MapControllers();//

            app.Run();
        }

        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<User>("Users");
            builder.EntitySet<Role>("Roles");
			builder.EntitySet<Quiz>("Quizs");
			builder.EntitySet<Question>("Questions");
			builder.EntitySet<Answer>("Answers");
			return builder.GetEdmModel();
        }
    }
}