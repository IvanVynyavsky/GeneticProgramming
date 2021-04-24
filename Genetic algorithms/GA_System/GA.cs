
using GA_System.DynamicSizePopulation;
using GA_System.GenericOperation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GA_System
{
    public class GA
    {
        Parametrs parametrs;
        Crossover crossover;
        Selection selection;
        Mutation mutation;
        ProportialDistribution distribution;
        Induvid induvid;
        Random random;

        public GA(Parametrs p)
        {
            //
            random = new Random();
            parametrs = new Parametrs();
            parametrs = p;
            induvid = new Induvid(p);
            crossover = new Crossover();
            selection = new Selection(p);
            mutation = new Mutation();
            distribution = new ProportialDistribution(p);
            //
            Proceed();
        }

        public void Proceed()
        {
            List<double> IterationBest = new List<double>();
            List<double> ProgramBest = new List<double>();
            Console.WriteLine("Program start \n");
            //
            List<Induvid> population = new List<Induvid>();
            population = CreatePopulation();
            //
            population = distribution.GetInduvidsLifeTime(population);
            //
            int amount_operation = 0;
            int amount_operation_without_improvment = 0;
            //
            double best_result = PopulationEvaluate(population).Item1;
            Induvid best_ind = new Induvid(parametrs);
            best_ind = (Induvid)PopulationEvaluate(population).Item2.Clone();
            //
            Console.WriteLine("Iteration : " + (amount_operation));
            Console.WriteLine("Population best result : " + best_result + "\n");
            //
            IterationBest.Add(best_result);
            ProgramBest.Add(best_result);
            //
            while (StopCriterion(amount_operation, amount_operation_without_improvment))
            {
                double iteration_best_result;
                Induvid iteration_best_ind = new Induvid(parametrs);
                //
                population = DinamicProcced(population);
                //
                if (population.Count == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("ALL DIED");
                    Console.WriteLine();
                    amount_operation = parametrs.MaxOperation + 1;
                    amount_operation_without_improvment = parametrs.MaxOperationWithoutImprovment + 1;
                }
                else
                {
                    //
                    (iteration_best_result, iteration_best_ind) = PopulationEvaluate(population);
                    IterationBest.Add(iteration_best_result);
                    //
                    amount_operation++;
                    if (iteration_best_result >= best_result)
                        amount_operation_without_improvment++;
                    else
                        amount_operation_without_improvment = 0;
                    //
                    if (PopulationEvaluate(population).Item1 < best_result)
                        (best_result, best_ind) = PopulationEvaluate(population);
                    ProgramBest.Add(best_result);
                    //
                    Console.WriteLine("Iteration : " + (amount_operation));
                    Console.WriteLine("Population best result : " + iteration_best_result);
                    Console.WriteLine("Program best result ---  " + best_result + "\n");
                    Console.WriteLine(best_ind.Induvid_toString);
                }
                //
            }
            //
            Console.WriteLine();
            Console.WriteLine("Best result : " + best_result);
            Console.WriteLine(best_ind.Induvid_toString);
            //
            WriteData(ProgramBest, IterationBest);
        }
        public List<Induvid> CreatePopulation()
        {
            List<Induvid> population = new List<Induvid>();
            for (int i = 0; i < parametrs.PopulationSize; i++)
                population.Add(new Induvid(parametrs));
            return population;
        }
        public (double, Induvid) PopulationEvaluate(List<Induvid> pop)
        {
            Induvid best_ind = new Induvid(parametrs);
            double best_fun_value = double.MaxValue;
            best_ind = pop[0];
            foreach (var elem in pop)
            {
                if (best_fun_value > elem.InduvidEvaluation)
                {
                    best_fun_value = elem.InduvidEvaluation;
                    best_ind = elem;
                }
            }
            return (best_fun_value, best_ind);
        }
        public Induvid Mutation(Induvid ind)
        {
            //Console.WriteLine(ind.Induvid_toString);
            //Console.WriteLine(ind.InduvidEvaluation);
            //return (Induvid)mutation.Mutation_(ind).Clone();
            if (random.Next(101) <= parametrs.MutationProbability)
            {
               // Console.WriteLine(ind.Induvid_toString);
               // Console.WriteLine(ind.InduvidEvaluation);
                return (Induvid)mutation.Mutation_(ind).Clone();
            }
            else
                return ind;
        }
        public Induvid Crossover(Induvid ind, List<Induvid> prew_pop)
        {
            //Console.WriteLine("Dad : ");
            //Console.WriteLine(ind.Induvid_toString);
            //Console.WriteLine(ind.InduvidEvaluation);

            //


            //Console.WriteLine("Mom : ");
            //Console.WriteLine(mom.Induvid_toString);
            //Console.WriteLine(mom.InduvidEvaluation);

           
            if (random.Next(101) <= parametrs.CrosoverProbability)
            {
                Induvid dad = ind;
                Induvid mom = selection.TournamentChoose(prew_pop);
                return (Induvid)crossover.Crossover_(dad, mom).Clone();
            }
            else
                return ind;
        }
        public List<Induvid> DinamicProcced(List<Induvid> prew_pop)
        {
            //foreach(var elem in prew_pop)
            //{
            //    Console.WriteLine(elem.tree.result);
            //}
            //Console.ReadLine();

            List<Induvid> new_pop = new List<Induvid>();
            //
            int survived_induvid = 0;
            int dead_induvid = 0;
            int number_of_new_induvid = (int)(prew_pop.Count * 1.0 * parametrs.PlaybackRatioPercent);
            //
            foreach (var elem in prew_pop)
            {
                if (elem.Age <= elem.LifeTime)
                {
                    elem.Age++;
                    new_pop.Add(elem);
                    survived_induvid++;
                }
                else
                    dead_induvid++;
            }
            //
            double MinEval;
            double AverageEval;
            double nu;
            (MinEval, AverageEval, nu) = distribution.GetLifeTimeParametrs(prew_pop);
            //


            List<double> eval_ = new List<double>();
            List<double> eval__ = new List<double>();
            foreach (var elem in prew_pop)
            {
                eval_.Add(elem.InduvidEvaluation * (-1));
            }
            double sum = 0;
            double temp = 0;
            foreach (var elem in eval_)
            {
                temp = elem - eval_.Min() + 1;
                sum += temp;
                eval__.Add(temp);
            }
            for (int i = 0; i < prew_pop.Count; i++)
            {
                prew_pop[i].Probability = eval__[i] / sum;
            }


            temp = 0;

            for (int i = 0; i < prew_pop.Count; i++)
            {
                prew_pop[i].SectorStart = temp;
                temp += prew_pop[i].Probability;
                prew_pop[i].SectotEnd = temp;
            }

            //for (int i = 0; i < prew_pop.Count; i++)
            //{
            //    Console.WriteLine("Sector strart : " + prew_pop[i].SectorStart);
            //    Console.WriteLine(prew_pop[i].Probability);
            //    Console.WriteLine("Sector end : " + prew_pop[i].SectotEnd);
            //}
            //Console.ReadLine();
            //
            for (int i = 0; i < number_of_new_induvid; i++)
            {
                //var rand = random.NextDouble();
                //var ind = (Induvid)ChooseBySector(rand, prew_pop).Clone();
                var ind = (Induvid)selection.TournamentChoose(prew_pop).Clone();
 
                ind = Crossover(ind, prew_pop);
                //Console.WriteLine(ind.Induvid_toString);
                //Console.WriteLine(ind.InduvidEvaluation);
                //Console.ReadLine();
                ind = Mutation(ind);
               
               
                distribution.GetInduvidLifeTime(ind, MinEval, AverageEval, nu);
                new_pop.Add(ind);
                //Console.WriteLine();
                //Console.ReadLine();
            }
            //
            Console.WriteLine("Number of individuals who survived : " + survived_induvid);
            Console.WriteLine("Number of individuals who died : " + dead_induvid);
            Console.WriteLine("Number of new individuals : " + number_of_new_induvid);
            Console.WriteLine("Population size : " + new_pop.Count);
            Console.WriteLine();
            return new_pop;
        }
        public bool StopCriterion(int amount_of_iteration, int amount_of_iteration_without_improvment)
        {
            return amount_of_iteration < parametrs.MaxOperation && amount_of_iteration_without_improvment < parametrs.MaxOperationWithoutImprovment;
        }
        public Induvid ChooseBySector(double rand, List<Induvid> pop)
        {
            foreach (var ind in pop)
            {
                if (ind.SectorStart < rand && ind.SectotEnd >= rand)
                {
                    return ind;
                }
            }
            return selection.TournamentChoose(pop);
        }

        public void WriteData(List<double> ProgramBest, List<double> IterationBest)
        {
            File.WriteAllText("IterationBestResult.txt", string.Empty);
            File.WriteAllText("IterationBestResult.txt", String.Empty);
            string iteration_best = "";
            foreach (var elem in IterationBest)
            {
                iteration_best += elem.ToString("0.00000", System.Globalization.CultureInfo.InvariantCulture) + " ";
            }
            File.WriteAllText("IterationBestResult.txt", iteration_best);
            //
            File.WriteAllText("ProgramBestResult.txt", string.Empty);
            File.WriteAllText("ProgramBestResult.txt", String.Empty);
            string program_best = "";
            foreach (var elem in ProgramBest)
            {
                program_best += elem.ToString("0.00000", System.Globalization.CultureInfo.InvariantCulture) + " ";
            }
            File.WriteAllText("ProgramBestResult.txt", program_best);
        }
    }

}
