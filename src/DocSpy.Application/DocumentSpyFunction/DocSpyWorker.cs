using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Threading;
/*
namespace DocSpy.DocumentSpyFunction
{
    public class DocSpyWorker : BackgroundWorkerBase
    {
        private readonly string _rootDirPath;
        private readonly string _receiveDirName;
        private readonly string _receiveDirPath;
        private readonly string _sendDirName;
        private readonly string _sendDirPath;
        private readonly string _fileFilter;
        private bool isStart;
        private Task monitorTask;
        protected IServiceScopeFactory ServiceScopeFactory { get; }

        public DocSpyWorker(IConfiguration configuration, ILogger<DocSpyWorker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _rootDirPath = configuration["FileMonitor:RootDir"];
            if (_rootDirPath.IsNullOrWhiteSpace())
                _rootDirPath = Directory.GetCurrentDirectory();

            _receiveDirName = configuration["FileMonitor:ReceiveDir"];
            if (_receiveDirName.IsNullOrWhiteSpace())
                _receiveDirName = "ReceiveDir";
            _receiveDirPath = Path.Combine(_rootDirPath, _receiveDirName);
            if (!Directory.Exists(_receiveDirPath))
                Directory.CreateDirectory(_receiveDirPath);

            _sendDirName = configuration["FileMonitor:SendDir"];
            if (_sendDirName.IsNullOrWhiteSpace())
                _sendDirName = "Send";

            _sendDirPath = Path.Combine(_rootDirPath, _sendDirName);
            if (!Directory.Exists(_sendDirPath))
                Directory.CreateDirectory(_sendDirPath);

            ServiceScopeFactory = serviceScopeFactory;

            _fileFilter = Path.Combine(_receiveDirName, "*.zip");
        }
        public override async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await base.StartAsync(cancellationToken);

            isStart = true;
            if (monitorTask == null)
                monitorTask = new Task(MonitorReceiveDir, cancellationToken);
            monitorTask.Start();
        }
        private void MonitorReceiveDir()
        {
            while (isStart)
            {
                MainAsync().GetAwaiter().GetResult();
            }
        }
        private async Task MainAsync()
        {
            //FileSystemWatcher _wat = new FileSystemWatcher(_fileFilter);

            var fileProvider = new PhysicalFileProvider(_rootDirPath);
            IChangeToken token = fileProvider.Watch(_fileFilter);

            var tcs = new TaskCompletionSource<object>();
            token.RegisterChangeCallback(state =>
            {
                ((TaskCompletionSource<object>)state).TrySetResult(null);
            }, tcs);
            await tcs.Task.ConfigureAwait(false);
            //var fileIfor = fileProvider.GetFileInfo(_receiveDirName);
            var fileIfor = fileProvider.GetDirectoryContents(_receiveDirName);

            var fileNames = fileProvider.GetDirectoryContents(_receiveDirName).Select(f => f.Name).JoinAsString("&");
            Logger.LogInformation(fileNames);
            AddFiletoDatabase(fileIfor);
        }

        private async void AddFiletoDatabase(IEnumerable<IFileInfo> fileIfor)
        {
            using (var scope = ServiceScopeFactory.CreateScope())
            {
                try
                {
                    var fileService = scope.ServiceProvider.GetRequiredService<IDocSpyService>();
                    await fileService.CreatemanyAsync(
                    //new CreateUpdateDocDto {
                    //    Name = fileIfor.Name,
                    //    Size = fileIfor.Length,
                    //    LastModified = fileIfor.LastModified });
 
                    fileIfor.Select(f => new CreateUpdateDocDto
                    { Name = f.Name,
                    Size = f.Length, 
                    LastModified = f.LastModified }));                  
                }
                catch (Exception)
                {
                    throw new NotImplementedException();
                }
            }
            
        }

        public override async Task StopAsync(CancellationToken cancellationToken = default)
        {
            isStart = false;
            await base.StopAsync(cancellationToken);
        }
    }
}
*/