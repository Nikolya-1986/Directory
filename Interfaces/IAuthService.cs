using Directory.Models.Dtos;
using Directory.Models.Requests;

namespace Directory.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto<bool>> RegisterAsync(Register register);
    }
}