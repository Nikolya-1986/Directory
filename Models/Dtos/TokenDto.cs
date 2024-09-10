namespace Directory.Models.Dtos
{
    public class TokenDto
    {
        public required string AcceessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}