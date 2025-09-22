using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass
{
    public class EmailTemplate
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string? Header { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public string? Body { get; set; }

        [MaxLength(100)]
        public string? Footer { get; set; }

        [Required]
        public int EmailTemplateType { get; set; }

        public string? AttachmentHtmlCode { get; set; }
    }
}
