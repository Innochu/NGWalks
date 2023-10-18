using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGWalksDomain.ModelDTO
{
	public class CreateRegionDTO
	{
		public string Code { get; set; } = String.Empty;
		public string Name { get; set; } = String.Empty;
		public string RegionImageUrl { get; set; } = String.Empty;
	}
}
