namespace BackGroundServiceOnDemand.BackgroundJob
{
    public interface ITaskProgressService
    {
        void RegisterTask(Guid taskId);
        void UpdateProgress(Guid taskId, int progress);
        int GetProgress(Guid taskId);
    }
}
