namespace BackGroundServiceOnDemand.LongProcessTask
{
    public class LongRunningProcess : ILongRunningProcess
    {
        public async  Task  Run()
        {
           Console.WriteLine("Long running process started");
           await Task.Delay(20000);
        }
    }

}
