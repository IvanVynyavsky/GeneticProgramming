using GA_System.GenericOperation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GA_System
{
    class Program
    {
        static void Main(string[] args)
        {
            Parametrs parametrs = new Parametrs();

            parametrs.CrosoverProbability = 75;

            parametrs.MutationProbability = 30;

            parametrs.PopulationSize = 1000;

            parametrs.MaxOperation = 500;

            parametrs.MaxOperationWithoutImprovment = 100;

            parametrs.ElementesForBestTower = 4;


            parametrs.PlaybackRatioPercent = 0.18;

            parametrs.MinLifeTime = 0;

            parametrs.MaxLifeTime = 10;


            parametrs.PercentForEliteModel = 15;


            parametrs.MinDepth = 2;
            parametrs.MaxDepth = 4;


            string[] xy = File.ReadAllLines("data.txt");

            while (xy[0].Contains('.'))
            {
                xy[0] = xy[0].Replace('.', ',');
            }

            while (xy[1].Contains('.'))
            {
                xy[1] = xy[1].Replace('.', ',');
            }

            var x = xy[0].Split(" ");
            var y = xy[1].Split(" ");


            List<double> x_ = new List<double>();
            List<double> y_ = new List<double>();

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != "")
                {
                    x_.Add(Convert.ToDouble(x[i]));
                }
                if (y[i] != "")
                {
                    y_.Add(Convert.ToDouble(y[i]));
                }

            }

            parametrs.X = x_;
            parametrs.Y = y_;

            GA gA = new GA(parametrs);

        }
    }
}
