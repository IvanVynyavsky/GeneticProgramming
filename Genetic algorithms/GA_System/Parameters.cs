using System;
using System.Collections.Generic;
using System.Text;

namespace GA_System
{
    [Serializable]
    public class Parametrs
    {
        public int PopulationSize { get; set; }
        public int CrosoverProbability { get; set; }
        public int MutationProbability { get; set; }
        public int MaxOperation { get; set; }
        public int MaxOperationWithoutImprovment { get; set; }
        public int ElementesForBestTower { get; set; }

        public double PlaybackRatioPercent { get; set; }
        public int MinLifeTime { get; set; }
        public int MaxLifeTime { get; set; }

        public int PercentForEliteModel { get; set; }

        public int MinDepth { get; set; }
        public int MaxDepth { get; set; }
        public List<double> X { get; set; }
        public List<double> Y { get; set; }

    }
}
