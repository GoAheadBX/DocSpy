using DocSpy.Documents;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace DocSpy.EntityFrameworkCore
{
    public static class DocSpyDbContextModelCreatingExtensions
    {
        public static void ConfigureDocSpy(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(DocSpyConsts.DbTablePrefix + "YourEntities", DocSpyConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
            builder.Entity<Document>(b =>
           {
               b.ToTable(DocSpyConsts.DbTablePrefix + "Document", DocSpyConsts.DbSchema);
               b.ConfigureByConvention();
           });
        }
    }
}