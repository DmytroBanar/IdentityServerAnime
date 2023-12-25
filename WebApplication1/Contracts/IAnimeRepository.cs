using WebApplication1.Dto;
using WebApplication1.Entities;

namespace WebApplication1.Contracts
{
    public interface IAnimeRepository
    {

        Task<IEnumerable<about>> GetAllAnime();
        Task<about> GetAnimeById(int id);
        Task<about> CreateAnime(AnimeForCreationDto about);
        Task UpdateAnime(int id, AnimeForUpdateDto about);
        Task DeleteAnime(int id);
        Task<List<about>> GetAnimeByAuthorId(int authorId);




    }
}
