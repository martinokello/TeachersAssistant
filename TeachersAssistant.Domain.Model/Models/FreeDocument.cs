using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  TeachersAssistant.Domain.Model.Models
{
    public class FreeDocument
    {

        [Key]
        public int? FreeDocumentId { get; set; }
        [Required]
        public string FilePath { get; set; }
        [ForeignKey("Subject")]
        [Required]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
