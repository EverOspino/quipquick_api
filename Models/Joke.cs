using System;
using System.Collections.Generic;

namespace quipquick_api.Models
{
    public partial class Joke
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public int Likes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
