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


		public async Task<List<Region>> GetRegionsAsync(string? filterOn = null, string? filterQuery = null,
			string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)

		{
			var getall = _nGDbContext.Regions.AsQueryable();

			//filtering
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

			//sorting

			if(string.IsNullOrWhiteSpace(sortBy) == false)
			{
				if(sortBy.Equals("name", StringComparison.OrdinalIgnoreCase))
				{
					getall = isAscending ? getall.OrderBy(item => item.Name) : getall.OrderByDescending(item => item.Name);
				}
			}

			//filtering
			var skipResult = (pageNumber - 1) * pageSize;

			return await getall.Skip(pageNumber).Take(pageSize).ToListAsync();
		}


		public async Task<Region> GetRegionByIdAsync(Guid Id)
		{
			 await _nGDbContext.Regions.FirstOrDefaultAsync(item => item.Id == Id);

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
