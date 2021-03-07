using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KCrm.Logic.Services.Projects.Notifications {
    public class SendMailOnProjectCreated : INotification {
        public Guid ProjectId { get; set; }
    }

    public class SendMailOnProjectCreatedHandler : INotificationHandler<SendMailOnProjectCreated> {
        private readonly ILogger _logger;

        public SendMailOnProjectCreatedHandler(ILogger<SendMailOnProjectCreatedHandler> logger) {
            _logger = logger;
        }

        public Task Handle(SendMailOnProjectCreated notification, CancellationToken cancellationToken) {

            Task.Run (() => {
                _logger.LogInformation ($"Project with ID Has been created: {notification.ProjectId}");
                System.Threading.Thread.Sleep (5000);
                _logger.LogInformation ($"Project notification processed");
            }, cancellationToken);
            return Task.CompletedTask;
        }
    }
}
