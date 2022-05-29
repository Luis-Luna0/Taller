using Taller.API.Datos.Entidades;
using Taller.API.Models;

namespace Taller.API.Helpers
{
    public interface IConverterHelper
    {
        Task<User> ToUserAsync(UserViewModel model, Guid imageId, bool isNew);

        UserViewModel ToUserViewModel(User user);

    }
}