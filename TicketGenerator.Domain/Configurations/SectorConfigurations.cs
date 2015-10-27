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
	public class SectorConfigurations : EntityTypeConfiguration<Sector>
	{
		public SectorConfigurations()
		{
			this.ToTable("Sectors");
			this.HasKey(s => s.Id);
			this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasMany(s => s.Seats).WithRequired(t => t.Sector).WillCascadeOnDelete(false);
		}

	}
}
