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
    public static class Questions
    {

        [Key]
        public static int Id { get; set; }
        public static string Question { get; set; }
        public static string VariantA { get; set; }
        public static string VariantB { get; set; }
        public static string VariantC { get; set; }
        public static string VariantD { get; set; }
        public static byte   Answer   { get; set; }
        public static int    Category { get; set; }

    }
}
