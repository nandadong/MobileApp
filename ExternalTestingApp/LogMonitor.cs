using System;
using System.IO;

namespace ExternalTestingApp
{
public class LogMonitor
{
	private readonly string _resultFile;

	public LogMonitor(string resultFile)
	{
		_resultFile = resultFile;
	}

	public void Start()
	{
		try
		{
			using(var sr = new StreamReader("TestFile.txt"))
			{
				String line = sr.ReadToEnd();
				Console.WriteLine(line);
			}
		}
		catch(Exception e)
		{
			Console.WriteLine("The file could not be read:");
			Console.WriteLine(e.Message);
		}
	}

}
}

