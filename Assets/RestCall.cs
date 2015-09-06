using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net;
using System.Text;


public class RestCall : MonoBehaviour {

	// Use this for initialization
	void Start () {
      //  authoize();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void authoize()
    {
        string url = "http://www.yahoo.co.in";
        // Create a request for the URL. 
        WebRequest request = WebRequest.Create(url);

        // If required by the server, set the credentials.
        // request.Credentials = CredentialCache.DefaultCredentials;
        // Get the response.
        WebResponse response = request.GetResponse();
        // Display the status.
        Console.WriteLine(((HttpWebResponse)response).StatusDescription);
        // Get the stream containing content returned by the server.
        Stream dataStream = response.GetResponseStream();
        // Open the stream using a StreamReader for easy access.
        StreamReader reader = new StreamReader(dataStream);
        // Read the content.
        string responseFromServer = reader.ReadToEnd();
        // Display the content.
        Debug.Log(responseFromServer);
        // Clean up the streams and the response.
        reader.Close();
        response.Close();
    }
}
