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
public class fetchCalender : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UnityEngine.Debug.Log("Launching exe to fetch data from server for upcoming calendar entries");
        UpcomingCalenderListText();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private static StringBuilder calendarOutput = null;
    private static int numOutputLines = 0;
    private static string exeLocation = "C:/Users/Mayank/Downloads/Data/Debug/GCalConsole.exe";
    public static void UpcomingCalenderListText()
    {
        // Initialize the process and its StartInfo properties. 
        // The sort command is a console application that 
        // reads and sorts text input.

        Process calenderProcess;
        calenderProcess = new Process();
        calenderProcess.StartInfo.FileName = exeLocation;

        // Set UseShellExecute to false for redirection.
        calenderProcess.StartInfo.UseShellExecute = false;

        // Redirect the standard output of the sort command.   
        // This stream is read asynchronously using an event handler.
        calenderProcess.StartInfo.RedirectStandardOutput = true;
        calendarOutput = new StringBuilder("");

        // Set our event handler to asynchronously read the sort output.
        calenderProcess.OutputDataReceived += new DataReceivedEventHandler(CalendarUpcomingEventsOutputHandler);

        // Redirect standard input as well.  This stream 
        // is used synchronously.
        calenderProcess.StartInfo.RedirectStandardInput = true;

        // Start the process.
        calenderProcess.Start();

        // Use a stream writer to synchronously write the sort input.
        // StreamWriter sortStreamWriter = sortProcess.StandardInput;

        // Start the asynchronous read of the sort output stream.
        calenderProcess.BeginOutputReadLine();

        // Wait for the sort process to write the sorted text lines.
        calenderProcess.WaitForExit();

        if (numOutputLines > 0)
        {
            // Write the formatted and sorted output to the console.
            UnityEngine.Debug.Log(" Calender Upcoming event results = " + numOutputLines);
            
            UnityEngine.Debug.Log(calendarOutput);
        }
        else
        {
            Console.WriteLine(" No entries found.");
        }

        calenderProcess.Close();
    }
    private static void CalendarUpcomingEventsOutputHandler(object sendingProcess,
            DataReceivedEventArgs outLine)
    {
        // Collect the sort command output. 
        if (!String.IsNullOrEmpty(outLine.Data))
        {
            numOutputLines++;

            // Add the text to the collected output.
            calendarOutput.Append(" " +
                "[" + numOutputLines.ToString() + "] - " + outLine.Data);
        }
    }
}
