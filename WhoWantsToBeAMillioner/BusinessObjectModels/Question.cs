using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectModels
{
   
    [Table("Questions")]
    public class Question
    {
        [Key]
        public int id { get; set; }
        public string question { get; set; }
        public string variantA { get; set; }
        public string variantB { get; set; }
        public string variantC { get; set; }
        public string variantD { get; set; }
        public byte   answer   { get; set; }
        public int    category { get; set; }
    }
}
