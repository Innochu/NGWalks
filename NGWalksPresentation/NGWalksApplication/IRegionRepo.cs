using NGWalksDomain.ModelDTO;
using NGWalksDomain.Models;

namespace NGWalksApplication
{
	public interface IRegionRepo
	{
		public Task<List<Region>> GetRegionsAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending, int pageNumber, int pageSize);
		//string? filterOn, string? filterQuery is used to impliment filtering 
		//string? sortBy, bool isAscending is used to implemet sorting in ascending as default
		//int pageNumber, int pageSize are used to implememnt Pagination

		public Task<Region> GetRegionByIdAsync(Guid id);
		public Task<Region> CreateAsync(Region region);
		public Task<Region> UpdateRegionAsync(Guid Id, Region region);
		public  Task<Region> DeleteRegion(Guid Id);
	}
}