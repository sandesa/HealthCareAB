using JournalService.DTO;
using JournalService.Interfaces;
using JournalService.Models;

namespace JournalService.Services
{
    public class JournalService
    {
        private readonly IJournalRepository _journalRepository;

        public JournalService(IJournalRepository journalRepository)
        {
            _journalRepository = journalRepository;
        }

        public async Task<ResponseDTO<IEnumerable<Journal>>> GetJournalsDevAsync()
        {
            try
            {
                var journals = await _journalRepository.GetJournalsDevAsync();
                return new ResponseDTO<IEnumerable<Journal>>
                {
                    Data = journals,
                    Message = "Journals DEV retrieved successfully.",
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<Journal>>
                {
                    Message = $"An error occurred while DEV retrieving journals: {ex.Message}",
                };
            }
        }

        public async Task<ResponseDTO<IEnumerable<JournalDTO>>> GetJournalsByUserIdAsync(int? caregiverId, int? patientId)
        {
            try
            {
                var journals = await _journalRepository.GetJournalsByUserIdAsync(caregiverId, patientId);
                return new ResponseDTO<IEnumerable<JournalDTO>>
                {
                    Data = journals,
                    Message = "Journals retrieved successfully.",
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<JournalDTO>>
                {
                    Message = $"An error occurred while retrieving journals: {ex.Message}",
                };
            }
        }

        public async Task<ResponseDTO<JournalDTO>> GetJournalByIdAsync(int id)
        {
            try
            {
                var journal = await _journalRepository.GetJournalByIdAsync(id);
                if (journal == null)
                {
                    return new ResponseDTO<JournalDTO>
                    {
                        Message = "Journal not found.",
                    };
                }
                return new ResponseDTO<JournalDTO>
                {
                    Data = journal,
                    Message = "Journal retrieved successfully.",
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<JournalDTO>
                {
                    Message = $"An error occurred while retrieving the journal: {ex.Message}",
                };
            }
        }

        public async Task<ResponseDTO<JournalDTO>> CreateJournalAsync(JournalCreate journalCreate)
        {
            try
            {
                var journal = await _journalRepository.CreateJournalAsync(journalCreate);
                if (journal == null)
                {
                    return new ResponseDTO<JournalDTO>
                    {
                        Message = "Failed to create journal.",
                    };
                }
                return new ResponseDTO<JournalDTO>
                {
                    Data = journal,
                    Message = "Journal created successfully.",
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<JournalDTO>
                {
                    Message = $"An error occurred while creating the journal: {ex.Message}",
                };
            }
        }

        public async Task<ResponseDTO<JournalDTO>> UpdateJournalAsync(int id, JournalUpdate journalUpdate)
        {
            try
            {
                var journal = await _journalRepository.UpdateJournalAsync(id, journalUpdate);
                if (journal == null)
                {
                    return new ResponseDTO<JournalDTO>
                    {
                        Message = "Failed to update journal.",
                    };
                }
                return new ResponseDTO<JournalDTO>
                {
                    Data = journal,
                    Message = "Journal updated successfully.",
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<JournalDTO>
                {
                    Message = $"An error occurred while updating the journal: {ex.Message}",
                };
            }
        }

        public async Task<ResponseDTO<JournalDTO>> DeleteJournalAsync(int id)
        {
            try
            {
                var journal = await _journalRepository.DeleteJournalAsync(id);
                if (journal == null)
                {
                    return new ResponseDTO<JournalDTO>
                    {
                        Message = "Failed to delete journal.",
                    };
                }
                return new ResponseDTO<JournalDTO>
                {
                    Data = journal,
                    Message = "Journal deleted successfully.",
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<JournalDTO>
                {
                    Message = $"An error occurred while deleting the journal: {ex.Message}",
                };
            }
        }
    }
}
