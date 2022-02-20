using AmbienTown.Models.Interfaces;

namespace TimeSheet.Api.Models
{
    public class BaseModel : IEntity
    {
        public int Id { get; set; }
    }
}