using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Site_1.Domain.Entities.Commons;

namespace Test_Site_1.Domain.Entities.HomePage
{
    public class Slider:BaseEntity
    {
        public String Src {  get; set; }    
        public String Link { get; set; }
    }
}
