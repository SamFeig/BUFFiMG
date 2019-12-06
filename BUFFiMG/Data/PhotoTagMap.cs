using System;
using System.Collections.Generic;

namespace BUFFiMG.Data
{
    public partial class PhotoTagMap
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }
        public int TagId { get; set; }

        public virtual Photos Photo { get; set; }
        public virtual Tags Tag { get; set; }
    }
}
