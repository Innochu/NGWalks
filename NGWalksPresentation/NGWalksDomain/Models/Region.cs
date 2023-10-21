using System.ComponentModel.DataAnnotations;

namespace NGWalksDomain.Models
{
	public  class Region
	{
        public Guid  Id { get; set; }
		public string Code { get; set; } = String.Empty;
        public string Name { get; set; }= String.Empty;
        public string RegionImageUrl { get; set; } = String.Empty;

    }
}
