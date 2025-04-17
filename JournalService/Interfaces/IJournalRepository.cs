using JournalService.DTO;
using JournalService.Models;

namespace JournalService.Interfaces
{
    public interface IJournalRepository
    {
        Task<IEnumerable<Journal>> GetJournalsDevAsync();
        Task<IEnumerable<JournalDTO>> GetJournalsByUserIdAsync(int? caregiverId, int? patientId);
        Task<JournalDTO?> GetJournalByIdAsync(int id);
        Task<JournalDTO?> CreateJournalAsync(JournalCreate journalCreate);
        Task<JournalDTO?> UpdateJournalAsync(int id, JournalUpdate journalUpdate);
        Task<JournalDTO?> DeleteJournalAsync(int id);
    }
}
