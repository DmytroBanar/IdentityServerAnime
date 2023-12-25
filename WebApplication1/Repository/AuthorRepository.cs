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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DapperContext _context;

        public AuthorRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<author>> GetAllAuthors()
        {
            var query = "SELECT * FROM author";
            using (var connection = _context.CreateConnection())
            {
                var animeAuthor = await connection.QueryAsync<author>(query);
                return animeAuthor;
            }
        }

        public async Task<author> GetAuthorById(int id)
        {
            var query = "SELECT * FROM author WHERE author_id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var authorid = await connection.QuerySingleOrDefaultAsync<author>(query, new { Id = id });
                return authorid;
            }
        }

        public async Task<author> CreateAuthor(AuthorForCreationDto authorid)
        {
            var query = "INSERT INTO author (author_name, date_of_dirth, place_of_residence, most_popular_work) " +
                        "VALUES (@Author, @Date_of_dirth, @Place_of_residence, @Most_popular_work) " +
                        "RETURNING author_id";

            var parameters = new DynamicParameters();
            parameters.Add("Author", authorid.author_name, DbType.String);
            parameters.Add("Date_of_dirth", authorid.date_of_dirth, DbType.String);
            parameters.Add("Place_of_residence", authorid.place_of_residence, DbType.String);
            parameters.Add("Most_popular_work", authorid.most_popular_work, DbType.String);
            

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdAuthor = new author
                {
                    author_id = id,
                    author_name = authorid.author_name,
                    date_of_dirth = authorid.date_of_dirth,
                    place_of_residence = authorid.place_of_residence,
                    most_popular_work = authorid.most_popular_work,
                    
                };

                return createdAuthor;
            }
        }


        public async Task UpdateAuthor(int id, AuthorForUpdateDto author)
        {
            var query = "UPDATE author " +
                "SET author_name = @author_name, date_of_dirth = @date_of_dirth, place_of_residence = @place_of_residence, most_popular_work = @most_popular_work " +
                "WHERE author_id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id, author.author_name, author.date_of_dirth, author.place_of_residence, author.most_popular_work });
            }
        }

        public async Task DeleteAuthor(int id)
        {
            var query = "DELETE FROM author WHERE author_id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }




    }
}
