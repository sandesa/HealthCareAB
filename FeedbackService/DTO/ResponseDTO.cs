namespace FeedbackService.DTO
{
    public class ResponseDTO<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; } = "";
        public bool IsSuccess { get; set; } = false;
    }
}
