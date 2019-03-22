using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.API.Models.FileData
{
    public class FileDataModel
    {
        public int ID { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Extension { get; set; }

    }
}
