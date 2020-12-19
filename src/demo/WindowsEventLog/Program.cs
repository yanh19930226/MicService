using System;
using System.Diagnostics;

namespace WindowsEventLog
{
    class Program
    {
        static void Main(string[] args)
        {
            Write();
            Console.WriteLine("Ok");
            
            Console.ReadKey();
        }


        public  static void Write()
        {
            EventLog eventLog = new EventLog();
            eventLog.Source = "MyEventLogTarget";
            eventLog.WriteEntry("This is a test message.", EventLogEntryType.Information);

            eventLog.Log = "MyEventLogTarget";
            foreach (EventLogEntry entry in eventLog.Entries)
            {
                Console.WriteLine(entry.Message);
            }
        }


        public static void Clear()
        {

            //if (EventLog.Exists("MyEventLogTarget"))
            //{
            //    EventLog.Delete("MyEventLogTarget");
            //}
        }
    }
}
