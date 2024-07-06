using System.ComponentModel.DataAnnotations;

namespace ToDo.API.DAL.Entities
{
    public class Todo : AuditBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsComplete { get; set; }
    }
}
