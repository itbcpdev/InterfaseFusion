using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceFusion
{
    [Table("TANKS_INFO_HIST")]
    public class TANKS_INFO_HIST
    {
        public int TankNr { get; set; }
        public int ProductNr { get; set; }
        public string? ProductId { get; set; }
        public string? DatelastMeasure { get; set; }
        public string? TimeLastMeasure { get; set; }
        public string? Alarms { get; set; }
        public decimal FuelHeight { get; set; }
        public decimal FuelTemp { get; set; }
        public decimal FuelVolume { get; set; }
        public string? MeasureUnit { get; set; }
        public string? TemperatureUnit { get; set; }
        public decimal WaterHeight { get; set; }
        public decimal WaterVolume { get; set; }
        public DateTime DateTimeMeasure { get; set; }
    }
}
