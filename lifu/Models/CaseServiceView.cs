using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using lifu.Filters;
using SSB.Web.Mvc;

namespace lifu.Models
{
    public class CaseServiceView
    {
        
        [MaxLength(200)]
        [Display(Name = "索取資料")]
        public string Data { get; set; }

        [MaxLength(200)]
        [Display(Name = "預約看屋")]
        public string Reserved { get; set; }

        //ForeignKey
        [Display(Name = "預算")]
        public int CostId { get; set; }


        [ForeignKey("CostId")]
        public virtual CaseServiceCost Cost { get; set; }

        //ForeignKey
        [Display(Name = "坪數")]
        public int AreaId { get; set; }


        [ForeignKey("AreaId")]
        public virtual CaseServiceArea Area { get; set; }

        //ForeignKey
        [Display(Name = "格局")]
        public int TypeId { get; set; }


        [ForeignKey("TypeId")]
        public virtual CaseServiceType Type { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "性別")]
        public GenderType Gender { get; set; }

        [Display(Name = "生日")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? Birthday { get; set; }

        [MaxLength(10)]
        [Display(Name = "電話區碼")]
        public string AreaCode { get; set; }

        [MaxLength(50)]
        [Display(Name = "電話")]
        public string Telphone { get; set; }

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

        [MaxLength(10)]
        [Display(Name = "城市")]
        public string City { get; set; }

        [MaxLength(10)]
        [Display(Name = "區")]
        public string Division { get; set; }

        [MaxLength(10)]
        [Display(Name = "郵遞區號")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "地址")]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "如何得知此網站")]
        public string Information { get; set; }

       
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "驗證碼")]
        [ValidateMvcCaptcha(ErrorMessage = "驗證碼錯誤")]
        public string MvcCaptcha { get; set; }

        [MustBeTrue(Value = true, ErrorMessage = "請閱讀並同意本站 個資服務條款")]
        [Display(Name = "已閱讀並同意")]
        public bool IsRead { get; set; }


        [Display(Name = " 個資服務條款")]
        public MoreInformation MoerInformation { get; set; }
    }
}