using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCore.Domain
{
    public class FilesToDatabase
    {
        public Guid Id { get; set; }
        public byte[] ImageData { get; set; }
        public Guid? RealEstateId { get; set; }
    }
}
