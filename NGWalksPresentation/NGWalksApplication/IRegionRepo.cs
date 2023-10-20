using NGWalksDomain.ModelDTO;
using NGWalksDomain.Models;

namespace NGWalksApplication
{
	public interface IRegionRepo
	{
		public Task<List<Region>> GetRegionsAsync();

		public Task<Region> GetRegionByIdAsync(Guid id);
		public Task<Region> CreateAsync(Region region);
		public Task<Region> UpdateRegionAsync(Guid Id, UpdateRegionDTO updateRegionDTO);
		public  Task<Region> DeleteRegion(Guid Id);
	}
}