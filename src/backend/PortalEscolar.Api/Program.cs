using PortalEscolar.Api.Filters;
using PortalEscolar.Application;
using PortalEscolar.Application.Services.Mapping;
using PortalEscolar.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfig());
}).CreateMapper());


builder.Services.AddRouting(option => option.LowercaseUrls = true);

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionsFilter)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();

#pragma warning disable CA1050, S3903, S1118
public partial class Program { }
#pragma warning restore CA1050, S3903, S1118