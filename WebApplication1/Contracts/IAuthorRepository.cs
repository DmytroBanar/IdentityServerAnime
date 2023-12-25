using WebApplication1.Dto;
using WebApplication1.Entities;

namespace WebApplication1.Contracts
{
    public interface IAuthorRepository
    {

        Task<IEnumerable<author>> GetAllAuthors();
        Task<author> GetAuthorById(int id);
        Task<author> CreateAuthor(AuthorForCreationDto author);
        Task UpdateAuthor(int id, AuthorForUpdateDto author);
        Task DeleteAuthor(int id);
  




    }
}
