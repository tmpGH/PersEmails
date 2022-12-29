﻿using PersEmails.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using PersEmails.Application.Interfaces;

namespace PersEmails.Infrastructure.Services
{
    public class CommandService : ICommandService
    {
        private IServiceProvider serviceProvider;

        public CommandService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public TCommand GetCommand<TCommand>() where TCommand : ICommand
        {
            return serviceProvider.GetService<TCommand>();
        }

        public TCommand GetAsyncCommand<TCommand>() where TCommand : ICommandAsync
        {
            return serviceProvider.GetService<TCommand>();
        }

        public int Execute(ICommand command)
        {
            var dbContext = serviceProvider.GetService<IAppContext>();
            return command.Execute(dbContext);
        }

        public Task<int> ExecuteAsync(ICommandAsync command, CancellationToken cancellationToken = default)
        {
            var dbContext = serviceProvider.GetService<IAppContext>();
            return command.ExecuteAsync(dbContext, cancellationToken);
        }
    }
}
