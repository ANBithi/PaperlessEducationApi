using System;

namespace Api.Commons
{
    public class MachineDateTime : IDateTime
    {
        public DateTime NowUTC => DateTime.Now.ToUniversalTime();
        public DateTime MaxUTC => DateTime.MaxValue.ToUniversalTime();
        public DateTime MinUTC => DateTime.MinValue.ToUniversalTime();
    }
}
