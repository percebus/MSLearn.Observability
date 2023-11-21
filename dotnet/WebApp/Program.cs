// <copyright file="Program.cs" company="JCystems">
// Licensed under the MIT license. See LICENSE file in the samples root for full license information.
// </copyright>

using JCystems.MSLearn.Observability.WebApp.Configuration;

var oWebApplicationBuilder = WebApplication.CreateBuilder(args);

oWebApplicationBuilder.Services.ConfigureServices(
    oWebApplicationBuilder.Configuration,
    oWebApplicationBuilder.Host);

var oWebApplication = oWebApplicationBuilder.Build();

// Configure the HTTP request pipeline.
if (oWebApplication.Environment.IsDevelopment())
{
    oWebApplication.UseSwagger();
    oWebApplication.UseSwaggerUI();
}

oWebApplication.UseHttpsRedirection();

oWebApplication.UseAuthorization();

oWebApplication.MapControllers();

oWebApplication.Run();
