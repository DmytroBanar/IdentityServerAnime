using System;

namespace WebApplication1.Dto
{
    public class AuthorForCreationDto
    {
        public int author_id { get; set; }
        public string author_name { get; set; }
        public string date_of_dirth { get; set; }
        public string place_of_residence { get; set; }
        public string most_popular_work { get; set; }
    }
}
