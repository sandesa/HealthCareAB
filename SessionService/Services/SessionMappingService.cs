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
                UserId = session.UserId,
                AccessToken = session.AccessToken,
                Login = session.Login,
                Logout = session.Logout
            };
        }

        public Session CreateToSession(SessionCreate sessionCreate)
        {
            return new Session
            {
                UserId = sessionCreate.UserId,
                AccessToken = sessionCreate.AccessToken,
                Expires = sessionCreate.Expires
            };
        }

        public Session UpdateToSession(Session existingSession, SessionUpdate sessionUpdate)
        {
            existingSession.UserId = sessionUpdate.UserId ?? existingSession.UserId;
            existingSession.AccessToken = sessionUpdate.AccessToken ?? existingSession.AccessToken;
            existingSession.Expires = sessionUpdate.Expires;
            existingSession.Login = sessionUpdate.Login ?? existingSession.Login;
            existingSession.Logout = sessionUpdate.Logout ?? existingSession.Logout;
            return existingSession;
        }


    }
}
