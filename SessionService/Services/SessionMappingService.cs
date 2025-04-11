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
                OnlineStatus = session.OnlineStatus
            };
        }

        public Session CreateToSession(SessionCreate sessionCreate)
        {
            return new Session
            {
                Token = sessionCreate.Token,
                OnlineStatus = sessionCreate.OnlineStatus,
                Login = sessionCreate.Login,
                Logout = sessionCreate.Logout
            };
        }

        public Session UpdateToSession(Session existingSession, SessionUpdate sessionUpdate)
        {
            existingSession.Id = existingSession.Id;
            existingSession.Token = sessionUpdate.Token ?? existingSession.Token;
            existingSession.OnlineStatus = sessionUpdate.OnlineStatus ?? existingSession.OnlineStatus;
            existingSession.Login = sessionUpdate.Login ?? existingSession.Login;
            existingSession.Logout = sessionUpdate.Logout ?? existingSession.Logout;
            return existingSession;
        }


    }
}
