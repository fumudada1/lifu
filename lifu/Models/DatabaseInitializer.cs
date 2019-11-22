using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace lifu.Models
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<BackendContext>
    {
        protected override void Seed(BackendContext context)
        {
            var unit = new Unit { Subject="資訊管理部",Alias = "IT",Poster = "admin",InitDate = DateTime.Now};
            var member = new Member { Account = "admin", Password = "/oIZh6cwrV5BIca+i8zTXNbS2aR89H+7az9pdWFMDvo=", PasswordSalt = "NqYBB/o=", Name = "總管理者", Gender = GenderType.男, Email = "admin@gmail.com", JobTitle = "總管理者", MyUnit = unit, Permission = "A01, B01, C01, D01, E01, F01, E02, E03, E04, C02", Poster = "admin", InitDate = DateTime.Now };
            context.Units.Add(unit);
            context.Members.Add(member);
            context.SaveChanges();

            //執行預存程序
            string createMemberProcedure =System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Config/CreateMember.sql")) ;
            context.Database.ExecuteSqlCommand(createMemberProcedure);
        }
    }
}