using Store.Domain.Entities.baseEntities;

namespace Store.Domain.Entities.HomePage
{
    public class Slides : BaseEntity
    {
        public string Src { get; set; }
        public string Link { get; set; }
    }
}
