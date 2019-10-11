using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeacherAssistant.Models
{
    public interface IProductViewModel
    {
        int ProductId { get; set; }
        string ProductName { get; set; }
        string ProductDescription { get; set; } 
        decimal ProductPrice { get; set; }
        bool IsPaidDocument { get; set; }
        bool IsPaidVideo { get; set; }
        int DocumentId { get; set; }
        string Select { get; set; }
        string Create { get; set; }
        string Delete { get; set; }
        string Update { get; set; }
        int DocumentType { get; set; }
    }
    public class ProductSelectOrDeleteViewModel: IProductViewModel
    {
        [Required(ErrorMessage ="Product Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Product Id Required!")]
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "MartinLayooInc ProductName Default";
        public string ProductDescription { get; set; } = "MartinLayooInc ProductDescription Default";
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
    public class ProductCreateViewModel : IProductViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name Required!")]
        public string ProductName { get; set; } = "MartinLayooInc ProductName Default";

        [Required(ErrorMessage = "Product Description Required!")]
        public string ProductDescription { get; set; } = "MartinLayooInc ProductDescription Default";

        [Required(ErrorMessage = "Product Price Required!")]
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
    public class ProductUpdateViewModel : IProductViewModel
    {
        [Required(ErrorMessage = "Product Id Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Product Id Required!")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product Name Required!")]
        public string ProductName { get; set; } = "MartinLayooInc ProductName Default";

        [Required(ErrorMessage = "Product Description Required!")]
        public string ProductDescription { get; set; } = "MartinLayooInc ProductDescription Default";

        [Required(ErrorMessage = "Product Price Required!")]
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