using SessionService.DTO;
using SessionService.Models;

namespace SessionService.Services
{
    public class SessionMappingService
    {
        public SessionDTO SessionToDto(Session session)
        {
            return new SessionDTO
            {
                Id = session.Id,
                Email = session.Email,
                AccessToken = session.AccessToken,
                Login = session.Login,
                Logout = session.Logout
            };
        }

        public Session CreateToSession(SessionCreate sessionCreate)
        {
            return new Session
            {
                Email = sessionCreate.Email,
                AccessToken = sessionCreate.AccessToken,
                Expires = sessionCreate.Expires,
                Login = sessionCreate.Login,
                Logout = sessionCreate.Logout
            };
        }

        public Session UpdateToSession(Session existingSession, SessionUpdate sessionUpdate)
        {
            existingSession.Email = sessionUpdate.Email ?? existingSession.Email;
            existingSession.AccessToken = sessionUpdate.AccessToken ?? existingSession.AccessToken;
            existingSession.Expires = sessionUpdate.Expires;
            existingSession.Login = sessionUpdate.Login ?? existingSession.Login;
            existingSession.Logout = sessionUpdate.Logout ?? existingSession.Logout;
            return existingSession;
        }


    }
}
