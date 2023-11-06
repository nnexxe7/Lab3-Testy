using System.ComponentModel.DataAnnotations;

namespace Lab3_Testy
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Imie musi miec przynajmniej 3 znaki")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Nazwisko musi miec przynajmniej 3 znaki")]
        public string LastName { get; set; }
    }
}