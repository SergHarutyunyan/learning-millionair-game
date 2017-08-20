using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectModels
{
    class Categories
    {
        [Table("Category")]
        public class Questions
        {
            [Key]
            public int Id { get; set; }
            public int Price { get; set; }
            public int IsPrimary { get; set; }
      
        }
    }
}
