using System;

namespace WebApplication1.Dto
{
    public class AnimeForCreationDto
    {
        public int about_id { get; set; }
        public string title { get; set; }
        public float score { get; set; }
        public int episodes { get; set; }
        public string aired { get; set; }
        public string type { get; set; }
        public string author { get; set; }
        public int authorid { get; set; }
    }
}
