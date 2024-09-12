using Food.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Configuration.GetConnectionString("FoodConnection").ToString();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(27);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddTransient<IFoodRepo, FoodRepo>();
builder.Services.AddTransient<IAdmin, Admin>();
builder.Services.AddScoped<IAdmin, Admin>();
builder.Services.AddTransient<IAddtocart, Addtocart>();
builder.Services.AddHttpContextAccessor();

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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
