using Microsoft.EntityFrameworkCore;
using NGWalksApplication;
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


		public async Task<List<Region>> GetRegionsAsync(string? filterOn = null, string? filterQuery = null)
		{
			var getall = _nGDbContext.Regions.AsQueryable();
			if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
			{
				switch (filterOn.ToLowerInvariant())
				{
					case "name":
						getall = getall.Where(region => EF.Functions.Like(region.Name, "%" + filterQuery + "%"));
						break;
					case "code":
						getall = getall.Where(region => EF.Functions.Like(region.Code, "%" + filterQuery + "%"));
						break;
				}
			}
			return await getall.ToListAsync();
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

		public async Task<Region> UpdateRegionAsync(Guid Id, Region region)
		{
			var update = await _nGDbContext.Regions.FirstOrDefaultAsync(item => item.Id == Id);
			if (update == null)
				return null;

			update.Name = region.Name;
			update.Code = region.Code;
			update.RegionImageUrl = region.RegionImageUrl;

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
