using System;
using System.Diagnostics;
using System.IO;

namespace ExternalTestingApp
{
class MainClass
{
	public static void Main(string[] args)
	{
		Console.WriteLine("Hello World!");
		Console.WriteLine("This Application can be used to ensure if the Mobile app can" +
		" be uploaded and opened on an Android device");

		if(args.Length != 3)
		{
			Console.WriteLine("Invalid Arguments.");
			Console.WriteLine("Usage: ExternalTestingApp <APK_File> <SCRIPT_FILE> <RESULT_FILE>");
			return;
		}

//			var device_id = getDeviceId ();

		Process proc = new Process();
		proc.StartInfo.FileName = "pwd";
		proc.StartInfo.UseShellExecute = false; 
		proc.StartInfo.RedirectStandardOutput = true;
		proc.Start();

		string pwd = "";
		while(!proc.StandardOutput.EndOfStream)
		{
			pwd += proc.StandardOutput.ReadLine();
		}

		var logMonitor = new LogMonitor(args[2]);

		cleanup();
		uploadApplication(args[0]);
		uploadStartScript(args[1]);
		installApplication(args[0]);
		startApplication(args[1]);

	}

	public static void cleanup()
	{
		Process proc = new Process();
		proc.StartInfo.FileName = "adb";
		proc.StartInfo.Arguments = "shell rm -r /sdcard/homeautomation";
		proc.StartInfo.UseShellExecute = false; 
		proc.StartInfo.RedirectStandardOutput = true;
		proc.Start();

		proc.WaitForExit();

		proc = new Process();
		proc.StartInfo.FileName = "adb";
		proc.StartInfo.Arguments = "shell mkdir -p /sdcard/homeautomation";
		proc.StartInfo.UseShellExecute = false; 
		proc.StartInfo.RedirectStandardOutput = true;
		proc.Start();

		proc.WaitForExit();

		if(proc.ExitCode != 0)
		{
			Console.WriteLine("Failed to create dirs");
			Environment.Exit(-1);
		}
	}

	public static void startApplication(string script)
	{
		Process proc = new Process();
		proc.StartInfo.FileName = "adb";
//			proc.StartInfo.Arguments = "shell sh /data/local/tmp/" + script;
		proc.StartInfo.Arguments = "shell sh /sdcard/homeautomation/" + script;
		proc.StartInfo.UseShellExecute = false; 
		proc.StartInfo.RedirectStandardOutput = true;
		proc.Start();

		while(!proc.StandardOutput.EndOfStream)
		{
			Console.WriteLine(proc.StandardOutput.ReadLine());
		}

		if(proc.ExitCode != 0)
		{
			Console.WriteLine("Failed to upload Script File");
			Environment.Exit(-1);
		}
	}

	public static void uploadStartScript(string scriptFile)
	{
		Process proc = new Process();
		proc.StartInfo.FileName = "adb";
//			proc.StartInfo.Arguments = "push " + scriptFile + " /data/local/tmp/";
		proc.StartInfo.Arguments = "push " + scriptFile + " /sdcard/homeautomation/";
		proc.StartInfo.UseShellExecute = false; 
		proc.StartInfo.RedirectStandardOutput = true;
		proc.Start();

		proc.WaitForExit();

		if(proc.ExitCode != 0)
		{
			Console.WriteLine("Failed to upload Script File");
			Environment.Exit(-1);
		}
	}

	public static void installApplication(string app)
	{
		Process proc = new Process();
		proc.StartInfo.FileName = "adb";
		proc.StartInfo.Arguments = "shell pm install /sdcard/homeautomation/" + app;
		proc.StartInfo.UseShellExecute = false; 
		proc.StartInfo.RedirectStandardOutput = true;
		proc.Start();

		while(!proc.StandardOutput.EndOfStream)
		{
			Console.WriteLine(proc.StandardOutput.ReadLine());
		}

		if(proc.ExitCode != 0)
		{
			Console.WriteLine("Failed to install APK File");
			Environment.Exit(-1);
		}
	}

	public static void uploadApplication(string apkFile)
	{
		Process proc = new Process();
		proc.StartInfo.FileName = "adb";
		proc.StartInfo.Arguments = "push " + apkFile + " /sdcard/homeautomation/";
		proc.StartInfo.UseShellExecute = false; 
		proc.StartInfo.RedirectStandardOutput = true;
		proc.Start();

		proc.WaitForExit();

		if(proc.ExitCode != 0)
		{
			Console.WriteLine("Failed to upload APK File");
			Environment.Exit(-1);
		}
	}

	public static string getDeviceId()
	{
		Process proc = new Process();
		proc.StartInfo.FileName = "adb";
		proc.StartInfo.Arguments = "devices";
		proc.StartInfo.UseShellExecute = false; 
		proc.StartInfo.RedirectStandardOutput = true;
		proc.Start();

		string adb_device_query = "";
		while(!proc.StandardOutput.EndOfStream)
		{
			adb_device_query += proc.StandardOutput.ReadLine();
			adb_device_query += '\n';
		}

		if(proc.ExitCode != 0)
		{
			Console.WriteLine("adb not found. Make sure it is included in the PATH.");
			Environment.Exit(-1);
		}

		var temp = adb_device_query.Split('\n');
		if(temp.Length < 2)
		{
			Console.WriteLine("No Device Found.");
			Environment.Exit(-1);
		}


		var device_id = temp[1].Split('\t')[0].Trim();
		return device_id;
	}
			
}
}
