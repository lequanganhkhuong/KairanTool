using System.ComponentModel.DataAnnotations;

namespace KairanTool.Models
{
    public class Kairan
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}