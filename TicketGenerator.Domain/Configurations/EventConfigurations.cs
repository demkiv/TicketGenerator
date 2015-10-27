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
	public class EventConfigurations : EntityTypeConfiguration<Event>
	{
		public EventConfigurations()
		{
			this.ToTable("Events");
			this.HasKey(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasMany(p => p.Tickets).WithRequired(t => t.Event).WillCascadeOnDelete(false);
		}
	}
}
