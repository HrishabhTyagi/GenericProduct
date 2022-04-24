using System.ComponentModel.DataAnnotations;

namespace Common.RequestModel
{
    public class LoginRequest
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email Id is not valid")]
        public string EmailId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        public string Password { get; set; }

    }
}
