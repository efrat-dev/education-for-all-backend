using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project.DataContext;
using Project.Repositories.Repositories;
using Project.Services.Services;
using System.Text;

// Required namespaces for authentication, swagger, EF, and custom services
var builder = WebApplication.CreateBuilder(args);

// Define a CORS policy name
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Configure CORS to allow requests from the frontend running at localhost:3000
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000",
                              "https://education-for-all.onrender.com").
                          AllowAnyMethod().
                          AllowAnyHeader().
                          AllowCredentials().
                          WithExposedHeaders("Content-Disposition", "Access-Control-Allow-Origin");
                      });

});

// Add support for controllers and minimal APIs
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register Swagger for API documentation
builder.Services.AddSwaggerGen();

// Register custom application services (extension method)
builder.Services.AddServices();

// Configure dependency injection for database context
builder.Services.AddDbContext<IContext, ApplicationDbContext>();


// Configure JWT authentication and token validation parameters
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(option =>
              option.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
                  ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience"),
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:Key")))

              });

// Add JWT support to Swagger UI
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] { }
        }
    });
});

// Build the application
var app = builder.Build();

// Enable Swagger UI only in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable HTTPS redirection and middleware pipeline
app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

// Map controller routes
app.MapControllers();

// Start the application
app.Run();