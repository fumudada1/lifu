using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace lifu.Models
{
    public class CaseDiagram : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "樓層名稱必填")]
        [MaxLength(200)]
        [Display(Name = "樓層名稱")]
        public string Subject { get; set; }

        //ForeignKey
        [Display(Name = "建案")]
        public int CaseId { get; set; }


        [ForeignKey("CaseId")]
        public virtual Cases Case { get; set; }

        [MaxLength(200)]
        [Display(Name = "平面圖")]
        public string UpPicUrl { get; set; }

        [Required(ErrorMessage = "排序必填")]
        [Display(Name = "排序")]
        public int ListNum { get; set; }

        [Display(Name = "人數")]
        public int Counter { get; set; }
    }
}