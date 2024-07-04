var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<CustomResourceFilterAttribute>();
builder.Services.AddSingleton<CustomActionFilterAttribute>();
builder.Services.AddSingleton<CustomExceptionFilterAttribute>();
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.AddService<CustomResourceFilterAttribute>();
    options.Filters.AddService<CustomActionFilterAttribute>();
    options.Filters.AddService<CustomExceptionFilterAttribute>();
});
builder.Services.AddSingleton<ITempDataDictionaryFactory, TempDataDictionaryFactory>();
builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

builder.Services.AddLogging();
builder.Services.AddTransient<IApiService, ApiService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IHttpService, HttpService>();


builder.Services.AddSession();
builder.Services.AddHttpClient("ShopApi", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7285/api/");
    httpClient.Timeout = Timeout.InfiniteTimeSpan;
}).SetHandlerLifetime(TimeSpan.FromMinutes(10));

builder.Services.AddHttpContextAccessor();

// TODO: Move applicable cookie settings to appsettings.json
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(options =>
  {
      options.Cookie.Name = ".AspNet.SharedCookie";
      options.Cookie.SameSite = SameSiteMode.Lax;
      options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
      options.Cookie.Path = "/";
      options.Cookie.HttpOnly = true;
      options.Cookie.IsEssential = true;
      options.SlidingExpiration = true;
      options.LoginPath = "/Auth";
      options.LogoutPath = "/Auth";
  });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/Index");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();


//app.Use(async (context, next) =>
//{
//    var endpointFilter = new CustomEndpointFilter(next, context.RequestServices.GetRequiredService<ILogger<CustomEndpointFilter>>());
//    await endpointFilter.InvokeAsync(context);
//});

app.UseRouting();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
