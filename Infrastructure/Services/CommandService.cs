﻿using PersEmails.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using PersEmails.Application.Interfaces;

namespace PersEmails.Infrastructure.Services
{
    public class CommandService : ICommandService
    {
        private IServiceProvider _serviceProvider;

        public CommandService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public int Execute<TCommand>(TCommand command) where TCommand : ICommand
        {
            var validator = _serviceProvider.GetService<IValidator<TCommand>>();
            if (validator != null && !validator.IsValid(command))
                return 0;
            
            var dbContext = _serviceProvider.GetService<IAppContext>();
            return command.Execute(dbContext);
        }

        public async Task<int> ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommandAsync
        {
            var validator = _serviceProvider.GetService<IValidatorAsync<TCommand>>();
            if (validator != null && !(await validator.IsValid(command)))
                return 0;

            var dbContext = _serviceProvider.GetService<IAppContext>();
            return await command.ExecuteAsync(dbContext, cancellationToken);
        }
    }
}
