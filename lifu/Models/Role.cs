using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace lifu.Models
{
    public class Role : BackendBase
    {
        public Role()
        {
            Members=new List<Member>();
        }

        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "群組名稱必填")]
        [MaxLength(100)]
        [Display(Name = "群組名稱")]
        public string Subject { get; set; }

        [MaxLength(6000)]
        [Display(Name = "權限")]
        public string Permission { get; set; }

        [Display(Name = "隸屬成員")]
        public virtual ICollection<Member> Members { get; set; }

        [Display(Name = "別名")]
        [MaxLength(50)]
        public string Alias { get; set; }

    }
}