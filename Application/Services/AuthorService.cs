using Application.DTOs;
using Application.Interfaces;

using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<AuthorDto> AddAuthorAsync(AuthorDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);
            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.SaveChangesAsync(); // Сохранение через UnitOfWork
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task UpdateAuthorAsync(AuthorDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);
            await _unitOfWork.Authors.UpdateAsync(author);
            await _unitOfWork.SaveChangesAsync(); // Сохранение через UnitOfWork
        }

        public async Task DeleteAuthorAsync(int id)
        {
            await _unitOfWork.Authors.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync(); // Сохранение через UnitOfWork
        }
    }
}