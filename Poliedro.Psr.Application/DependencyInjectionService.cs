using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Poliedro.Psr.Application.Markers.Commands;
using Poliedro.Psr.Application.Markers.Mappers;
using Poliedro.Psr.Application.Markers.Validators;
using System.Reflection;

namespace Poliedro.Psr.Application;

public static class DependencyInjectionService
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        #region Mappers
        var mapper = new MapperConfiguration(config =>
        {
            config.AddProfile(new MarkerMapper());

        });
        services.AddSingleton(mapper.CreateMapper());
        #endregion

        #region Validators
        services.AddScoped<IValidator<AddMarkerToReaderCommand>, AddMarkerToReaderCommandValidator>();
        #endregion


        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}