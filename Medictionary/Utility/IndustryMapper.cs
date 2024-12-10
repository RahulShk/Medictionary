using Medictionary.Models;
using Medictionary.DTOS;

namespace Medictionary.Utility
{
    public class IndustryMapper
    {
        public static Industry Map(IndustryDTO convertFrom)
        {
            var convertTo = new Industry
            {
                ID = Guid.NewGuid().ToString(),
                Name = convertFrom.Name,
                Description = convertFrom.Description,
                Location = convertFrom.Location
            };
            return convertTo;
        }
        public static List<Industry> Map(List<IndustryDTO> convertFrom)
        {
            var convertTo = new List<Industry>();
            foreach (var item in convertFrom)
            {
                convertTo.Add(IndustryMapper.Map(item));
            }
            return convertTo;
        }
    }
}