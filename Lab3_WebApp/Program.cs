using Lab3_WebApp.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// ������������ �'������� � ����� ����� SQLite
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=app.db";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "Google";
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => // �������� cookie ������ �����
{
    options.LoginPath = "/account/login";
})
.AddCookie("Identity.External") // <-- ������: �������� ���������� cookie
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Google:ClientId"];
    options.ClientSecret = builder.Configuration["Google:ClientSecret"];
    // <-- ������: ������ Google ��������������� ���������� cookie ���� �����
    options.SignInScheme = "Identity.External";
});
/*

 options.Events.OnTicketReceived = async context =>
    {
        if (context.Principal == null)
        {
            return;
        }

        var externalId = context.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(externalId))
        {
            return;
        }
        // �������� ������ �� ������ DbContext
        await using (var scope = context.HttpContext.RequestServices.CreateAsyncScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // 4. ������ ����������� � ����� ���
            var userExists = await dbContext.Users.AsNoTracking()
                .AnyAsync(u => u.ExternalId == externalId);

            // 5. ���� ����������� ����� � ���, ��������������� ���� �� ���������
            if (!userExists)
            {
                context.Response.Redirect("/Account/Register");
                context.HandleResponse(); // ��������� �������� �������
            }
        }
    };

*/
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
