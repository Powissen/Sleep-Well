namespace SleepWell.Services
{
    public interface IForegroundService
    {
        void OpenAlarmClock();

        void StartMyForegroundService();

        void StopMyForegroundService();
    }
}
