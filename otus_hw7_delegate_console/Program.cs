using System;
using System.Threading.Tasks;
using otus_hw7_delegate;

namespace otus_hw7_delegate_console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DocumentsReceiver documentsReceiver = new DocumentsReceiver();
            documentsReceiver.TimedOut += Console.WriteLine; 
            documentsReceiver.DocumentsReady += Console.WriteLine;
            
            await documentsReceiver.StartAsync("/Users/mikhail/123",10000);
            
        }
    }
}
