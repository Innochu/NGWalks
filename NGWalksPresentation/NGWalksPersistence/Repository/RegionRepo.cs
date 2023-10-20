using Microsoft.EntityFrameworkCore;
using NGWalksApplication;
using NGWalksDomain.ModelDTO;
using NGWalksDomain.Models;

namespace NGWalksPersistence.Repository
{
	public class RegionRepo : IRegionRepo

    {
		private readonly NGDbContext _nGDbContext;

		public RegionRepo(NGDbContext nGDbContext)
		{
			_nGDbContext = nGDbContext;
		}


		public async Task<List<Region>> GetRegionsAsync()
        {
			var getall = await _nGDbContext.Regions.ToListAsync();
			return getall;
			
        }

		public async Task<Region> GetRegionByIdAsync(Guid Id)
		{
			var getId = await _nGDbContext.Regions.FirstOrDefaultAsync(item => item.Id == Id);

			return getId;
		}

		public async Task<Region> CreateAsync(Region region)
		{
			await _nGDbContext.Regions.AddAsync(region);
			await _nGDbContext.SaveChangesAsync();
			return region;

		}

		public async Task<Region> UpdateRegionAsync(Guid Id, UpdateRegionDTO updateRegionDTO)
		{
			var update = await _nGDbContext.Regions.FirstOrDefaultAsync(item => item.Id == Id);
			if (update == null)
				return null;

			update.Name = updateRegionDTO.Name;
			update.Code = updateRegionDTO.Code;
			update.RegionImageUrl = updateRegionDTO.RegionImageUrl;

			await _nGDbContext.SaveChangesAsync();
			return update;
		}


		public async Task<Region> DeleteRegion(Guid Id)
		{
			var del = await _nGDbContext.Regions.FirstOrDefaultAsync(item => item.Id == Id);

			if(del != null)
			{
				 _nGDbContext.Regions.Remove(del);
				await _nGDbContext.SaveChangesAsync();
				return del;
			}

			return null;
		}
    }
}
