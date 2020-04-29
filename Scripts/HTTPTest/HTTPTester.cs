using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using System;

public class HTTPTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HTTPRequest request = new HTTPRequest(new Uri("http://34.74.31.140/nodejsApp"), OnRequestFinished);
        request.Send();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnRequestFinished(HTTPRequest request, HTTPResponse response)
    {
        Debug.Log("Request Finished! Text received: " + response.DataAsText);
    }
}
