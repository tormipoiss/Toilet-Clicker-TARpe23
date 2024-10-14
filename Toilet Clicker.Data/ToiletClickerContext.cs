using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Toilet_Clicker.Core.Domain;

namespace Toilet_Clicker.Data
{
	public class ToiletClickerContext : DbContext
	{
		public DbSet<Toilet> Toilets { get; set; }
	}
}
