using System;
using System.Collections.Generic;

namespace quipquick_api.Models
{
    public partial class CategoryJoke
    {
        public int IdCategory { get; set; }
        public int IdJoke { get; set; }

        public virtual Category IdCategoryNavigation { get; set; } = null!;
        public virtual Joke IdJokeNavigation { get; set; } = null!;
    }
}
