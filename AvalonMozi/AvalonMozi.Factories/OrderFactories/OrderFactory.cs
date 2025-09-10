using AvalonMozi.Domain.Orders;
using AvalonMozi.Domain.Users;
using AvalonMozi.Factories.OrderFactories.Dto;
using AvalonMozi.Factories.UserFactories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Factories.OrderFactories
{
    public interface IOrderFactory
    {
        BillingInformation ConvertBillingInfoDtoToEntity(BillingInformationDto dto);
        BillingInformationDto ConvertBillingInfoEntityToDto(BillingInformation Entity);
        List<BillingInformationDto> ConvertBillingInfoListEntityToDtoList(List<BillingInformation> Entity);
    }
    public class OrderFactory : IOrderFactory
    {
        public BillingInformation ConvertBillingInfoDtoToEntity(BillingInformationDto dto)
        {
            return new BillingInformation()
            {
                Address1 = dto.Address1,
                Address2 = dto.Address2,
                City = dto.City,
                CompanyName = dto.CompanyName,
                County = dto.County,
                Name = dto.Name,
                VATNumber = dto.VATNumber,
                ZipCode = dto.ZipCode,
                Deleted = false,
                TechnicalId = dto.TechnicalId
            };
        }

        public BillingInformationDto ConvertBillingInfoEntityToDto(BillingInformation Entity)
        {
            return new BillingInformationDto()
            {
                Address1 = Entity.Address1,
                Address2 = Entity.Address2,
                City = Entity.City,
                CompanyName = Entity.CompanyName,
                County = Entity.County,
                Name = Entity.Name,
                VATNumber = Entity.VATNumber,
                ZipCode = Entity.ZipCode,
                TechnicalId = Entity.TechnicalId
            };
        }

        public List<BillingInformationDto> ConvertBillingInfoListEntityToDtoList(List<BillingInformation> Entity)
        {
            var list = new List<BillingInformationDto>();
            foreach (var item in Entity)
            {
                list.Add(this.ConvertBillingInfoEntityToDto(item));
            }
            return list;
        }
    }
}
