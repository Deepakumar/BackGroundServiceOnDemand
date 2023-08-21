using BackGroundServiceOnDemand.BackgroundJob;
using BackGroundServiceOnDemand.LongProcessTask;
using Microsoft.AspNetCore.Mvc;

namespace BackGroundServiceOnDemand.Controllers
{
    public class BackgroundJobController : Controller
    {
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;
        private readonly ILongRunningProcess _longRunningProcess;
        private readonly ITaskProgressService _taskProgressService;

        public BackgroundJobController(IBackgroundTaskQueue backgroundTaskQueue, ILongRunningProcess longRunningProcess, ITaskProgressService taskProgressService)
        {
            _backgroundTaskQueue = backgroundTaskQueue;
            _longRunningProcess = longRunningProcess;
            _taskProgressService = taskProgressService;
        }

        [HttpPost("runtask")]
        public IActionResult Index()
        {
            Guid taskId = Guid.NewGuid();
            _taskProgressService.RegisterTask(taskId);

            _backgroundTaskQueue.QueueBackgroundWorkItem(async token =>
            {
                await _longRunningProcess.Run();
                _taskProgressService.UpdateProgress(taskId, 10);

                await _longRunningProcess.Run();
                _taskProgressService.UpdateProgress(taskId, 20);

                await _longRunningProcess.Run();
                _taskProgressService.UpdateProgress(taskId, 30);

                await _longRunningProcess.Run();
                _taskProgressService.UpdateProgress(taskId, 40);

                await _longRunningProcess.Run();
                _taskProgressService.UpdateProgress(taskId, 50);
            });

            return Ok(new { TaskId = taskId });
        }

        [HttpGet("task-progress/{taskId}")]
        public IActionResult GetTaskProgress(Guid taskId)
        {
            int progress = _taskProgressService.GetProgress(taskId);

            if (progress == -1)
            {
                return NotFound();
            }

            return Ok(new { Progress = progress });
        }
    }
}
