using TVShowTracker.Webapi.Application.Options;
using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using TVShowTracker.Webapi.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Options;
using SendGrid;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;
using System.Threading;
using System;


namespace TVShowTracker.Webapi.Application.DomainEventHandlers
{
    public class SendEmailPasswordRetrieveCreatedDomainEventHandler : INotificationHandler<PasswordRetrieveCreatetedDomainEvent>
    {
        private SendGridOption SendGridOptions { get; }
        private AppKeysOption AppKeysOption { get; }

        public SendEmailPasswordRetrieveCreatedDomainEventHandler(IOptions<SendGridOption> sendGridOptions,
                                                                  IOptions<AppKeysOption> appKeysOption) 
        {
            SendGridOptions = sendGridOptions.Value;
            AppKeysOption = appKeysOption.Value;
        }

        public async Task Handle(PasswordRetrieveCreatetedDomainEvent notification, CancellationToken cancellationToken)
        {

            Identity identity = notification.Identity;
            string passwordProvisional = notification.PasswordProvisional;

            SendGridClient sendGridClient = new SendGridClient(SendGridOptions.ApiKey);
            SendGridMessage message = new SendGridMessage();
            TemplateItem template = notification.Language == null || notification.Language == "" ? SendGridOptions.Templates.TrackerPasswordRetrieveIntegrationEventPT :
                notification.Language.ToUpper() == "EN" ? SendGridOptions.Templates.TrackerPasswordRetrieveIntegrationEventEN :
                notification.Language.ToUpper() == "ES" ? SendGridOptions.Templates.TrackerPasswordRetrieveIntegrationEventES :
                notification.Language.ToUpper() == "IT" ? SendGridOptions.Templates.TrackerPasswordRetrieveIntegrationEventIT :
                notification.Language.ToUpper() == "FR" ? SendGridOptions.Templates.TrackerPasswordRetrieveIntegrationEventFR :
                SendGridOptions.Templates.TrackerPasswordRetrieveIntegrationEventPT ;

            message.SetFrom(SendGridOptions.From.Email, SendGridOptions.From.Name);
            message.AddTo(identity.Username, identity.Name);
            message.SetSubject(template.Subject);
            message.SetTemplateId(template.TemplateId);
            message.SetTemplateData(new { name = identity.Name, code = passwordProvisional });

            await sendGridClient.SendEmailAsync(message);
        }
    }
}
