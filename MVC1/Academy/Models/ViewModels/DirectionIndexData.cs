using System.Text.RegularExpressions;

namespace Academy.Models.ViewModels
{
    public class DirectionIndexData
    {
        public IEnumerable<Directions> Directions { get; set; }
        public IEnumerable<Groups> Groups { get; set; }
    }
}
