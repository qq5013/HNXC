using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;

namespace THOK.Wms.DbModel.Mapping
{
    public class VIEW_PRODUCE_DATEMap : EntityTypeConfiguration<VIEW_PRODUCE_DATE>
    {
        public VIEW_PRODUCE_DATEMap()
        {
            // Primary Key
            this.HasKey(t => t.BILL_NO);

            // Properties
            this.Property(t => t.BILL_NO)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ITEM_NO)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);



            this.ToTable("VIEW_PRODUCE_DATE", "HNXC");

            this.Property(t => t.BILL_NO).HasColumnName("BILL_NO");
            this.Property(t => t.ITEM_NO).HasColumnName("ITEM_NO");
            this.Property(t => t.FIRST_DATE).HasColumnName("FIRST_DATE");
            this.Property(t => t.LAST_DATE).HasColumnName("LAST_DATE");

        }
    }
}
