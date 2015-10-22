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
	public class PersonConfigurations : EntityTypeConfiguration<Person>
	{
		public PersonConfigurations() 
		{
			this.ToTable("Persons");
			this.HasKey(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasMany(p => p.TicketInfos).WithRequired(t => t.Owner).WillCascadeOnDelete(false);
		}
		
	}
}
