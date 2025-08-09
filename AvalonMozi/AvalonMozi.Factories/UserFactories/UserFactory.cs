using AvalonMozi.Domain.Users;
using AvalonMozi.Factories.UserFactories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Factories.UserFactories
{
    public interface IUserFactory
    {
        User ConvertDtoToEntity(UserDto dto); 
        UserDto ConvertEntityToDto(User Entity); 
    }

    public class UserFactory : IUserFactory
    {
        private readonly IRoleFactory _roleFactory;
        public UserFactory(IRoleFactory roleFactory)
        {
            _roleFactory = roleFactory;
        }
        public User ConvertDtoToEntity(UserDto dto)
        {
            throw new NotImplementedException();

        }

        public UserDto ConvertEntityToDto(User Entity)
        {
            return new UserDto()
            {
                FirstName = Entity.FirstName,
                LastName = Entity.LastName,
                Email = Entity.Email,
                Phone = Entity.Phone,
                TechnicalId = Entity.TechnicalId,
                Roles = _roleFactory.ConvertEntityListToDtoList(Entity.Roles)
            };
        }
    }
}
