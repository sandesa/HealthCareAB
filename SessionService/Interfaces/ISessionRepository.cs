using SessionService.DTO;
using SessionService.Models;

namespace SessionService.Interfaces
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> GetSessionDevAsync();
        Task<SessionDTO?> GetSessionByIdAsync(int id);
        Task<SessionDTO?> CreateSessionAsync(SessionCreate sessionCreate);
        Task<SessionDTO?> UpdateSessionAsync(string? token, int? id, SessionUpdate? sessionUpdate);
        Task<SessionDTO?> DeleteSessionAsync(int id);
    }
}
