using AutoMapper;
using CoreMvcTemplate.Configurations;
using CoreMvcTemplate.Entities;
using CoreMvcTemplate.Entities.Data;
using CoreMvcTemplate.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//Identity Configuration
builder.Services.AddIdentity<User, Role>(options => { }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


builder.Services.AddScoped<IdentityService>();
builder.Services.AddScoped<ImageService>();


builder.Services.AddAutoMapper(options =>
{
    options.AddProfile<AutoMapperProfile>(); 
});


var app = builder.Build();
//test
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	var identityService = services.GetRequiredService<IdentityService>();

	await identityService.EnsureAdminUser();
	identityService.EnsureLandingPageExist();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Route config
app.UseEndpoints(endpoints =>
{
    endpoints.ConfigureRoutes();
});

app.Run();
