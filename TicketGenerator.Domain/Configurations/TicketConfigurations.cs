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
	public class TicketConfigurations : EntityTypeConfiguration<Ticket>
	{
		public TicketConfigurations()
		{
			this.ToTable("Tickets");
			this.HasKey(ti => ti.Id);
			this.Property(ti => ti.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
		}
	}
}
