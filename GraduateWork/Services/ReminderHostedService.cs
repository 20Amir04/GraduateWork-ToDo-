using GraduateWork.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

public class ReminderHostedService : BackgroundService
{
    private int executionCount = 0;
    private readonly ILogger<ReminderHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly CancellationTokenSource _cts;
    private PeriodicTimer? _timer = null;

    public ReminderHostedService(ILogger<ReminderHostedService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _cts = new CancellationTokenSource();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var token = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, stoppingToken).Token;
        var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
        while (await timer.WaitForNextTickAsync(token))
        {
            await DoWork();
        }
    }

    private async Task DoWork()
    {
        using var scope = _serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var reminders = await dbContext
            .Reminders
            .Include(x => x.User)
            .Include(x => x.ToDoItem)
            .Where(x => (x.ReminderDate <= DateTime.Now) && (x.Completed != true))
            .ToListAsync();

        if (!reminders.Any())
        {
            _logger.LogInformation("No reminders found");
            return;
        }

        _logger.LogInformation($"Found {reminders.Count} reminders");

        var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();
        foreach (var reminder in reminders)
        {
            await emailSender.SendEmailAsync(reminder.User.Email, "Reminder", reminder.ToDoItem.Description);
            reminder.Completed = true;
            dbContext.Remove(reminder);
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Reminder Hosted Service is stopping.");

        _cts?.Cancel();

        await base.StopAsync(stoppingToken);
    }


    public void Dispose()
    {
        _timer?.Dispose();
    }

}