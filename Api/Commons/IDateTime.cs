using System;

namespace Api.Commons
{
    public interface IDateTime
    {
        DateTime NowUTC { get; }
        DateTime MaxUTC { get; }
        DateTime MinUTC { get; }
    }
}
