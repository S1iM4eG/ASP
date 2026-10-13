using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ContosoUniversity1Context") ?? throw new InvalidOperationException("Connection string 'ContosoUniversity1Context' not found.");

builder.Services.AddDbContext<ContosoUniversity1Context>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

CreateDbIfNotExists(app);

app.Run();

static void CreateDbIfNotExists(IHost host)
{
    //Scope - создание области видимости для получения сервисов из контейнера зависимостей
    using (IServiceScope scope = host.Services.CreateScope())
    {
        IServiceProvider provider = scope.ServiceProvider;
    try
        {
            ContosoUniversity1Context context = provider.GetRequiredService<ContosoUniversity1Context>();
        }
        catch (Exception ex)
        {
            ILogger<Program> logger = provider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Ошибка инициализации Базы Данных.");
        }
    }
}
