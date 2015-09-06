using UnityEngine;
using System.Collections;
using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

public class fetchEmail : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UnityEngine.Debug.Log("Launching exe to fetch data from server for getting user's emails");
        FetchEmailAsList();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private static StringBuilder emailListOutput = null;
    private static int numOutputLines = 0;
    private static string exeLocation = "C:/Users/Mayank/Documents/PennApps/GmailConsole/ConsoleApplication1/bin/Debug/ConsoleApplication1.exe";
    public static void FetchEmailAsList()
    {
        // Initialize the process and its StartInfo properties. 
        // The sort command is a console application that 
        // reads and sorts text input.

        Process emailProcess;
        emailProcess = new Process();
        emailProcess.StartInfo.FileName = exeLocation;

        // Set UseShellExecute to false for redirection.
        emailProcess.StartInfo.UseShellExecute = false;

        // Redirect the standard output of the sort command.   
        // This stream is read asynchronously using an event handler.
        emailProcess.StartInfo.RedirectStandardOutput = true;
        emailListOutput = new StringBuilder("");

        // Set our event handler to asynchronously read the sort output.
        emailProcess.OutputDataReceived += new DataReceivedEventHandler(EmailListOutputHandler);

        // Redirect standard input as well.  This stream 
        // is used synchronously.
        emailProcess.StartInfo.RedirectStandardInput = true;

        // Start the process.
        emailProcess.Start();

        // Use a stream writer to synchronously write the sort input.
        // StreamWriter sortStreamWriter = sortProcess.StandardInput;

        // Start the asynchronous read of the sort output stream.
        emailProcess.BeginOutputReadLine();

        // Wait for the sort process to write the sorted text lines.
        emailProcess.WaitForExit();

        if (numOutputLines > 0)
        {
            // Write the formatted and sorted output to the console.
            UnityEngine.Debug.Log(" No of Emails in Email List  = " + numOutputLines);

            UnityEngine.Debug.Log(emailListOutput);
        }
        else
        {
            Console.WriteLine(" No entries found.");
        }

        emailProcess.Close();
    }
    private static void EmailListOutputHandler(object sendingProcess,
            DataReceivedEventArgs outLine)
    {
        // Collect the sort command output. 
        if (!String.IsNullOrEmpty(outLine.Data))
        {
            numOutputLines++;

            // Add the text to the collected output.
            emailListOutput.Append(" " +
                "[" + numOutputLines.ToString() + "] - " + outLine.Data);
        }
    }
}
