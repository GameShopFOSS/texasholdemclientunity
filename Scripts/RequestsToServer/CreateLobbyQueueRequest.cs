using BestHTTP;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLobbyQueueRequest : MonoBehaviour
{

    public LobbySceneManager lobbySceneManager;
    public GameObject roomManager;
    // Start is called before the first frame update
    void Start()
    {
        lobbySceneManager = GameObject.Find("SceneManager").GetComponent<LobbySceneManager>();
        roomManager = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRequest() {
        HTTPRequest request = new HTTPRequest(new Uri("http://34.74.31.140/nodejsApp/createnewlobbyqueue"), HTTPMethods.Post, OnRequestFinished);
        
        request.AddField("email", lobbySceneManager.connectionPoller.email);

        request.AddField("firstname", lobbySceneManager.uil.firstname);
      //  request.AddField("destination", "LobbyQueue");

        request.Send();
    }

    void OnRequestFinished(HTTPRequest request, HTTPResponse response)
    {
        Debug.Log("Request Finished! Text received: " + response.DataAsText);
        // if (response.DataAsText.Contains("success: \"OK\"")) {
        //LobbyQueueRoomManager
        //response roomId;
        //GameObject.Find("LobbyQueueRoomManager").SetActive(true);
        // GameObject.Find("LobbyQueueRoomManager");
        //roomManager = GameObject.Find("LobbyQueueRoomManager").GetComponent<LobbyQueueListItem>();
        //roomManager.gameObject.SetActive(true);
        roomManager = Instantiate(roomManager);
        roomManager.gameObject.SetActive(true);
        RoomResponse roomResponse = RoomResponse.CreateFromJSON(response.DataAsText);
            roomManager.GetComponent<LobbyQueueListItem>().roomId = roomResponse.roomId; //GetComponent<LobbyQueueListItem>().roomId;
            roomManager.GetComponent<LobbyQueueListItem>().players.AddRange(lobbySceneManager.lobbyQueuePrefab.GetComponent<LobbyQueueListItem>().players);
            DontDestroyOnLoad(roomManager);
        //DontDestroyOnLoad(gameObject);
            lobbySceneManager.GotoLobbyQueue();
       // }


        //string responseData = response.DataAsText.Substring(1, response.DataAsText.Length - 2);
        //Debug.Log(responseData);
        //CurrentGameScene currentGameScene = CurrentGameScene.CreateFromJSON(responseData);
        //gameStateManager.chips = int.Parse(currentGameScene.chips);
        //gameStateManager.gameScene = currentGameScene.gameScene;
    }

    [System.Serializable]
    public class RoomResponse
    {
        public string response;
        public string roomId;
        //public float health;

        public static RoomResponse CreateFromJSON(string jsonString)
        {
            return JsonConvert.DeserializeObject<RoomResponse>(jsonString);
        }
    }




}
