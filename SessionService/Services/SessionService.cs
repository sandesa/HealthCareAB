using SessionService.DTO;
using SessionService.Interfaces;
using SessionService.Models;

namespace SessionService.Services
{
    public class SessionService
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<ResponseDTO<IEnumerable<Session>>> GetSessionsDevAsync()
        {
            try
            {
                var sessions = await _sessionRepository.GetSessionDevAsync();
                if (!sessions.Any())
                {
                    return new ResponseDTO<IEnumerable<Session>>
                    {
                        Message = "No sessions found.",
                        IsSuccess = true
                    };
                }
                return new ResponseDTO<IEnumerable<Session>>
                {
                    Data = sessions,
                    Message = "Sessions retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when getting DEV Session data. Error message: \"{ex.Message}\"");
                return new ResponseDTO<IEnumerable<Session>>
                {
                    Message = "An error occurred when getting DEV Session data.",
                    IsSuccess = false
                };
            }
        }

        public async Task<ResponseDTO<SessionDTO?>> GetSessionByIdAsync(int id)
        {
            try
            {
                var session = await _sessionRepository.GetSessionByIdAsync(id);
                if (session == null)
                {
                    return new ResponseDTO<SessionDTO?>
                    {
                        Message = "Session not found.",
                        IsSuccess = false
                    };
                }
                return new ResponseDTO<SessionDTO?>
                {
                    Data = session,
                    Message = "Session retrieved successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when getting Session data. Error message: \"{ex.Message}\"");
                return new ResponseDTO<SessionDTO?>
                {
                    Message = "An error occurred when getting Session data.",
                    IsSuccess = false
                };
            }
        }

        public async Task<ResponseDTO<SessionDTO?>> CreateLoginSessionAsync(SessionCreate sessionCreate)
        {
            try
            {
                var session = await _sessionRepository.CreateSessionAsync(sessionCreate);
                if (session == null)
                {
                    return new ResponseDTO<SessionDTO?>
                    {
                        Message = "Failed to create session.",
                        IsSuccess = false
                    };
                }
                return new ResponseDTO<SessionDTO?>
                {
                    Data = session,
                    Message = "Session created successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when creating Session data. Error message: \"{ex.Message}\"");
                return new ResponseDTO<SessionDTO?>
                {
                    Message = "An error occurred when creating Session data.",
                    IsSuccess = false
                };
            }
        }

        public async Task<ResponseDTO<SessionDTO?>> UpdateSessionAsync(string? token, int? id, SessionUpdate? sessionUpdate)
        {
            try
            {
                SessionDTO? session;

                if (sessionUpdate == null)
                {
                    if (token != null)
                    {
                        session = await _sessionRepository.UpdateSessionAsync(token, null, null);
                    }
                    else
                    {
                        session = await _sessionRepository.UpdateSessionAsync(null, id, null);
                    }
                }
                else
                {
                    session = await _sessionRepository.UpdateSessionAsync(null, id, sessionUpdate);
                }

                if (session == null)
                {
                    return new ResponseDTO<SessionDTO?>
                    {
                        Message = "Failed to update session.",
                        IsSuccess = false
                    };
                }
                return new ResponseDTO<SessionDTO?>
                {
                    Data = session,
                    Message = "Session updated successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when updating Session data. Error message: \"{ex.Message}\"");
                return new ResponseDTO<SessionDTO?>
                {
                    Message = "An error occurred when updating Session data.",
                    IsSuccess = false
                };
            }
        }

        public async Task<ResponseDTO<SessionDTO?>> DeleteSessionAsync(int id)
        {
            try
            {
                var session = await _sessionRepository.DeleteSessionAsync(id);
                if (session == null)
                {
                    return new ResponseDTO<SessionDTO?>
                    {
                        Message = "Failed to delete session.",
                        IsSuccess = false
                    };
                }
                return new ResponseDTO<SessionDTO?>
                {
                    Data = session,
                    Message = "Session deleted successfully.",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when deleting Session data. Error message: \"{ex.Message}\"");
                return new ResponseDTO<SessionDTO?>
                {
                    Message = "An error occurred when deleting Session data.",
                    IsSuccess = false
                };
            }
        }
    }
}