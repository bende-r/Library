using Domain.Entities;
using System;

using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBookRepository Books { get; private set; }
        public IAuthorRepository Authors { get; private set; }
        public IUserBookRepository UserBooks { get; private set; }
        public IUserRepository Users { get; private set; }

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _applicationDbContext = context;
            _userManager = userManager;
            _roleManager = roleManager;
            Books = new BookRepository(_applicationDbContext);
            Authors = new AuthorRepository(_applicationDbContext);
            UserBooks = new UserBookRepository(_applicationDbContext);
            Users = new UserRepository( _applicationDbContext, _userManager, _roleManager);
        }

        

        //public UnitOfWork(ApplicationDbContext context)
        //{
        //    _context = context;
        //    Books = new BookRepository(_context);
        //    Authors = new AuthorRepository(_context);
        //    UserBooks = new UserBookRepository(_context);
        //}

        public async Task<int> CompleteAsync()
        {
            return await _applicationDbContext.SaveChangesAsync();
        }

        public async Task Save()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }

}