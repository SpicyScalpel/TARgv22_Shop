using ShopCore.Domain;
using ShopCore.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCore.ServiceInterface
{
    public interface IFileServices
    {
        void FilesToApi(SpaceshipDto dto, Spaceship spaceship);
    }
}
