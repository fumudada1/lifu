using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace lifu.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Member : BackendBase
    {

        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "帳號")]
        [JsonProperty]
        public string Account { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [MaxLength(100)]
        [Display(Name = "密碼鹽")]
        public string PasswordSalt { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "名稱")]
        [JsonProperty]
        public string Name { get; set; }

        [Display(Name = "性別")]
        [JsonProperty]
        public GenderType Gender { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [EmailAddress(ErrorMessage = "{0} 格式錯誤")]
        [MaxLength(200)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-Mail")]
        [JsonProperty]
        public string Email { get; set; }

        [MaxLength(50)]
        [Display(Name = "照片")]
        public string MyPic { get; set; }

        [MaxLength(50)]
        [Display(Name = "職稱")]
        [JsonProperty]
        public string JobTitle { get; set; }

        //ForeignKey
        public int UnitId { get; set; }

        [Display(Name = "單位")]
        [ForeignKey("UnitId")]
        public virtual Unit MyUnit { get; set; }


        public virtual ICollection<Role> Roles { get; set; }

        [MaxLength(500)]
        [Display(Name = "權限")]
        [JsonProperty]
        public string Permission { get; set; }

        public bool AddMember()
        {
            BackendContext db = new BackendContext();

            var pid = new SqlParameter { ParameterName = "Id", Value = 0, Direction = ParameterDirection.Output };
            var result =
                db.Members.SqlQuery(
                    "dbo.CreateMember @Account,@Password,@PasswordSalt,@Name,@Email,@JobTitle,@Permission,@Poster,@UnitId,@Gender,@MyPic,@Id out",
                    new SqlParameter("Account", this.Account), new SqlParameter("Password", this.Password),
                    new SqlParameter("@PasswordSalt", this.PasswordSalt),
                    new SqlParameter("Name", this.Name),
                    new SqlParameter("Email", this.Email), new SqlParameter("JobTitle", this.JobTitle),
                    new SqlParameter("Permission", this.Permission),
                    new SqlParameter("Poster", "admin"), new SqlParameter("UnitId", this.UnitId),
                    new SqlParameter("Gender", this.Gender),
                    new SqlParameter("MyPic", this.MyPic ?? ""), pid).SingleOrDefault();
            return pid.Value.ToString() != "0";
        }


    }

   

    public class MemberLogin
    {
        [Required]
        [Display(Name = "使用者名稱")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }
    }

    public class MemberChangePassword
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "新密碼")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認新密碼")]
        [Compare("NewPassword", ErrorMessage = "新密碼與確認密碼不相符。")]
        public string ConfirmPassword { get; set; }
    }

}