using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Domain
{
    public class OpenFileResult
    {
        public string FilePath { get; set; }
        public Stream File { get; set; }

        public OpenFileResult(string filePath, Stream file)
        {
            FilePath = filePath;
            File = file;
        }
    }
}
