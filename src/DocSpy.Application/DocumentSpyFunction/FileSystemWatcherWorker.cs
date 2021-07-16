using DocSpy.Documents;
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

namespace DocSpy.DocumentSpyFunction
{
    class FileSystemWatcherWorker : BackgroundWorkerBase
    {
        private readonly string _rootDirPath;
        private readonly string _receiveDirName;
        private readonly string _receiveDirPath;
        private readonly string _sendDirName;
        private readonly string _sendDirPath;
        private readonly string _fileFilter;
        private Task monitorTask;
        private Dictionary<string, string> dic = new Dictionary<string, string>();
        protected IServiceScopeFactory ServiceScopeFactory { get; }
        public FileSystemWatcherWorker(IConfiguration configuration, ILogger<FileSystemWatcherWorker> logger, IServiceScopeFactory serviceScopeFactory)
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

            _fileFilter = Path.Combine(_receiveDirName, "*.*");

        }

       public override async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await base.StartAsync(cancellationToken);

            if (monitorTask == null)
                monitorTask = new Task(MonitorReceiveDir, cancellationToken);
            monitorTask.Start();
        }

        private void MonitorReceiveDir()
        {
            using var watcher = new FileSystemWatcher(_receiveDirPath);
            watcher.NotifyFilter = NotifyFilters.Attributes
                                     | NotifyFilters.CreationTime
                                     | NotifyFilters.DirectoryName
                                     | NotifyFilters.FileName
                                     | NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.Security
                                     | NotifyFilters.Size;

            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
            watcher.Error += OnError;

            watcher.Filter = "*.*";
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true; //开启提交事件


            //Console.ReadLine();
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            string filepath = e.FullPath;
            FileInfo fi = new FileInfo(filepath);
            if ((fi.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden || Path.GetExtension(e.FullPath) == ".tmp")
                return;

            DealWait(e);
        }

        private void DealWait(FileSystemEventArgs e)
        {
            string filepath = e.FullPath;
            FileInfo fi = new FileInfo(filepath);
            try
            {
                using (fi.OpenRead()) { };
                if (dic.ContainsKey(e.Name))
                {
                    if (dic[e.Name] == "Create")
                        DealOnCreated(e);
                    else if (dic[e.Name] == "Updata")
                        DealOnChanged(e);
                }
                else
                    DealOnChanged(e, true);
            }
            catch
            {
                //throw new NotImplementedException();
            }
            finally
            {
                if (dic.ContainsKey(e.Name))
                    if (dic[e.Name] == "Create")
                        DealWait(e);
            }
        }
        private async void DealOnChanged(FileSystemEventArgs e,bool IsFirst)
        {
            dic.Add(e.Name, "Updata");
            using (var scope = ServiceScopeFactory.CreateScope())
            {
                string filepath = e.FullPath;
                FileInfo fi = new FileInfo(filepath);
                var fileService = scope.ServiceProvider.GetRequiredService<IDocSpyService>();
                await fileService.CreateAsync(new CreateUpdateDocDto
                {
                    Name = e.Name,
                    Size = fi.Exists ? fi.Length : 0,
                    LastModified = fi.CreationTime,
                    OType = OperateType.Update
                }); 
            }
        }
        private void DealOnChanged(FileSystemEventArgs e)
        {                
            try
            {
             //   File.Move(e.FullPath, Path.Combine(_sendDirPath, e.Name), true);
                dic.Remove(e.Name);
            }
            catch { }
            
        }
        private void DealOnCreated(FileSystemEventArgs e)
        {
            try
            {
                File.Move(e.FullPath, Path.Combine(_sendDirPath, e.Name), true);
                dic.Remove(e.Name);
            }
            catch { }

        }
        private async void OnCreated(object sender, FileSystemEventArgs e)
        {
            string filepath = e.FullPath;
            FileInfo fi = new FileInfo(filepath);
            if ((fi.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden || Path.GetExtension(e.FullPath) == ".tmp")
                return;

            dic.Add(e.Name, "Create");

            using (var scope = ServiceScopeFactory.CreateScope())
            {
                try
                {
                    var fileService = scope.ServiceProvider.GetRequiredService<IDocSpyService>();
                    await fileService.CreateAsync(new CreateUpdateDocDto
                    {
                        Name = e.Name,
                        Size = fi.Exists ? fi.Length : 0,
                        LastModified = fi.CreationTime,
                        OType = OperateType.Create
                    });
                }
                catch (Exception)
                {
                    throw new NotImplementedException();
                }
            }

        }

        private async void OnDeleted(object sender, FileSystemEventArgs e)
        {
            string filepath = e.FullPath;
            FileInfo fi = new FileInfo(filepath);
            if ((fi.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden || Path.GetExtension(e.FullPath) == ".tmp")
                return;
            using (var scope = ServiceScopeFactory.CreateScope())
            {
                try
                {
                    var fileService = scope.ServiceProvider.GetRequiredService<IDocSpyService>();
                    await fileService.CreateAsync(new CreateUpdateDocDto
                    {
                        Name = e.Name,
                        //Size = 0,
                        LastModified = fi.CreationTime,
                        OType = OperateType.Delete
                    });
                }
                catch (Exception ex)
                {
                    Logger.LogWarning(ex.ToString());
                }
            }
        }
            

        private async void OnRenamed(object sender, RenamedEventArgs e)
        {
            string filepath = e.FullPath;
            FileInfo fi = new FileInfo(filepath);
            if ((fi.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden || Path.GetExtension(e.FullPath) == ".tmp" || Path.GetExtension(e.OldFullPath) == ".tmp")
                return;

            using (var scope = ServiceScopeFactory.CreateScope())
            {
                try
                {
                    var fileService = scope.ServiceProvider.GetRequiredService<IDocSpyService>();
                    await fileService.CreateAsync(new CreateUpdateDocDto
                    {
                        Name = e.Name,
                        Size = fi.Exists ? fi.Length : 0,
                        LastModified = fi.CreationTime,
                        OType = OperateType.Rename
                    });
                }
                catch (Exception ex)
                {
                    Logger.LogWarning(ex.ToString());
                }
            }
        }

           private static void OnError(object sender, ErrorEventArgs e) =>
                PrintException(e.GetException());

            private static void PrintException(Exception? ex)
            {
                if (ex != null)
                {
                    throw new NotImplementedException();
            }
            }

        public override async Task StopAsync(CancellationToken cancellationToken = default)
        {
            await base.StopAsync(cancellationToken);
        }

    }
}
