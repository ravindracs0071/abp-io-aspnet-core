using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Sample.Demo.Localization;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Sample.Demo.EntityFrameworkCore
{
    public static class DemoDbContextModelCreatingExtensions
    {
        public static void ConfigureDemo(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(DemoConsts.DbTablePrefix + "YourEntities", DemoConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});

            builder.Entity<ApplicationLanguage>(b =>
            {
                b.ToTable(DemoConsts.DbTablePrefix + "Languages");
                b.ConfigureByConvention();
            });

            builder.Entity<ApplicationLanguageText>(b =>
            {
                b.ToTable(DemoConsts.DbTablePrefix + "LanguageTexts");
                b.ConfigureByConvention();
            });
        }
    }
}