using Store.Domain.Entities.baseEntities;

namespace Store.Domain.Entities.HomePage
{
    public class HomePageImages : BaseEntity
    {
        public string Src { get; set; }
        public string Link { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }
    public enum ImageLocation
    {
        L1,
        L2,
        R1,
        CenterFullScrean,
        G1,
        G2,
    }
}
