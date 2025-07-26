using System.ComponentModel.DataAnnotations;

namespace WalksAPI.Models.DTOs.AuthenticationDTO
{
    public class RegisterRequestDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Username must be between 3 and 100 characters.", MinimumLength = 3)]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Password must be between 6 and 100 characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}
