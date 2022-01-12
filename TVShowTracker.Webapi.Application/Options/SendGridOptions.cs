namespace TVShowTracker.Webapi.Application.Options
{
    public class SendGridOption
    {
        public string ApiKey { get; set; }
        public From From { get; set; }
        public Template Templates { get; set; }
    }

    public class From 
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }

    public class Template 
    {
        public TemplateItem TrackerGeneratePasswordIntegrationEventPT { get; set; }
        public TemplateItem TrackerGeneratePasswordIntegrationEventEN { get; set; }
        public TemplateItem TrackerGeneratePasswordIntegrationEventES { get; set; }
        public TemplateItem TrackerGeneratePasswordIntegrationEventIT { get; set; }
        public TemplateItem TrackerGeneratePasswordIntegrationEventFR { get; set; }
        public TemplateItem TrackerPasswordRetrieveIntegrationEventPT { get; set; }
        public TemplateItem TrackerPasswordRetrieveIntegrationEventEN { get; set; }
        public TemplateItem TrackerPasswordRetrieveIntegrationEventES { get; set; }
        public TemplateItem TrackerPasswordRetrieveIntegrationEventIT { get; set; }
        public TemplateItem TrackerPasswordRetrieveIntegrationEventFR { get; set; }
    }

    public class TemplateItem
    {
        public string TemplateId { get; set; }
        public string Subject { get; set; }
    }
}
