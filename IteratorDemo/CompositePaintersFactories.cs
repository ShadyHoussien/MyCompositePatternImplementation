using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IteratorDemo
{
    static class CompositePaintersFactories
    {
        public static IPainter Creategroup(IEnumerable<ProportionalPainter> painters) =>
            new CompositePainter<ProportionalPainter>(painters, (sqMeteres, sequence) =>
           {
                   TimeSpan time =
                       TimeSpan.FromHours(
                       1 /
                       sequence
                           .Where(painter => painter.IsAvailable)
                           .Select(painter => 1 / painter.EstimateTimeToPaint(sqMeteres).TotalHours)
                           .Sum());

                   double cost =
                       sequence
                           .Where(painter => painter.IsAvailable)
                           .Select(painter =>
                               painter.EstimateCompensation(sqMeteres) /
                               painter.EstimateTimeToPaint(sqMeteres).TotalHours *
                               time.TotalHours)
                            .Sum();

                   return new ProportionalPainter
                   {
                       DollarPerMeter = cost * time.TotalHours,
                       TimePerMeter = TimeSpan.FromHours(time.TotalHours / sqMeteres)
                   };
           });

        public static IPainter FindCheapestPainter(IEnumerable<IPainter> painters) =>
                new CompositePainter<IPainter>(painters,(sqMeters , sequence) => new Painters(sequence).GetAvailablePainter().GetCheapestOne(sqMeters));

        public static IPainter FindFastestPainter(IEnumerable<IPainter> painters) =>
                 new CompositePainter<IPainter>(painters, (sqMeters, sequence) => new Painters(sequence).GetAvailablePainter().GetFastestOne(sqMeters));

    }
}
