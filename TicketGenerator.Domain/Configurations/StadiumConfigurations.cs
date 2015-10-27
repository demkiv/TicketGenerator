using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGenerator.Domain.Entities;

namespace TicketGenerator.Domain.Configurations
{
	public class StadiumConfigurations : EntityTypeConfiguration<Stadium>
	{
		public StadiumConfigurations()
		{
			this.ToTable("Stadiums");
			this.HasKey(s => s.Id);
			this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasMany(s => s.Events).WithRequired(t => t.Stadium).WillCascadeOnDelete(false);
			this.HasMany(s => s.Sectors).WithRequired(t => t.Stadium).WillCascadeOnDelete(false);
		}

	}
}
