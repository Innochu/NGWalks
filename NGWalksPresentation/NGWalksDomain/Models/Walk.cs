namespace NGWalksDomain.Models
{
    public class Walk

    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double LengthInKm { get; set; }
        public string WalkImageUrl { get; set; } = string.Empty;
        public  Guid  DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        //navigation properties

        public Difficulty Difficulty { get; set; }
        public Regions Regions { get; set; }


    }
}