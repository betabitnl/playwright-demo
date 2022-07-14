using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using PlaywrightTest1.Helpers;
using PlaywrightTest1.PageObjects;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PlaywrightTest1.Init;

public class PlaywrightServiceProvider : IAsyncDisposable
{
    private ServiceProvider? _serviceProvider;

    public async Task<T> GetRequiredServiceAsync<T>()
    {
        if (_serviceProvider == null)
        {
            await SetupAsync();
        }

        return _serviceProvider.GetRequiredService<T>();
    }

    private async Task SetupAsync()
    {
        var path = Path.GetDirectoryName(typeof(PlaywrightServiceProvider).Assembly.Location);
        var configuration = new ConfigurationBuilder()
           .SetBasePath(path)
           .AddJsonFile("appsettings.json")
           .Build();

        var playwright = await Playwright.CreateAsync();

        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = Convert.ToBoolean(configuration["Headless"]),
        });

        var stateFileExists = File.Exists("state.json");
        var browserContext = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            StorageStatePath = stateFileExists ? "state.json" : null,
            ViewportSize = new ViewportSize() { Width = 1920, Height = 1080 }
        });

        var page = await browserContext.NewPageAsync();

        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddSingleton(browser);
        services.AddSingleton(browserContext);
        services.AddSingleton(page);
        services.AddScoped(x => new UrlService(page, configuration["BaseUrl"]));
        services.AddScoped<ScreenshotTestResult>();
        services.AddScoped<MenuPage>();
        services.AddScoped<MicrosoftPage>();

        _serviceProvider = services.BuildServiceProvider();
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (_serviceProvider is not null)
        {
            var browser = await GetRequiredServiceAsync<IBrowser>();
            await browser.DisposeAsync();
            await _serviceProvider.DisposeAsync().ConfigureAwait(false);
        }

        _serviceProvider = null;
    }
}
