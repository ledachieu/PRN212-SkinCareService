using DAO;
using Microsoft.EntityFrameworkCore;
using Models;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SkinServiceContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDistributedMemoryCache(); // Để sử dụng bộ nhớ đệm cho session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn session
    options.Cookie.HttpOnly = true; // Chỉ có thể truy cập session qua HTTP, không qua JavaScript
    options.Cookie.IsEssential = true; // Đảm bảo session hoạt động ngay cả khi không bật cookie
});
builder.Services.AddHttpContextAccessor();
// Đăng ký DAO
builder.Services.AddScoped<ServiceDAO>();
builder.Services.AddScoped<DashboardDAO>();
builder.Services.AddScoped<TherapistDAO>();
builder.Services.AddScoped<BlogDAO>();
builder.Services.AddScoped<UserDAO>();
builder.Services.AddScoped<BookingDAO>();
builder.Services.AddScoped<WorkingScheduleDAO>();
builder.Services.AddScoped<FeedbackDAO>();
builder.Services.AddScoped<ServiceResultDAO>();
// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
