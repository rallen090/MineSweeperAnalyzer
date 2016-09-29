<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Activities.Core.Presentation.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Activities.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Activities.DurableInstancing.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Activities.Presentation.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.AddIn.Contract.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.AddIn.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Collections.Concurrent.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Collections.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ComponentModel.Annotations.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ComponentModel.Composition.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\system.componentmodel.composition.registration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ComponentModel.DataAnnotations.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ComponentModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ComponentModel.EventBasedAsync.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.Install.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Core.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.Parallel.dll</Reference>
  <NuGetReference>HtmlAgilityPack</NuGetReference>
  <NuGetReference>MedallionShell</NuGetReference>
  <Namespace>HtmlAgilityPack</Namespace>
  <Namespace>Medallion.Shell</Namespace>
  <Namespace>Medallion.Shell.Streams</Namespace>
  <Namespace>Microsoft.CSharp</Namespace>
  <Namespace>Microsoft.Win32.SafeHandles</Namespace>
  <Namespace>System</Namespace>
  <Namespace>System.CodeDom</Namespace>
  <Namespace>System.CodeDom.Compiler</Namespace>
  <Namespace>System.Collections.Concurrent</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.Collections.ObjectModel</Namespace>
  <Namespace>System.Collections.Specialized</Namespace>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.ComponentModel.Design</Namespace>
  <Namespace>System.ComponentModel.Design.Serialization</Namespace>
  <Namespace>System.Configuration</Namespace>
  <Namespace>System.Diagnostics</Namespace>
  <Namespace>System.Diagnostics.CodeAnalysis</Namespace>
  <Namespace>System.Diagnostics.Eventing</Namespace>
  <Namespace>System.Diagnostics.Eventing.Reader</Namespace>
  <Namespace>System.Diagnostics.PerformanceData</Namespace>
  <Namespace>System.Dynamic</Namespace>
  <Namespace>System.IO</Namespace>
  <Namespace>System.IO.Compression</Namespace>
  <Namespace>System.IO.MemoryMappedFiles</Namespace>
  <Namespace>System.IO.Pipes</Namespace>
  <Namespace>System.IO.Ports</Namespace>
  <Namespace>System.Linq</Namespace>
  <Namespace>System.Linq.Expressions</Namespace>
  <Namespace>System.Management.Instrumentation</Namespace>
  <Namespace>System.Media</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Cache</Namespace>
  <Namespace>System.Net.Configuration</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Mail</Namespace>
  <Namespace>System.Net.Mime</Namespace>
  <Namespace>System.Net.NetworkInformation</Namespace>
  <Namespace>System.Net.Security</Namespace>
  <Namespace>System.Net.Sockets</Namespace>
  <Namespace>System.Net.WebSockets</Namespace>
  <Namespace>System.Reflection</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>System.Runtime.InteropServices</Namespace>
  <Namespace>System.Runtime.InteropServices.ComTypes</Namespace>
  <Namespace>System.Runtime.Versioning</Namespace>
  <Namespace>System.Security</Namespace>
  <Namespace>System.Security.AccessControl</Namespace>
  <Namespace>System.Security.Authentication</Namespace>
  <Namespace>System.Security.Authentication.ExtendedProtection</Namespace>
  <Namespace>System.Security.Authentication.ExtendedProtection.Configuration</Namespace>
  <Namespace>System.Security.Claims</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>System.Security.Cryptography.X509Certificates</Namespace>
  <Namespace>System.Security.Permissions</Namespace>
  <Namespace>System.Text.RegularExpressions</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Timers</Namespace>
  <Namespace>System.Web</Namespace>
  <Namespace>System.Windows.Input</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
</Query>

async Task Main()
{
	var cancellationSource = new CancellationTokenSource();
	var executable = @"C:\dev\MineSweeper\MineSweeper\bin\Debug\MineSweeper.exe";
	var command = Command.Run(executable, new []{ "10", "10" });
	var processOutput = this.PrintStandardOutput(command, cancellationSource.Token);
	await command.StandardInput.WriteLineAsync("Test Input");
	await command.StandardInput.WriteLineAsync("EXIT");
	var completedTask = await Task.WhenAny(new[] { command.Task, processOutput });
	cancellationSource.Cancel();
	command.Kill();
}

// Define other methods and classes here
public async Task PrintStandardOutput(Command command, CancellationToken token)
{
	string line;
	while ((line = await command.StandardOutput.ReadLineAsync()) != null)
	{
		line.Dump();
		if (token.IsCancellationRequested) { break; }
	}
}