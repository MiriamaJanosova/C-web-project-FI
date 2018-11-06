using BL.DTOs.Common;

namespace BL.DTOs.Base

///////////////////////////////////////////////////
/// Informace o kategoriich pri pridavani itemu ///
///////////////////////////////////////////////////

{
    public class CategoryBasicInfo : DtoBase
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}