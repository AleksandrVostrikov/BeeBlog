using BeeBlog.Web.Data;
using BeeBlog.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddScoped<IPostRepos, PostRepos>();
builder.Services.AddScoped<IImageRepos, ImageRepos>();
builder.Services.AddScoped<ITagRepos, TagRepos>();
builder.Services.AddScoped<ILikesRepos, LikesRepos>();
builder.Services.AddScoped<IBlogPostCommentRepos, BlogPostCommentRepos>();
builder.Services.AddScoped<IUsersRepos, UsersRepos>();

//dataBases
builder.Services.AddDbContext<BeeBlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BeeBlogDbConnectionString")));

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BeeBlogAythConnectionString")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath= "/AccessDeniedPath";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The  default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
