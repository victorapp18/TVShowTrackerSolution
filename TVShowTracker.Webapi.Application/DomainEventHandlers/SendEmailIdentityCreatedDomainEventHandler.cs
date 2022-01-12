using TVShowTracker.Webapi.Application.Options;
using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using TVShowTracker.Webapi.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace TVShowTracker.Webapi.Application.DomainEventHandlers
{
    public class SendEmailIdentityCreatedDomainEventHandler : INotificationHandler<IdentityCreatedDomainEvent>
    {
        private SendGridOption SendGridOptions { get; }

        public SendEmailIdentityCreatedDomainEventHandler(IOptions<SendGridOption> sendGridOptions) 
        {
            SendGridOptions = sendGridOptions.Value;
        }

        public async Task Handle(IdentityCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            Identity identity = notification.Identity;
            string password = notification.Password;

            SendGridClient sendGridClient = new SendGridClient(SendGridOptions.ApiKey);
            SendGridMessage message = new SendGridMessage();
            TemplateItem template = notification.Language == null || notification.Language == "" ? SendGridOptions.Templates.TrackerGeneratePasswordIntegrationEventPT :
             notification.Language.ToUpper() == "EN" ? SendGridOptions.Templates.TrackerGeneratePasswordIntegrationEventEN :
             notification.Language.ToUpper() == "ES" ? SendGridOptions.Templates.TrackerGeneratePasswordIntegrationEventES :
             notification.Language.ToUpper() == "IT" ? SendGridOptions.Templates.TrackerGeneratePasswordIntegrationEventIT :
             notification.Language.ToUpper() == "FR" ? SendGridOptions.Templates.TrackerGeneratePasswordIntegrationEventFR :
             SendGridOptions.Templates.TrackerGeneratePasswordIntegrationEventPT;

            message.SetFrom(SendGridOptions.From.Email, SendGridOptions.From.Name);
            message.AddTo(identity.Username, identity.Name);
            message.SetSubject(template.Subject);
            message.SetTemplateId(template.TemplateId);
            message.SetTemplateData(new { name = identity.Name, code = password });

            await sendGridClient.SendEmailAsync(message);
        }
    }
}
