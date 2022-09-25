using System.Text;
using Api.Data;
using Api.Data.SeedingData;
using Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Api.Interfaces;
using Api.Repositories;
using Api.Services;
using Api.Helpers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICloudStorage,CloudStorageRepo>();
builder.Services.AddScoped<IShoppingCart,ShoppingCartRepository>();
builder.Services.AddScoped<ICategory,CategoryRepository>();
builder.Services.AddScoped<IBill,BillRepository>();
builder.Services.AddScoped<ISale,SalesRepository>();
builder.Services.AddScoped<IMember,MemebersRepository>();
builder.Services.AddScoped<IProduct,ProductRepository>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddControllers().AddNewtonsoftJson(options =>{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    
    string connStr;
    if (env == "Development"){
        connStr = builder.Configuration.GetConnectionString("DefaultConnections");     
    } else{
                    // Use connection string provided at runtime by Heroku.
                    var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

                    // Parse connection URL to connection string for Npgsql
                    connUrl = connUrl.Replace("postgres://", string.Empty);
                    var pgUserPass = connUrl.Split("@")[0];
                    var pgHostPortDb = connUrl.Split("@")[1];
                    var pgHostPort = pgHostPortDb.Split("/")[0];
                    var pgDb = pgHostPortDb.Split("/")[1];
                    var pgUser = pgUserPass.Split(":")[0];
                    var pgPass = pgUserPass.Split(":")[1];
                    var pgHost = pgHostPort.Split(":")[0];
                    var pgPort = pgHostPort.Split(":")[1];

                 connStr = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Require;Trust Server Certificate=true";
                }
                 options.UseNpgsql(connStr);
});
builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options => {
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireDigit = false;

}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>{
  options.TokenLifespan = TimeSpan.FromHours(2);
});
builder.Services.AddAuthentication(optins => {
  optins.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  optins.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
        ValidateIssuer = false,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AuthSetting:Audience"],
        ValidIssuer = builder.Configuration["AuthSettings:Issuer"],
        RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])
                    ),
                       
                    ValidateIssuerSigningKey = true
    };
     options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                
                        return Task.CompletedTask;
                    }
                };
});
 builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

var app = builder.Build();

// seeding data
Configure(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x=> x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
app.UseHttpsRedirection();
 app.UseRouting();
 
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.UseEndpoints(endPoints =>{
    endPoints.MapControllerRoute(
        name : "api",
        pattern: "{controller}/{action}/{id?}"
    );
    endPoints.MapFallbackToController("Index", "FallBack");
});



 app.Run();

async void Configure(WebApplication host){
   using var scope = host.Services.CreateScope();
   var services = scope.ServiceProvider;
   try{
    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
     dbContext.Database.Migrate();
    await Seed.SeedingRoles(roleManager);
    await Seed.SeedingCategories(dbContext);
    await Seed.SeedingAdmin(userManager,roleManager);
     
   }catch(Exception e){
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(e,"ann error accured while migration");
   }
    

    /*
    mysql:
    connection:"server=localhost;user=root;password='';database=jostore"
     options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnections"),
    new MySqlServerVersion(new Version(1,0)));
    
     */
    
}