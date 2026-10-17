// Modern .NET, Chapter 6, Section 6.14
using System.Threading.Channels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton(Channel.CreateBounded<Job>(new BoundedChannelOptions(50)
{
    FullMode = BoundedChannelFullMode.Wait,
    SingleReader = false
}));
builder.Services.AddHostedService<JobProducer>();
builder.Services.AddHostedService<JobConsumer>();

await builder.Build().RunAsync();

public sealed record Job(int Id, string Payload);

public sealed class JobProducer(Channel<Job> channel, ILogger<JobProducer> logger,
    IHostApplicationLifetime lifetime) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            for (var i = 1; i <= 20 && !stoppingToken.IsCancellationRequested; i++)
            {
                await channel.Writer.WriteAsync(new Job(i, $"payload-{i}"), stoppingToken);
                logger.LogInformation("Enqueued job {JobId}", i);
                await Task.Delay(50, stoppingToken);
            }
        }
        catch (OperationCanceledException) { /* shutting down */ }
        finally
        {
            channel.Writer.TryComplete();       // tell consumers no more work is coming
        }
    }
}

public sealed class JobConsumer(Channel<Job> channel, ILogger<JobConsumer> logger,
    IHostApplicationLifetime lifetime) : BackgroundService
{
    private const int Workers = 3;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var workers = Enumerable.Range(1, Workers)
            .Select(id => WorkerLoopAsync(id, stoppingToken));

        await Task.WhenAll(workers);

        logger.LogInformation("All jobs processed. Stopping host.");
        lifetime.StopApplication();             // demo only: exit when the queue drains
    }

    private async Task WorkerLoopAsync(int workerId, CancellationToken ct)
    {
        try
        {
            await foreach (var job in channel.Reader.ReadAllAsync(ct))
            {
                try
                {
                    await Task.Delay(200, ct);  // pretend work
                    logger.LogInformation("Worker {Worker} finished job {JobId}", workerId, job.Id);
                }
                catch (Exception ex) when (ex is not OperationCanceledException)
                {
                    logger.LogError(ex, "Job {JobId} failed", job.Id);   // one bad job must not kill the worker
                }
            }
        }
        catch (OperationCanceledException) { /* shutting down */ }
    }
}
