
namespace VDOEasy.ApplicationCore.Models
{
    public class DataTableResultModel<T> where T : class, new()
    {
        public List<T> Data { get; set; }
        public int Rows { get; set; }
    }
}
