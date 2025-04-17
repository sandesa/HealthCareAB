using JournalService.Database;
using JournalService.DTO;
using JournalService.Interfaces;
using JournalService.Models;
using JournalService.Services;
using Microsoft.EntityFrameworkCore;

namespace JournalService.Repositories
{
    public class JournalRepository : IJournalRepository
    {
        private readonly JournalDbContext _context;
        private readonly JournalMappingService _mapper;

        public JournalRepository(JournalDbContext context, JournalMappingService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Journal>> GetJournalsDevAsync()
        {
            return await _context.Journals.ToListAsync();
        }

        public async Task<IEnumerable<JournalDTO>> GetJournalsByUserIdAsync(int? caregiverId, int? patientId)
        {
            List<Journal> journals = [];
            if (caregiverId != null)
            {
                journals = await _context.Journals.Where(j => j.CaregiverId == caregiverId).ToListAsync();
            }
            if (patientId != null)
            {
                journals = await _context.Journals.Where(j => j.PatientId == patientId).ToListAsync();
            }
            var journalDTOs = journals.Select(j => _mapper.JournalToDto(j));
            return journalDTOs;
        }

        public async Task<JournalDTO?> GetJournalByIdAsync(int id)
        {
            var journal = await _context.Journals.FirstOrDefaultAsync(j => j.Id == id);
            if (journal == null)
            {
                return null;
            }
            var journalDTO = _mapper.JournalToDto(journal);
            return journalDTO;
        }

        public async Task<JournalDTO?> CreateJournalAsync(JournalCreate journalCreate)
        {
            var journal = _mapper.CreateToJournal(journalCreate);
            journal.CreatedAt = DateTime.Now;

            await _context.Journals.AddAsync(journal);
            await _context.SaveChangesAsync();

            var journalDTO = _mapper.JournalToDto(journal);
            return journalDTO;
        }

        public async Task<JournalDTO?> UpdateJournalAsync(int id, JournalUpdate journalUpdate)
        {
            var journal = await _context.Journals.FindAsync(id);
            if (journal == null)
            {
                return null;
            }
            if (journalUpdate != null)
            {
                journal = _mapper.UpdateToJournal(journal, journalUpdate);
            }

            journal.UpdatedAt = DateTime.Now;
            _context.Journals.Update(journal);
            await _context.SaveChangesAsync();
            var journalDTO = _mapper.JournalToDto(journal);
            return journalDTO;
        }

        public async Task<JournalDTO?> DeleteJournalAsync(int id)
        {
            var journal = await _context.Journals.FindAsync(id);
            if (journal == null)
            {
                return null;
            }
            _context.Journals.Remove(journal);
            await _context.SaveChangesAsync();

            var journalDTO = _mapper.JournalToDto(journal);
            return journalDTO;
        }
    }
}
