using Medictionary.Models;
using Medictionary.DTOS;

namespace Medictionary.Utility
{
    public class HomeMapper
    {
        public static User Map(User convertFrom)
        {
            var convertTo = new User
            {
                UserId = Guid.NewGuid().ToString(),
                Name = convertFrom.Name,
                Address = convertFrom.Address,
                ContactNo = convertFrom.ContactNo,
                ProfileImage = convertFrom.ProfileImage
            };
            return convertTo;
        }
        public static List<User> Map(List<User> convertFrom)
        {
            var convertTo = new List<User>();
            foreach (var item in convertFrom)
            {
                convertTo.Add(HomeMapper.Map(item));
            }
            return convertTo;
        }
    }
}