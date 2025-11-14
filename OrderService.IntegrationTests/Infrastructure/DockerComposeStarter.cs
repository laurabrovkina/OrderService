using System.Diagnostics;

namespace OrderService.IntegrationTests.Infrastructure;

public class DockerComposeStarter : IDisposable
{
    private readonly Process _dockerComposeProcess;

    public DockerComposeStarter(string dockerComposeFilePath)
    {
        var info = new ProcessStartInfo
        {
            FileName = "docker-compose",
            Arguments = $"-f {dockerComposeFilePath} up -d test-db"
        };
        _dockerComposeProcess = new Process { StartInfo = info };
        _dockerComposeProcess.StartInfo.RedirectStandardError = true;
        _dockerComposeProcess.StartInfo.RedirectStandardOutput = true;
        _dockerComposeProcess.StartInfo.UseShellExecute = false;
        _dockerComposeProcess.EnableRaisingEvents = true;
        _dockerComposeProcess.OutputDataReceived += OutputHandler;
        _dockerComposeProcess.ErrorDataReceived += OutputHandler;
    }

    public void Start()
    {
        _dockerComposeProcess.Start();
        _dockerComposeProcess.BeginOutputReadLine();
        _dockerComposeProcess.BeginErrorReadLine();
        _dockerComposeProcess.WaitForExit();
    }

    private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
    {
        Console.WriteLine(outLine.Data);
        if (outLine.Data != null && outLine.Data.Contains("ERROR", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("docker-compose failed to start");
        }
    }
    
    public void Dispose()
    {
        _dockerComposeProcess.Dispose();
        GC.SuppressFinalize(this);
    }
}