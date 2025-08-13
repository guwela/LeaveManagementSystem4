using LeaveManagementSystem4.Web.Services.LeaveAllocations;
using LeaveManagementSystem4.Web.Services.Email;
using LeaveManagementSystem4.Web.Services.LeaveRequests;
using LeaveManagementSystem4.Web.Services.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddScoped<ILeaveTypesService, LeaveTypesService>();
builder.Services.AddScoped<ILeaveAllocationsService, LeaveAllocationsService>();
builder.Services.AddScoped<ILeaveRequestsService, LeaveRequestsService>(); // Assuming ILeaveRequestsService is defined in LeaveManagementSystem4.Web.Services.LeaveRequests namespace
builder.Services.AddScoped<IPeriodsService, PeriodsService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminSupervisorOnly", policy => {
    policy.RequireRole(Roles.Administrator, Roles.Supervisor);
    });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();
