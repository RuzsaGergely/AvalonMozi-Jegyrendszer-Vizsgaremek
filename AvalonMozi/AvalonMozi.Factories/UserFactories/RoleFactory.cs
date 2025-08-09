using AvalonMozi.Domain.Users;
using AvalonMozi.Factories.UserFactories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Factories.UserFactories
{

    public interface IRoleFactory
    {
        Role ConvertDtoToEntity(RoleDto dto);
        RoleDto ConvertEntityToDto(Role Entity);
        List<RoleDto> ConvertEntityListToDtoList(List<Role> roles);
    }

    public class RoleFactory : IRoleFactory
    {
        public Role ConvertDtoToEntity(RoleDto dto)
        {
            throw new NotImplementedException();

        }

        public RoleDto ConvertEntityToDto(Role Entity)
        {
            return new RoleDto()
            {
                DisplayName = Entity.DisplayName,
                TechnicalName = Entity.TechnicalName
            };
        }

        public List<RoleDto> ConvertEntityListToDtoList(List<Role> roles)
        {
            var returnList = new List<RoleDto>();
            foreach (var role in roles)
            {
                returnList.Add(this.ConvertEntityToDto(role));
            }
            return returnList;
        }
    }
}
