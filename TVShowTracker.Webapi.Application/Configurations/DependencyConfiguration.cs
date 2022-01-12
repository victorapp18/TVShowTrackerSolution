using FluentValidation;
using TVShowTracker.Webapi.Application.Services;
using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using TVShowTracker.Webapi.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Framework.Data.Abstractions.DbCotexts;
using Framework.Data.Abstractions.UnitOfWork;
using Framework.Data.Concrete.UnitOfWork;
using Framework.Message.Concrete;
using TVShowTracker.Webapi.Infrastructure.Repositories;
using TVShowTracker.Webapi.Application.Queries.Identity;
using TVShowTracker.Webapi.Domain.Aggregates.ChannelAggregate;
using TVShowTracker.Webapi.Application.Queries.Channel;
using TVShowTracker.Webapi.Application.Queries.Program;
using TVShowTracker.Webapi.Domain.Aggregates.ProgramAggregate;

namespace TVShowTracker.Webapi.Application.Configurations
{
    public static class DependencyConfiguration
    {
        public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            string conncetion = ConnectorService.GetMySqlConnectionString(configuration);

            services.AddMediatR(Assembly.GetExecutingAssembly());
            AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
                           .ForEach(r => services.AddScoped(r.InterfaceType, r.ValidatorType));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestBehavior<,>));

            services.AddDbContext<NBDbContext>(options => options.UseMySQL(conncetion));

            services.AddScoped<MySqlDbContext, NBDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>(ctx =>
            {
                IMediator mediator = ctx.GetRequiredService<IMediator>();
                NBDbContext context = ctx.GetRequiredService<NBDbContext>();

                return new UnitOfWork(context, mediator);
            });
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddScoped<IIdentityQuery, IdentityQuery>();
            services.AddScoped<IChannelRepository, ChannelRepository>();
            services.AddScoped<IChannelQuery, ChannelQuery>();
            services.AddScoped<IProgramRepository, ProgramRepository>();
            services.AddScoped<IProgramQuery, ProgramQuery>();
            services.AddSingleton(configuration);
            
        }
    }
}

