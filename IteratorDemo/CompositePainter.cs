using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IteratorDemo
{
    class CompositePainter<TPainter> : IPainter
        where TPainter : IPainter
    {
        private IEnumerable<TPainter> Painters { get; }

        private Func<double , IEnumerable<TPainter> , IPainter> Reduce { get; }

        public bool IsAvailable => Painters.Any(painter => painter.IsAvailable);


        public CompositePainter(IEnumerable<TPainter> painters , Func<double, IEnumerable<TPainter>, IPainter> reduce)
        {
            Painters = painters.ToList();
            Reduce = reduce;
        }

        public TimeSpan EstimateTimeToPaint(double sqMeters) => 
            this.Reduce(sqMeters,this.Painters).EstimateTimeToPaint(sqMeters);

        public double EstimateCompensation(double sqMeters) =>
            this.Reduce(sqMeters,this.Painters).EstimateCompensation(sqMeters);

    }
}
