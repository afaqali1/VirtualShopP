using System.ComponentModel.DataAnnotations;

namespace VirtualShop.Models
{
    public class SharedModel
    {
        public SharedModel()
        {
            Id = Path.GetRandomFileName().Replace(".","");
            DbEntryTime = DateTime.Now;
        }

        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DbEntryTime { get; set; }
    }
}
