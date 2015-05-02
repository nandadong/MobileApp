using System;
using CryptSharp;

namespace HomeAutomationApp
{
public static class User
{
	private static string username;
	private static string password;
	private static string deviceID;


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

	public static string getPassword()
	{
		return password;
	}

	public static string getDeviceID()
	{
		return deviceID;
	}

	public static void setDeviceID(string id)
	{
		deviceID = id;
	}

	public static string GetHash(string inputString)
	{
		string cryptpass = Crypter.Sha256.Crypt(inputString);
		return cryptpass;
	}

	public static bool TestCrypt(string input)
	{
		bool matches = Crypter.CheckPassword(input, getPassword());
		return matches;
	}
}
}

