namespace Assignment.Contracts.DTO
{
    public class BaseResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string[] Errors { get; set; }
        public string[] Message { get; set; }
        
    }
}