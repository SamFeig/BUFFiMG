using System;
using System.Collections.Generic;

namespace BUFFiMG.Data
{
    public partial class Photos
    {
        public Photos()
        {
            PhotoTagMap = new HashSet<PhotoTagMap>();
        }

        public int PhotoId { get; set; }
        public string FilePath { get; set; }
        public string UserId { get; set; }
        public bool IsPublic { get; set; }

        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<PhotoTagMap> PhotoTagMap { get; set; }
    }
}
