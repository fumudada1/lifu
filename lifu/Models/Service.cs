using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace lifu.Models
{
    public class Service : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [MaxLength(10)]
        [Display(Name = "電話區碼(日)")]
        public string AreaCodeAM { get; set; }

        [MaxLength(50)]
        [Display(Name = "電話(日)")]
        public string TelphoneAM { get; set; }

        [MaxLength(10)]
        [Display(Name = "電話區碼(夜)")]
        public string AreaCodePM { get; set; }

        [MaxLength(50)]
        [Display(Name = "電話(夜)")]
        public string TelphonePM { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "手機")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [EmailAddress(ErrorMessage = "{0} 格式錯誤")]
        [MaxLength(200)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Display(Name = "已購戶")]
        public string User { get; set; }

        [Display(Name = "您的意見")]
        public string ShortMemo { get; set; }

        [Display(Name = "回覆")]
        public string Replay { get; set; }
    }
}