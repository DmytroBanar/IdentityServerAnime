using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Context;
using WebApplication1.Contracts;
using WebApplication1.Dto;
using WebApplication1.Entities;

namespace WebApplication1.Repository
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly DapperContext _context;

        public AnimeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<about>> GetAllAnime()
        {
            var query = "SELECT * FROM about";
            using (var connection = _context.CreateConnection())
            {
                var anime = await connection.QueryAsync<about>(query);
                return anime;
            }
        }

        public async Task<about> GetAnimeById(int id)
        {
            var query = "SELECT * FROM about WHERE about_id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var animeid = await connection.QuerySingleOrDefaultAsync<about>(query, new { Id = id });
                return animeid;
            }
        }

        public async Task<about> CreateAnime(AnimeForCreationDto animeid)
        {
            var query = "INSERT INTO about (title, score, episodes, aired, type, author, authorid) " +
                        "VALUES (@Title, @Score, @Episodes, @Aired, @Type, @Author, @Authorid) " +
                        "RETURNING about_id";

            var parameters = new DynamicParameters();
            parameters.Add("Title", animeid.title, DbType.String);
            parameters.Add("Score", animeid.score, DbType.Single);
            parameters.Add("Episodes", animeid.episodes, DbType.Int32);
            parameters.Add("Aired", animeid.aired, DbType.String);
            parameters.Add("Type", animeid.type, DbType.String);
            parameters.Add("Author", animeid.author, DbType.String);
            parameters.Add("Authorid", animeid.authorid, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdAnime = new about
                {
                    about_id = id,
                    title = animeid.title,
                    score = animeid.score,
                    episodes = animeid.episodes,
                    aired = animeid.aired,
                    type = animeid.type,
                    author = animeid.author,
                    authorid = animeid.authorid
                };

                return createdAnime;
            }
        }


        public async Task UpdateAnime(int id, AnimeForUpdateDto about)
        {
            var query = "UPDATE about " +
                        "SET title = @title, score = @score, episodes = @episodes, aired = @aired, type = @type, author = @author, authorid = @authorid " +
                        "WHERE about_id = @id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id, about.title, about.score, about.episodes, about.aired, about.type, about.author, about.authorid });
            }
        }

        public async Task DeleteAnime(int id)
        {
            var query = "DELETE FROM about WHERE about_id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }


        public async Task<List<about>> GetAnimeByAuthorId(int authorId)
        {
            var query = @"
                SELECT author.*, about.title
                FROM author
                INNER JOIN about ON author.author_id = about.author_id
                WHERE author.author_id = @AuthorId;";

            using (var connection = _context.CreateConnection())
            {
                var animeList = await connection.QueryAsync<about>(query, new { AuthorId = authorId });
                return animeList.ToList();
            }
        }




    }
}
