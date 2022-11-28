using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using SamplePoc.Host.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddDomain()
    .AddApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
