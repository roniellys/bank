using System.ComponentModel.DataAnnotations;

namespace Bank.API.ViewModel
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "The name cannot be empty.")]
        [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres.")]
        [MaxLength(80, ErrorMessage = "O nome deve ter no máximo 80 caracteres.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "The email cannot be empty.")]
        [MinLength(10, ErrorMessage = "O email deve ter no mínimo 10 caracteres.")]
        [MaxLength(180, ErrorMessage = "O nome deve ter no máximo 180 caracteres.")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "O email informado não é valido.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "The password cannot be empty.")]
        [MinLength(6, ErrorMessage = "O password deve ter no mínimo 6 caracteres.")]
        [MaxLength(30, ErrorMessage = "O nome deve ter no máximo 30 caracteres.")]
        public string Password { get; set; }
    }
}