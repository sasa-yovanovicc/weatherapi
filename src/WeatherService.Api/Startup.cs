public class Startup
{
    private readonly IHostEnvironment _env;

    public Startup(IConfiguration configuration, IHostEnvironment env)
    {
        Configuration = configuration;
        _env = env;
    }

    public IConfiguration Configuration { get; }

    public void SharedConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
    }

    public void ConfigureDevelopmentServices(IServiceCollection services)
    {
        SharedConfigureServices(services);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        SharedConfigureServices(services);
    }

    public void SharedConfigure(IApplicationBuilder app)
    {
        app.UseHttpsRedirection();
        // app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }

    public void ConfigureDevelopment(IApplicationBuilder app)
    {
        SharedConfigure(app);
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseHsts();
        SharedConfigure(app);
    }
}