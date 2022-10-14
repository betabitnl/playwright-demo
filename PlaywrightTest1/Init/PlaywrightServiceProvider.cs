using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTest1.Helpers;
using PlaywrightTest1.PageObjects;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PlaywrightTest1.Init;

public class PlaywrightServiceProvider : IWorkerService
{
    private readonly ServiceProvider _serviceProvider;
    private IServiceScope _scope;
    private IServiceProvider _scopedServiceProvider => _scope.ServiceProvider;

    public PlaywrightServiceProvider()
    {
        string? path = Path.GetDirectoryName(typeof(PlaywrightServiceProvider).Assembly.Location);
        IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(path)
           .AddJsonFile("appsettings.json")
           .Build();

        _serviceProvider = new ServiceCollection()
            .AddSingleton<IConfiguration>(configuration)
            .AddScoped<UrlService>()
            .AddScoped<ScreenshotTestResult>()
            .AddScoped<MenuPage>()
            .AddScoped<MicrosoftPage>()
            .BuildServiceProvider();

        _scope = _serviceProvider.CreateScope();
    }

    public T IPageObjectGetPageObject<T>(IPage page) where T : IPageObject
    {
        T pageObject = _scopedServiceProvider.GetRequiredService<T>();
        pageObject.SetPage(page);
        return pageObject;
    }

    public T GetRequiredService<T>() where T : notnull
    {
        if (typeof(T).IsAssignableFrom(typeof(IPageObject)))
        {
            throw new NotSupportedException("Use IPageObjectGetPageObject method when resolving IPageObjects");
        }
        return GetRequiredServiceInternal<T>();
    }

    private T GetRequiredServiceInternal<T>() where T : notnull
    {
        return _scopedServiceProvider.GetRequiredService<T>();
    }

    public static Task<PlaywrightServiceProvider> Register(WorkerAwareTest test)
    {
        return test.RegisterService("ServiceProvider", () => Task.FromResult(new PlaywrightServiceProvider()));
    }

    public async Task DisposeAsync()
    {
        _scope.Dispose();
        await _serviceProvider.DisposeAsync().ConfigureAwait(false);
    }

    public Task ResetAsync()
    {
        _scope.Dispose();
        _scope = _serviceProvider.CreateScope();

        return Task.CompletedTask;
    }
}
