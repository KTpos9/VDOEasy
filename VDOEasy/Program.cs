using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using VDOEasy.Data;
using VDOEasy.Data.Repositories;
using VDOEasy.Data.Repositories.Interfaces;
using VDOEasy.Models;
using VDOEasy.Validatiors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddScoped<IValidator<HomeViewModel>, MemberValidator>();

// Add services DI.
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMovieTypeRepository, MovieTypeRepository>();
builder.Services.AddScoped<IMemberTypeRepository, MemberTypeRepository>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();

builder.Services.Configure<FormOptions>(option =>
{
    option.ValueCountLimit = 10240;
    option.MultipartBodyLengthLimit = 52428800;
});
// Add DbContext
builder.Services.AddDbContext<VdoeasyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
