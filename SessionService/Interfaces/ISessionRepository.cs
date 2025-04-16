using SessionService.DTO;
using SessionService.Models;

namespace SessionService.Interfaces
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> GetSessionDevAsync();
        Task<SessionDTO?> GetSessionByIdAsync(int id);
        Task<SessionDTO?> CreateSessionAsync(SessionCreate sessionCreate);
        Task<SessionDTO?> UpdateSessionAsync(int id, SessionUpdate? sessionUpdate, bool logout);
        Task<SessionDTO?> DeleteSessionAsync(int id);
    }
}
