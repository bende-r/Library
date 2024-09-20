namespace Application.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public ICollection<int> BookIds { get; set; }

        // Дополнительное свойство для полного имени автора
        public string FullName => $"{FirstName} {LastName}";

        // Вычисляемое свойство для возраста автора
        public int Age => CalculateAge(DateOfBirth);

        private int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}