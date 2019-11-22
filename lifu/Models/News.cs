using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace lifu.Models
{
    public class News : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(200)]
        [Display(Name = "主題")]
        public string Subject { get; set; }

        [MaxLength(200)]
        [Display(Name = "圖片")]
        public string UpImageUrl { get; set; }

        [MaxLength(1000)]
        [Display(Name = "內容")]
        public string ShortMemo { get; set; }

        [Display(Name = "開始時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "結束時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "人數")]
        public int Counter { get; set; }

    }
}