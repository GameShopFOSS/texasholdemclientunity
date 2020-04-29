using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using System;

//Now tests encryption to server

public class EncryptTest : MonoBehaviour
{
    public EncryptAndDecryptAES encryptor;
    //string returnedmessage;
    // Start is called before the first frame update
    void Start()
    {
        string encryptedmessage = encryptor.Encrypt("Hello");
        
        Debug.Log(encryptedmessage);
        

        HTTPRequest request = new HTTPRequest(new Uri("http://34.74.31.140/nodejsApp/encryptionTest"), HTTPMethods.Post, OnRequestFinished);
        request.AddField("encryptedString", encryptedmessage);
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
