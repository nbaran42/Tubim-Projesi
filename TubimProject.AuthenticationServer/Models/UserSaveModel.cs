using System.ComponentModel.DataAnnotations;

namespace TubimProject.AuthenticationServer.Models
{
    public class UserSaveModel
    {
         
            [Required]
            public string UserName { get; set; }



            [DataType(DataType.Password)]
            [Required]
            public string Password { get; set; }

            [Required]
            public string Sicil { get; set; }
    
    }
}
