﻿using System.Reflection;
using Employees.Application.Configuration.Validation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Employees.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}