using DeskJr.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskJr.Service.Abstract
{
    public interface IOrganizationUnitService
    {
        Task<IEnumerable<OrganizationUnitDto>> GetOrganizationUnitsAsync();
    }
}
