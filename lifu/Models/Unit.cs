using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace lifu.Models
{
    public class Unit : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "主旨必填")]
        [MaxLength(100)]
        [Display(Name = "主旨")]
        public string Subject { get; set; }

        [Display(Name = "別名")]
        [MaxLength(50)]
        public string Alias { get; set; }


        [Required(ErrorMessage = "排序必填")]
        [Display(Name = "排序")]
        public int ListNum { get; set; }

        public ICollection<Member> Members { get; set; }

       
    }
}