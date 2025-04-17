using JournalService.DTO;
using JournalService.Models;

namespace JournalService.Services
{
    public class JournalMappingService
    {
        public JournalDTO JournalToDto(Journal journal)
        {
            return new JournalDTO
            {
                CaregiverId = journal.CaregiverId,
                PatientId = journal.PatientId,
                BookingId = journal.BookingId,
                JournalType = journal.JournalType,
                JournalEntry = journal.JournalEntry,
                CreatedAt = journal.CreatedAt,
                UpdatedAt = journal.UpdatedAt
            };
        }

        public Journal CreateToJournal(JournalCreate journalCreate)
        {
            return new Journal
            {
                CaregiverId = journalCreate.CaregiverId,
                PatientId = journalCreate.PatientId,
                BookingId = journalCreate.BookingId,
                JournalType = journalCreate.JournalType,
                JournalEntry = journalCreate.JournalEntry,
            };
        }

        public Journal UpdateToJournal(Journal existingJournal, JournalUpdate journalUpdate)
        {
            existingJournal.Id = existingJournal.Id;
            existingJournal.PatientId = existingJournal.PatientId;
            existingJournal.CaregiverId = existingJournal.CaregiverId;
            existingJournal.BookingId = journalUpdate.BookingId ?? existingJournal.BookingId;
            existingJournal.JournalType = journalUpdate.JournalType ?? existingJournal.JournalType;
            existingJournal.JournalEntry = journalUpdate.JournalEntry ?? existingJournal.JournalEntry;
            existingJournal.CreatedAt = existingJournal.CreatedAt;
            return existingJournal;
        }
    }
}
