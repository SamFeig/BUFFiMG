using System;
using System.Collections.Generic;

namespace BUFFiMG.Data
{
    public partial class Tags
    {
        public Tags()
        {
            PhotoTagMap = new HashSet<PhotoTagMap>();
        }

        public int TagId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PhotoTagMap> PhotoTagMap { get; set; }
    }
}
