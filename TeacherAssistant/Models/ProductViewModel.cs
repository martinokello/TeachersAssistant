using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeacherAssistant.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; } = "MartinLayooInc ProductName Default";
        [Required]
        public string ProductDescription { get; set; } = "MartinLayooInc ProductDescription Default";
        [Required]
        public decimal ProductPrice { get; set; } = (decimal)0.00;
        public bool IsPaidDocument { get; set; }
        public bool IsPaidVideo { get; set; }
        public int DocumentId { get; set; }
        public string Select { get; set; }
        public string Create { get; set; }
        public string Delete { get; set; }
        public string Update { get; set; }
        public int DocumentType { get; set; }
    }
}