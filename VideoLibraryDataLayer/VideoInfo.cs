using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCSD.VideoLibrary
{
    public class VideoInfo
    {
        public int VideoId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public int TotalCopies { get; set; }
        public bool IsDeleted { get; set; }
        public VideoFormat Format { get; set; }
    }
}
