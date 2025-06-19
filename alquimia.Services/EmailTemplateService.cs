using alquimia.Services.Interfaces;

namespace alquimia.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        public string GetPasswordResetEmail(string userName, string resetLink)
        {
            return $@"
            <p>Hola, {userName},</p>
            <p>Recibimos una solicitud para restablecer tu contraseña. Podés hacerlo haciendo clic en el siguiente enlace:</p>
            <p><a href='{resetLink}'>Restablecer contraseña</a></p>
            <p>Si no fuiste vos, podés ignorar este mensaje.</p>
            <p><strong>Equipo de Alquimia</strong></p>";
        }

        public string GetWelcomeEmail(string userName)
        {
            return $@"
                <h1>¡Bienvenido a Alquimia, {userName}!</h1>
                <p>Gracias por registrarte como proveedor.</p>
                <p>Tu cuenta ha sido creada exitosamente, pero antes de poder cargar tus productos, debe ser aprobada por nuestro equipo.</p>
                <p>Te avisaremos por correo una vez que tu cuenta esté habilitada.</p>
                <br/>
                <p>Gracias por tu paciencia.</p>
                <p><strong>Equipo de Alquimia</strong></p>";
        }
    }
}
