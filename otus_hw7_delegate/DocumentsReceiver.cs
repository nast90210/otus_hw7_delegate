using System;
using System.IO;
using System.Timers;

namespace otus_hw7_delegate
{
    public class DocumentsReceiver
    {
        private delegate void DocumentsReadyHandler(string message);
        private event DocumentsReadyHandler DocumentsReady;

        private delegate void TimedOutHandler(string message);
        private event TimedOutHandler TimedOut;

        private bool pasport, foto, zayav = false;

        public void Start(string targetDirectory, double waitingInterval)
        {
            var timer = new Timer(waitingInterval);
            timer.Elapsed += (o, args) => TimedOut?.Invoke("Time is out.");

            var fileSystemWatcher = new FileSystemWatcher(targetDirectory);
            fileSystemWatcher.Created+= OnCreated;
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
