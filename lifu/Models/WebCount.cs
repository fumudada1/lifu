using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace lifu.Models
{
    public class WebCount : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "主旨必填")]
        [MaxLength(100)]
        [Display(Name = "網站")]
        public string Subject { get; set; }

        [Display(Name = "次數")]
        public int TotalCount { get; set; }
    }
}