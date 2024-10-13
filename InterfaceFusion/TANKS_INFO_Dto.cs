using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceFusion
{
    public class TANKS_INFO_Dto
    {
        public int productNr { get; set; }
        public int volumeCapacity { get; set; }
        public string measureUnitType { get; set; }
        public string temperatureUnit { get; set; }
        public string fuelHeight { get; set; }
        public string fuelVolume { get; set; }
        public string temperature { get; set; }
        public string waterHeigth { get; set; }
        public string waterVolume { get; set; }
        public string alarms { get; set; }
        public string dateLastMeasure { get; set; }
        public string timeLastMeasure { get; set; }
    }
}
