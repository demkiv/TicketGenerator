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
	public class SeatConfigurations : EntityTypeConfiguration<Seat>
	{
		public SeatConfigurations() 
		{
			this.ToTable("Seats");
			this.HasKey(s => s.Id);
			this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasMany(s => s.TicketInfos).WithRequired(t => t.Seat).WillCascadeOnDelete(false);
		}
		
	}
}
