using System;
using System.Threading;
using System.Messaging;
using System.Collections;
using System.Configuration;

namespace ITSSMS
{
    static class Log
    {
        private static MessageQueue LoggingQueue;
        private static int LoggingVerbosity;
        private static object syncObject = new object();

        private static ManualResetEvent MRE = new ManualResetEvent(false);
        private static Thread WritingThread = null;
        private static Queue Messages = new Queue(100, 1.0f);
        private static bool IsClosed = false;

        public static void InitializeLogging()
        {
            LoggingQueue = new MessageQueue(@".\Private$\" + ConfigurationManager.AppSettings["LoggingQueue"], true, false, QueueAccessMode.Send);
            LoggingQueue.Formatter = new BinaryMessageFormatter();
            LoggingVerbosity = int.Parse(ConfigurationManager.AppSettings["LogVerbosity"]);

            if (WritingThread == null)
            {
                WritingThread = new Thread(new ThreadStart(WriteToMSMQueue));
                WritingThread.Name = "ITSSMSLogger";
                WritingThread.IsBackground = true;
                WritingThread.Start();
            }
        }

        public static void StopLogging()
        {
            IsClosed = true;
            LoggingQueue.Close();
        }

        public static void Information(string Message)
        {
            WriteToQueue("I: " + Message);
        }

        public static void Information(string MethodName, string Message)
        {
            WriteToQueue("I: {" + MethodName + "} " + Message);
        }

        public static void Information(string ThreadId, string MethodName, string Message)
        {
            WriteToQueue("I: {T_" + ThreadId + "} {" + MethodName + "} " + Message);
        }

        public static void Warning(string Message)
        {
            WriteToQueue("W: " + Message);
        }

        public static void Warning(string MethodName, string Message)
        {
            WriteToQueue("W: {" + MethodName + "} " + Message);
        }

        public static void Warning(string ThreadId, string MethodName, string Message)
        {
            WriteToQueue("W: {T_" + ThreadId + "} {" + MethodName + "} " + Message);
        }

        public static void Error(string Message)
        {
            WriteToQueue("E: " + Message);
        }

        public static void Error(string MethodName, string Message)
        {
            WriteToQueue("E: {" + MethodName + "} " + Message);
        }

        public static void Error(string ThreadId, string MethodName, string Message)
        {
            WriteToQueue("E: {T_" + ThreadId + "} {" + MethodName + "} " + Message);
        }

        private static void WriteToQueue(string Message)
        {            
            lock (Messages.SyncRoot)
            {
                Messages.Enqueue(DateTime.Now.ToString("HH:mm:ss.fffffff") + "|" + Message);
            }
            MRE.Set();
        }

        private static void WriteToMSMQueue()
        {
            string Message;

            while (true)
            {
                try
                {
                    if (IsClosed && Messages.Count < 1) break;

                    MRE.WaitOne();

                    lock (Messages.SyncRoot)
                    {
                        Message = (string)Messages.Dequeue();
                        if (Messages.Count < 1) MRE.Reset();
                    }

                    #region Actual Write

                    if (Message != null && Message.Length > 0)
                    {
                        LoggingQueue.Send(Message);
                    }

                    #endregion
                }
                catch (Exception)
                {
                }
            }
        }
    }
}