using System;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface ITimeMoq
    {
        DateTime Time { get; set; }

        DateTime GetTime();
    }
}
