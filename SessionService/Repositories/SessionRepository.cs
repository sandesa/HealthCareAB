using Microsoft.EntityFrameworkCore;
using SessionService.Database;
using SessionService.DTO;
using SessionService.Interfaces;
using SessionService.Models;
using SessionService.Services;

namespace SessionService.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly SessionDbContext _context;
        private readonly SessionMappingService _mapper;

        public SessionRepository(SessionDbContext context, SessionMappingService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Session>> GetSessionDevAsync()
        {
            return await _context.Sessions.ToListAsync();
        }

        public async Task<SessionDTO?> GetSessionByIdAsync(int id)
        {
            var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Id == id);
            if (session == null)
            {
                return null;
            }
            var sessionDTO = _mapper.SessionToDto(session);
            return sessionDTO;

        }

        public async Task<SessionDTO?> CreateSessionAsync(SessionCreate sessionCreate)
        {
            var session = _mapper.CreateToSession(sessionCreate);
            session.OnlineStatus = true;
            session.Login = DateTime.Now;
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
            var sessionDTO = _mapper.SessionToDto(session);
            return sessionDTO;
        }

        public async Task<SessionDTO?> UpdateSessionAsync(int id, SessionUpdate SessionUpdate)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return null;
            }
            session = _mapper.UpdateToSession(session, SessionUpdate);
            _context.Sessions.Update(session);
            await _context.SaveChangesAsync();
            var sessionDTO = _mapper.SessionToDto(session);
            return sessionDTO;
        }

        public async Task<SessionDTO?> DeleteSessionAsync(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return null;
            }
            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
            var sessionDTO = _mapper.SessionToDto(session);
            return sessionDTO;
        }
    }
}
