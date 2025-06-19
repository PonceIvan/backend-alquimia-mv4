namespace alquimia.Services.Interfaces
{
    public interface IEmailTemplateService
    {
        string GetPasswordResetEmail(string userName, string resetLink);
        string GetWelcomeEmail(string userName);
    }
}
