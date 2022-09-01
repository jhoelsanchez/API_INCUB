using System.ComponentModel.DataAnnotations;

namespace InClub.Core.Models
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Boolean Status { get; set; }
    }
}
