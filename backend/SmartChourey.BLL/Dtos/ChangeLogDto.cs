using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.Dtos
{
    public class ChangeLogDto
    {
        public string Email { get; set; }
        public string Category { get; set; }
        public string ChangeCategory { get; set; }
        public string ChangeType { get; set; }
        public string ChangedProperties { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

    }
}
