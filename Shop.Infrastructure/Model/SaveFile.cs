using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Model
{
    public class SaveFile
    {
        public byte[] File { get; set; }

        public string FileName { get; set; }

        public long FileSize { get; set; }
        public string FileExtension { get; set; }
    }
}
