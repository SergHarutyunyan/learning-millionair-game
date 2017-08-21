using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectModels
{
        [Table("Category")]
        public class Categories
        {
            [Key]
            public int Id { get; set; }
            public int Price { get; set; }
            public bool IsPrimary { get; set; }
      
        }    
}
