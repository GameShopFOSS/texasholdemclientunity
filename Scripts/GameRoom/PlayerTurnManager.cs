using BestHTTP;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnManager : MonoBehaviour
{
    //public bool hasPlayed = false;
    public bool isTurn = false;
    public bool checkAvailable = false;
    public long chipsBet;
    public long currentBet;
    public long totalChips;
    //public string action;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoFold() {
        if (isTurn)
        {
            HTTPRequest request = new HTTPRequest(new Uri("http://34.74.31.140/nodejsApp/submitplayeraction"), HTTPMethods.Post, OnSubmitPlayerAction);
            request.AddField("action", "fold");
            request.AddField("roomId", GameObject.Find("CurrentRoomId").GetComponent<CurrentRoomId>().currentRoomId.ToString());
            request.AddField("email", GameObject.Find("ConnectionPoller").GetComponent<ConnectionPoller>().email);
            request.AddField("password", GameObject.Find("ConnectionPoller").GetComponent<ConnectionPoller>().password);
            request.AddField("amount", "0");
           

            request.Send();
        }
    }
    public void DoCheck()
    {
        if (isTurn)
        {
            if (CanCheck())
            {


                HTTPRequest request = new HTTPRequest(new Uri("http://34.74.31.140/nodejsApp/submitplayeraction"), HTTPMethods.Post, OnSubmitPlayerAction);
                request.AddField("action", "check");
                request.AddField("roomId", GameObject.Find("CurrentRoomId").GetComponent<CurrentRoomId>().currentRoomId.ToString());
                request.AddField("email", GameObject.Find("ConnectionPoller").GetComponent<ConnectionPoller>().email);
                request.AddField("password", GameObject.Find("ConnectionPoller").GetComponent<ConnectionPoller>().password);
                request.AddField("amount", "");


                request.Send();
            }
        }
    }

    public bool CanCheck()
    {
        if (chipsBet >= currentBet)
        {
            return true;
        }
        return false;
    }
    public void DoCall()
    {
        if (isTurn)
        {
            if (CanCall())
            {
                HTTPRequest request = new HTTPRequest(new Uri("http://34.74.31.140/nodejsApp/submitplayeraction"), HTTPMethods.Post, OnSubmitPlayerAction);
                request.AddField("action", "call" );
                request.AddField("roomId", GameObject.Find("CurrentRoomId").GetComponent<CurrentRoomId>().currentRoomId.ToString());
                request.AddField("email", GameObject.Find("ConnectionPoller").GetComponent<ConnectionPoller>().email);
                request.AddField("password", GameObject.Find("ConnectionPoller").GetComponent<ConnectionPoller>().password);
                request.AddField("amount", CallMatch());


                request.Send();
            }
           
        }
    }

    public bool CanCall() {
        if (totalChips >= currentBet - chipsBet)
        {
            return true;
        }

        return false;
    }

    public string CallMatch()
    {
        long total = currentBet - chipsBet;
        return total.ToString();
    }
    public void DoRaise()
    {
        if (isTurn)
        {
            if (CanRaise())
            {
                HTTPRequest request = new HTTPRequest(new Uri("http://34.74.31.140/nodejsApp/submitplayeraction"), HTTPMethods.Post, OnSubmitPlayerAction);
                request.AddField("action", "raise");
                request.AddField("roomId", GameObject.Find("CurrentRoomId").GetComponent<CurrentRoomId>().currentRoomId.ToString());
                request.AddField("email", GameObject.Find("ConnectionPoller").GetComponent<ConnectionPoller>().email);
                request.AddField("password", GameObject.Find("ConnectionPoller").GetComponent<ConnectionPoller>().password);
                request.AddField("amount", RaiseMatch());


                request.Send();
            }
         
        }
    }

    public bool CanRaise()
    {
        if (totalChips >= (currentBet * 2) - chipsBet) {
            return true;
        }
        return false;
    }

    public string RaiseMatch()
    {
        long total = (currentBet * 2) - chipsBet;
        return total.ToString();
    }

    void OnSubmitPlayerAction(HTTPRequest request, HTTPResponse response)
    {
        Debug.Log("Request Finished! Text received: " + response.DataAsText);
        //string responseData = response.DataAsText.Substring(1, response.DataAsText.Length - 2);
        //Debug.Log(responseData);
        //CurrentGameScene currentGameScene = CurrentGameScene.CreateFromJSON(responseData);
        //gameStateManager.chips = int.Parse(currentGameScene.chips);
        //gameStateManager.gameScene = currentGameScene.gameScene;
    }
}
