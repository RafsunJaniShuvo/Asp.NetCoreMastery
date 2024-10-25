using System.ComponentModel.DataAnnotations;

namespace Asp.NetCoreMastery.Models
{
    public class TestModel
    {
        [Required,EmailAddress,Display(Name ="Email Address")]
        public string? Email { get; set; }
        public string? Password { get; set; }
       public string? ConfirmPassword { get; set; }
    }
}
