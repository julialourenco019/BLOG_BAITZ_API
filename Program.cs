using BAITZ_BLOG_API.Context;
using BAITZ_BLOG_API.Interfaces;
using BAITZ_BLOG_API.Repository;
using BAITZ_BLOG_API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "Bearer",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});


/*builder.Services.AddDbContext<ApplicationDataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));*/
builder.Services.AddDbContext<ApplicationDataContext>(options => {
    // Verifica se está no ambiente do Render
    var isRender = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("RENDER"));
    
    if (isRender) {
        // Caminho persistente no Render
        options.UseSqlite("Data Source=/var/lib/data/BAITZ_BLOG_DATABASE.db");
    } else {
        // Ambiente de desenvolvimento
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
});

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddCors(options => {
    options.AddPolicy(name: "MyPolicy",
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:4200",
                "https://seu-frontend-no-render.onrender.com" // Adicione a URL do seu frontend no Render
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

//var key = Encoding.ASCII.GetBytes(BAITZ_BLOG_API.Key.Secret);
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? 
                BAITZ_BLOG_API.Key.Secret;
builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)), // Correção aqui
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

/*builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(jwtSecret),
        ValidateIssuer = false,
        ValidateAudience = false

    };
});*/



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
     app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDataContext>();
    db.Database.Migrate();
}
app.UseCors("MyPolicy");
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/health", () => "Healthy");


// Obtendo a porta definida pelo Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000"; // Porta padrão caso PORT não esteja definida
app.Urls.Add($"http://0.0.0.0:{port}");
app.Urls.Add($"http://0.0.0.0:443");

app.Run();

