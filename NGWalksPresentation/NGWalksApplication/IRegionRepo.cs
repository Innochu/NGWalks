using NGWalksDomain.ModelDTO;
using NGWalksDomain.Models;

namespace NGWalksApplication
{
	public interface IRegionRepo
	{
		public Task<List<Region>> GetRegionsAsync(string? filterOn, string? filterQuery);
		//string? filterOn, string? filterQuery is used to impliment filtering 

		public Task<Region> GetRegionByIdAsync(Guid id);
		public Task<Region> CreateAsync(Region region);
		public Task<Region> UpdateRegionAsync(Guid Id, Region region);
		public  Task<Region> DeleteRegion(Guid Id);
	}
}