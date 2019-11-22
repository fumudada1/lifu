using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web.DataAccess;

namespace lifu.Models
{
    public class BackendContext : DbContext
    {
        // 您可以將自訂程式碼新增到這個檔案。變更不會遭到覆寫。
        // 
        // 如果您要 Entity Framework 每次在您變更模型結構描述時
        // 自動卸除再重新產生資料庫，請將下列
        // 程式碼新增到 Global.asax 檔案的 Application_Start 方法中。
        // 注意: 這將隨著每次模型變更而損毀並重新建立您的資料庫。
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<gowatershop.Models.UnitContext>());

        public BackendContext()
            : base("name=DefaultConnection")
        {
        }
      

        public DbSet<Unit> Units { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Role> Roles { get; set; }

        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
           
        }

        public DbSet<News> News { get; set; }

        public DbSet<CaseDiagram> CaseDiagrams { get; set; }

        public DbSet<Cases> Cases { get; set; }

        public DbSet<CasePictures> CasePictures { get; set; }

        public DbSet<CaseService> CaseServices { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Area> Areas { get; set; }

        public DbSet<CaseServiceCost> CaseServiceCosts { get; set; }

        public DbSet<CaseServiceArea> CaseServiceAreas { get; set; }

        public DbSet<CaseServiceType> CaseServiceTypes { get; set; }

        public DbSet<WebCount> WebCounts { get; set; }

    }
}
