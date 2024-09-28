using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Identity;

using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class DatabaseSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public DatabaseSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public async Task SeedAsync()
        {
            // Создание ролей и пользователей
            await CreateRoleAsync("Admin");
            await CreateRoleAsync("User");
            await CreateUserAsync("admin@library.com", "Admin123!", "Admin");
            await CreateUserAsync("user@library.com", "User123!", "User");

            // Добавление авторов и книг
            await SeedAuthorsAndBooksAsync();
        }

        private async Task SeedAuthorsAndBooksAsync()
        {
            // Проверка, есть ли уже авторы в базе
            var existingAuthors = await _unitOfWork.Authors.GetAllAsync();
            if (!existingAuthors.Any())
            {
                // Создаем авторов
                var author1 = new Author
                {
                    FirstName = "George",
                    LastName = "Orwell",
                    DateOfBirth = new DateTime(1903, 6, 25),
                    Country = "United Kingdom"
                };

                var author2 = new Author
                {
                    FirstName = "Harper",
                    LastName = "Lee",
                    DateOfBirth = new DateTime(1926, 4, 28),
                    Country = "United States"
                };

                await _unitOfWork.Authors.AddAsync(author1);
                await _unitOfWork.Authors.AddAsync(author2);

                // Создаем книги для авторов
                var book1 = new Book
                {
                    Title = "1984",
                    Genre = "Fiction",
                    ISBN = "9780451524935",
                    Description = "A dystopian social science fiction novel.",
                    Author = author1
                };

                var book2 = new Book
                {
                    Title = "Animal Farm",
                    Genre = "Mystery",
                    ISBN = "9780451526342",
                    Description = "A political allegory and satire.",
                    Author = author1
                };

                var book3 = new Book
                {
                    Title = "To Kill a Mockingbird",
                    Genre = "Classic",
                    ISBN = "9780061120084",
                    Description = "A novel about racial injustice in the Deep South.",
                    Author = author2
                };

                await _unitOfWork.Books.AddAsync(book1);
                await _unitOfWork.Books.AddAsync(book2);
                await _unitOfWork.Books.AddAsync(book3);

                // Сохранение изменений в базе данных через UnitOfWork
                await _unitOfWork.CompleteAsync();
            }
        }

        private async Task CreateRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var role = new IdentityRole(roleName);
                await _roleManager.CreateAsync(role);
            }
        }

        private async Task CreateUserAsync(string email, string password, string role)
        {
            var userExists = await _userManager.FindByEmailAsync(email);
            if (userExists == null)
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email
                };
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
