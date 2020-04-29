using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BestHTTP;
using System.Linq;
using System;

public class LoginAuth : MonoBehaviour
{
    public InputField emailText;
    public InputField passwordText;
    public EncryptAndDecryptAES encryptor;
    public ConnectionPoller connectionPoller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Submit()
    {
        SendToServer();
    }

    public void SendToServer() {
        HTTPRequest request = new HTTPRequest(new Uri("http://34.74.31.140/nodejsApp/connectionpoll"), HTTPMethods.Post, OnRequestFinished);
        request.AddField("email", emailText.text);
     
        request.AddField("password", encryptor.Encrypt(passwordText.text));
    

        request.Send();
    }

    void OnRequestFinished(HTTPRequest request, HTTPResponse response)
    {
        Debug.Log("Request Finished! Text received: " + response.DataAsText);

        connectionPoller.email = emailText.text;
        connectionPoller.password = encryptor.Encrypt(passwordText.text);
        connectionPoller.gameObject.SetActive(true);
        DontDestroyOnLoad(connectionPoller.gameObject);
        connectionPoller.gameStateManager.TransitionSceneState("MainMenu", connectionPoller.email, connectionPoller.password);
        
    }

}
