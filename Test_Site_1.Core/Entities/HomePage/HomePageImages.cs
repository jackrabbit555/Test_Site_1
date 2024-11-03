using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Site_1.Domain.Entities.Commons;

namespace Test_Site_1.Domain.Entities.HomePage
{
    public  class HomePageImages:BaseEntity
    {
        public string Src {  get; set; }
        public string Link { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }

    public enum ImageLocation 
    {
        l1 = 0 ,
        l2 = 1 ,
        R1 = 2,
        CenterFullScreen = 4 , 
        G1 = 5 ,
        G2 = 6 ,    

    }
}
