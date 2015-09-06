using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

public class LaunchJavaApplication : MonoBehaviour {
    public string root;
    // Use this for initialization
    void Start() {
        root = "C:/Users/Public/Documents/Unity Projects/VirtualDesk/java_support";
        UnityEngine.Debug.Log("Launching bat file ...........");
        //UnityEngine.Debug.Log(Application.dataPath);
        //System.Diagnostics.Process.Start(Application.dataPath + "JavaSupport/RestAdaper.jar");
        //ExecuteCommand();
        //demoProcess();
        SortInputListText();
        UnityEngine.Debug.Log("External Java Process started");
       // startNotepad();
    }

    // Update is called once per frame
    void Update() {

    }
    static void ExecuteCommand()
    {
        int exitCode;
        ProcessStartInfo processInfo;
        Process process = new Process();

        processInfo = new ProcessStartInfo("C:/Users/Mayank/Downloads/Data/Debug/GCalConsole.exe", "/c ");
        //processInfo = process.StartInfo;
        processInfo.CreateNoWindow = true;
        processInfo.UseShellExecute = false;
        //processInfo.FileName = @"C:/Users/Mayank/Downloads/Data/Debug/GCalConsole.exe";

        // *** Redirect the output ***
        processInfo.RedirectStandardError = true;
        processInfo.RedirectStandardOutput = true;
        // processInfo.Arguments = "-R C:\\";
        process = Process.Start(processInfo);
        //process.BeginOutputReadLine();
        process.WaitForExit();

        // *** Read the streams ***
        // Warning: This approach can lead to deadlocks, see Edit #2
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        exitCode = process.ExitCode;

        UnityEngine.Debug.Log("output>>" + (string.IsNullOrEmpty(output) ? "(none)" : output));
        UnityEngine.Debug.Log("error>>" + (string.IsNullOrEmpty(error) ? "(none)" : error));
        UnityEngine.Debug.Log("ExitCode: " + exitCode.ToString());
        process.Close();
    }
    public void startNotepad()
    {
        string path = @"f:\temp\data.txt";
        Process foo = new Process();
        // foo.StartInfo.FileName = "cmd.exe";
        foo.StartInfo.FileName = "C:/Users/Mayank/Downloads/Data/Debug/GCalConsole.exe";
        foo.Start();
    }
    public void demoProcess()
    {
        Process p = new Process();
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.CreateNoWindow = true;
        p.StartInfo.FileName = @"C:\Users\Mayank\Downloads\Data\Debug\GCalConsole.exe";
        p.StartInfo.Arguments = "-R C:\\";

        p.OutputDataReceived += new DataReceivedEventHandler(
            (s, e) =>
            {
                //System.Console.WriteLine(e.Data);
            }
        );
        p.ErrorDataReceived += new DataReceivedEventHandler((s, e) => { System.Console.WriteLine(e.Data); });

        p.Start();
        p.BeginOutputReadLine();
    }
    private static StringBuilder sortOutput = null;
    private static int numOutputLines = 0;
    public static void SortInputListText()
    {
        // Initialize the process and its StartInfo properties. 
        // The sort command is a console application that 
        // reads and sorts text input.

        Process sortProcess;
        sortProcess = new Process();
        sortProcess.StartInfo.FileName = "C:/Users/Mayank/Downloads/Data/Debug/GCalConsole.exe";

        // Set UseShellExecute to false for redirection.
        sortProcess.StartInfo.UseShellExecute = false;

        // Redirect the standard output of the sort command.   
        // This stream is read asynchronously using an event handler.
        sortProcess.StartInfo.RedirectStandardOutput = true;
        sortOutput = new StringBuilder("");

        // Set our event handler to asynchronously read the sort output.
        sortProcess.OutputDataReceived += new DataReceivedEventHandler(SortOutputHandler);

        // Redirect standard input as well.  This stream 
        // is used synchronously.
        sortProcess.StartInfo.RedirectStandardInput = true;

        // Start the process.
        sortProcess.Start();

        // Use a stream writer to synchronously write the sort input.
       // StreamWriter sortStreamWriter = sortProcess.StandardInput;

        // Start the asynchronous read of the sort output stream.
        sortProcess.BeginOutputReadLine();

        // Prompt the user for input text lines.  Write each  
        // line to the redirected input stream of the sort command.
        System.Console.WriteLine("Ready to sort up to 50 lines of text");

       
        Console.WriteLine("<end of input stream>");
        Console.WriteLine();

        // End the input stream to the sort command.
       // sortStreamWriter.Close();

        // Wait for the sort process to write the sorted text lines.
        sortProcess.WaitForExit();

        if (numOutputLines > 0)
        {
            // Write the formatted and sorted output to the console.
            UnityEngine.Debug.Log(" Sort results = "+numOutputLines+"sorted text line(s) ");
            Console.WriteLine("----------");
            UnityEngine.Debug.Log(sortOutput);
        }
        else
        {
            Console.WriteLine(" No input lines were sorted.");
        }

        sortProcess.Close();
    }
    private static void SortOutputHandler(object sendingProcess,
            DataReceivedEventArgs outLine)
    {
        // Collect the sort command output. 
        if (!String.IsNullOrEmpty(outLine.Data))
        {
            numOutputLines++;

            // Add the text to the collected output.
            sortOutput.Append(" " +
                "[" + numOutputLines.ToString() + "] - " + outLine.Data);
        }
    }

}
