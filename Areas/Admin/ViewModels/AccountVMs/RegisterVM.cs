using System.ComponentModel.DataAnnotations;

namespace FinalExam_B14.Areas.Admin.ViewModels.AccountVMs
{
    public class RegisterVM
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(255)]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
