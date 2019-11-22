using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace lifu.Models
{
    public class Cases : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "主題必填")]
        [MaxLength(200)]
        [Display(Name = "主題")]
        public string Subject { get; set; }

        //ForeignKey
        [Display(Name = "所屬區別")]
        public int AreaId { get; set; }


        [ForeignKey("AreaId")]
        public virtual Area Area { get; set; }

        [Display(Name = "狀態")]
        public CaseStatus Status { get; set; }

        [MaxLength(200)]
        [Display(Name = "建案內文圖片")]
        public string UpImageUrl { get; set; }

        [MaxLength(200)]
        [Display(Name = "建案形象圖片(167*115)")]
        public string ServerImageUrl { get; set; }

        [MaxLength(200)]
        [Display(Name = "樓層圖(307*169)")]
        public string AreaImageUrl { get; set; }

        [Display(Name = "描述")]
        public string Article { get; set; }

        [Display(Name = "興建日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "完工日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? EndDate { get; set; }

       

        [Display(Name = "人數")]
        public int Counter { get; set; }

        public virtual ICollection<CasePictures> Pictureses { get; set; }
        public virtual ICollection<CaseDiagram> Diagrams { get; set; }
    }
}