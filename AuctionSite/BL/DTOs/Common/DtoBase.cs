using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Common
{
    public abstract class DtoBase
    {
        [Editable(false)]
        public int Id { get; set; }
    }
}