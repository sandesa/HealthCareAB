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
                Email = session.Email,
                OnlineStatus = session.OnlineStatus
            };
        }

        public Session CreateToSession(SessionCreate sessionCreate)
        {
            return new Session
            {
                Email = sessionCreate.Email,
                AccessToken = sessionCreate.AccessToken,
                ExpiresIn = sessionCreate.ExpiresIn,
                OnlineStatus = sessionCreate.OnlineStatus,
                Login = sessionCreate.Login,
                Logout = sessionCreate.Logout
            };
        }

        public Session UpdateToSession(Session existingSession, SessionUpdate sessionUpdate)
        {
            existingSession.Id = existingSession.Id;
            existingSession.Email = sessionUpdate.Email ?? existingSession.Email;
            existingSession.AccessToken = sessionUpdate.AccessToken ?? existingSession.AccessToken;
            existingSession.ExpiresIn = sessionUpdate.ExpiresIn;
            existingSession.OnlineStatus = sessionUpdate.OnlineStatus ?? existingSession.OnlineStatus;
            existingSession.Login = sessionUpdate.Login ?? existingSession.Login;
            existingSession.Logout = sessionUpdate.Logout ?? existingSession.Logout;
            return existingSession;
        }


    }
}
