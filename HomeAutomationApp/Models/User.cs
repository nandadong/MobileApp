using System;

namespace HomeAutomationApp
{
public static class User
{
	private static string username;
	private static byte[] password;


	public static string getUsername()
	{
		return username;
	}

	public static void setUsername(string u)
	{
		username = u;	
	}

	public static void setPassword(string p)
	{
		password = GetHash(p);
	}

	public static byte[] getPassword()
	{
		return password;
	}

	public static byte[] GetHash(string inputString)
	{
		return null;
	}
}
}

