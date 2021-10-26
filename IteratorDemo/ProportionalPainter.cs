using System;

namespace IteratorDemo
{
    class ProportionalPainter : IPainter
    {
        public bool IsAvailable => true;
        public TimeSpan TimePerMeter { get; set; }
        public double DollarPerMeter { get; set; }

        public double EstimateCompensation(double sqMeters) =>
            this.EstimateTimeToPaint(sqMeters).TotalSeconds * this.DollarPerMeter;

        public TimeSpan EstimateTimeToPaint(double sqMeters) =>
            TimeSpan.FromHours(this.TimePerMeter.TotalHours * sqMeters);
    }
}
