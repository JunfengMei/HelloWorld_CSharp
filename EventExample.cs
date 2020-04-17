using System;
using System.IO;


namespace BoilerEventApp
{
	public class Boiler
	{
		private int temperature;
		private int pressure;

		public Boiler(int t, int p)
		{
			temperature = t;
			pressure = p;
		}

		public int getTemperatiure()
		{
			return temperature;
		}
		public int getPressure()
		{
			return pressure;
		}

	}

	class DelegateBoilerEvent
	{
		public delegate void BoilerLogHandler(String status);

		public event BoilerLogHandler BoilerEventLog;

		public void LogProcess()
		{
			string remarks = "OK";
			Boiler boiler = new Boiler(100, 12);
			int t = boiler.getTemperatiure();
			int p = boiler.getPressure();
			if (t > 150 || t < 80 || p < 12 || p > 15)
			{
				remarks = "Need Maintenance!";
			}
			OnBoilerEventLog("Logging Info:\n");
			OnBoilerEventLog("Temperature:" + t + "\nPressure:" + p);
			OnBoilerEventLog("\nLogging Info:" + remarks);
		}

		protected void OnBoilerEventLog(string message)
		{
			if (BoilerEventLog != null)
			{
				BoilerEventLog(message);
			}
		}
	}

	class BoilerInfoLogger
	{
		FileStream fs;
		StreamWriter sw;
		public BoilerInfoLogger(string filename)
		{
			fs = new FileStream(filename, FileMode.Append, FileAccess.Write);
			sw = new StreamWriter(fs);
		}
		public void Logger(string info)
		{
			sw.WriteLine(info);
		}
		public void Close()
		{
			sw.Close();
			fs.Close();
		}
	}

	public class RecordBoilerInfo
	{
		static void Logger(string info)
		{
			Console.WriteLine(info);
		}

		static void Main(string[] args)
		{
			BoilerInfoLogger filelog = new BoilerInfoLogger("d:\\boiler.log");
			DelegateBoilerEvent boilerEvent = new DelegateBoilerEvent();
			boilerEvent.BoilerEventLog += new DelegateBoilerEvent.BoilerLogHandler(Logger);
			boilerEvent.BoilerEventLog += new DelegateBoilerEvent.BoilerLogHandler(filelog.Logger);
			boilerEvent.LogProcess();
			Console.ReadLine();
			filelog.Close();
		}
	}
}