using Directory.Models.Enums;

namespace Directory.Models.Dtos
{
    public class ResponseDto<T>
    {
        public bool IsSucceed { get; set; }
        public ResultStatus Status { get; set; }
        public required string Message { get; set; }
        public required T Data { get; set; }
    }
}