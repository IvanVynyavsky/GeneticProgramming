using GA_System.GenericOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_System.DynamicSizePopulation
{
    public class ProportialDistribution
    {
        Parametrs parametrs;
        public ProportialDistribution(Parametrs p)
        {
            this.parametrs = p;
        }
        public (double, double, double) GetLifeTimeParametrs(List<Induvid> pop)
        {
            List<double> evals1 = new List<double>();
            foreach (var elem in pop)
                evals1.Add(elem.InduvidEvaluation * (-1.0));
            //
            double MinEval = evals1.Min();
            //
            List<double> evals2 = new List<double>();
            foreach (var elem in evals1)
                evals2.Add(elem - MinEval + 1);
            //
            double AverageEval = evals2.Average();
            //
            double nu = (1.0 / 2.0) * (1.0 * (parametrs.MaxLifeTime - parametrs.MinLifeTime));
            return (MinEval, AverageEval, nu);
        }
        public List<Induvid> GetInduvidsLifeTime(List<Induvid> pop)
        {
            List<double> evals1 = new List<double>();
            foreach (var elem in pop)
                evals1.Add(elem.InduvidEvaluation * (-1.0));
            //
            List<double> evals2 = new List<double>();
            foreach (var elem in evals1)
                evals2.Add(elem - evals1.Min() + 1);
            //
            double AverageEval = evals2.Average();
            //
            double nu = (1.0 / 2.0) * (1.0 * (parametrs.MaxLifeTime - parametrs.MinLifeTime));
            //
            for (int i = 0; i < pop.Count; i++)
                pop[i].LifeTime = Math.Min(parametrs.MinLifeTime + nu * evals2[i] / AverageEval, parametrs.MaxLifeTime);
            //
            return pop;
        }
        public void GetInduvidLifeTime(Induvid induvid, double MinEval, double AverageEval, double nu)
        {
            double eval = induvid.InduvidEvaluation * (-1.0) - MinEval + 1;
            induvid.LifeTime = Math.Min(parametrs.MinLifeTime + nu * eval / AverageEval, parametrs.MaxLifeTime);
        }
    }
}
