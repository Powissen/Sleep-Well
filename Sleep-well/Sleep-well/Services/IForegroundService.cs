using System;

namespace SleepWell.Services
{
    public interface IForegroundService
    {
        void StartMyForegroundService();

        void StopMyForegroundService();
    }
}
