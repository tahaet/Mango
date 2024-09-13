using Mango.Services.EmailAPI.Data;
using Mango.Services.EmailAPI.Messaging;
using Mango.Services.ShoppingCartAPI.Extension;
using Mango.Sevices.EmailAPI.Service;
using Mango.Utility.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});


builder.Services.AddScoped<IEmailSender, EmailSender>();

//var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
//optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddSingleton<EmailService>(sp =>
{
    var options = sp.GetRequiredService<DbContextOptions<AppDbContext>>();
    return new EmailService(options, sp);
}); 
builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ApplyMigration();
app.UseAzureServiceBusConsumer();
app.Run();
void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}