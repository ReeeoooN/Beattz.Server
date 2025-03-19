using AutoMapper;
using Beattz.Server.Models.Persistance;
using Beattz.Server.Models.Persistance.Repository;
using Beattz.Server.Models.Services;
using Beattz.Server.Models.Services.Mapping;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Console;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TracksDbContext>();
builder.Services.AddSingleton<ILog, ConsoleLog>();
builder.Services.AddSingleton(x =>
{
    var mapperConfig = new MapperConfiguration(cfg =>
        cfg.AddProfile<MappingProfile>());
    return mapperConfig.CreateMapper();
});
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ITrackRepository, FakeTracksRepository>();
builder.Services.AddScoped<ITrackService, TrackService>();
builder.Services.AddScoped<IUploadTrackService, TrackImportService>();
builder.Services.AddScoped<IActualizeTrackList, FileSyncService>();
builder.Services.AddHostedService<CrontabSync>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();