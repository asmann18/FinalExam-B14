using System.ComponentModel.DataAnnotations;

namespace FinalExam_B14.Areas.Admin.ViewModels.AccountVMs;

public class LoginVM
{
    [MaxLength(255)]
    public string Username { get; set; }
    [DataType(DataType.Password)]
    public string Password {get; set; }
}