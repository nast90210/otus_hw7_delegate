using System;
using System.IO;
using System.Threading.Tasks;
using System.Timers;

namespace otus_hw7_delegate
{
    public class DocumentsReceiver
    {
        public delegate void DocumentsReadyHandler(string message);
        public event DocumentsReadyHandler DocumentsReady;

        public delegate void TimedOutHandler(string message);
        public event TimedOutHandler TimedOut;

        private bool pasport, foto, zayav = false;

        public async Task StartAsync(string targetDirectory, double waitingInterval)
        {
            var timer = new Timer(waitingInterval);
            timer.Elapsed += (o, args) => TimedOut?.Invoke("Time is out.");
            timer.Start();
            
            var fileSystemWatcher = new FileSystemWatcher(targetDirectory);
            fileSystemWatcher.Created+= OnCreated;
            await Task.Run(() => fileSystemWatcher.WaitForChanged(WatcherChangeTypes.Created, (int)waitingInterval));
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            switch (e.Name)
            {
                case ("Паспорт.jpg"):
                    pasport = true;
                    break;
                case ("Заявление.txt"):
                    zayav = true;
                    break;
                case ("Фото.jpg"):
                    foto = true;
                    break;
            }
            
            if(pasport&& foto && zayav) DocumentsReady?.Invoke("Documents is ready.");
        }
    }
}
