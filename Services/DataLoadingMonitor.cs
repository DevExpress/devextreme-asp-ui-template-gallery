namespace DevExtremeVSTemplateMVC.Services
{
    public class DataLoadingMonitor {
        private readonly TaskCompletionSource<bool> _tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        public Task WaitUntilLoadedAsync() => _tcs.Task;
        public void SetLoaded() => _tcs.TrySetResult(true);
    }
}
