using System;
using System.Collections.Generic;
using System.Text;

namespace GA_System.GenericOperation
{
    public class Selection
    {
        Random random;
        Parametrs parametrs;
        public Selection(Parametrs p)
        {
            random = new Random();
            this.parametrs = p;
        }
        public Induvid TournamentChoose(List<Induvid> prew_pop)
        {
            List<Induvid> lst = new List<Induvid>();
            for (int i = 0; i < parametrs.ElementesForBestTower; i++)
            {
                var rand = random.Next(0, prew_pop.Count);
                lst.Add(prew_pop[rand]);
            }
            Induvid best_ind = new Induvid(parametrs);
            best_ind = lst[0];
            //best_ind = (Induvid)lst[0].Clone();
            double best_value = best_ind.InduvidEvaluation;
            foreach (var elem in lst)
            {
                if (best_value > elem.InduvidEvaluation)
                {
                    best_value = elem.InduvidEvaluation;
                    best_ind = elem;
                    //best_ind = (Induvid)elem.Clone();
                }
            }
            return best_ind;
        }
        public List<Induvid> EliteModel(List<Induvid> pop)
        {
            List<Induvid> res = new List<Induvid>();
            //
            pop.Sort(delegate (Induvid ind1, Induvid ind2) { return ind1.InduvidEvaluation.CompareTo(ind2.InduvidEvaluation); });
            //
            int amount_of_elements = (int)(pop.Count * 1.0 * parametrs.PercentForEliteModel * 1.0 / 100);
            //
            for (int i = 0; i < amount_of_elements; i++)
                res.Add(pop[i]);
            //
            return res;
        }
    }
}
