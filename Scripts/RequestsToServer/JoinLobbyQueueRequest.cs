using BestHTTP;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinLobbyQueueRequest : MonoBehaviour
{
    public LobbySceneManager lobbySceneManager;
    public GameObject roomManager;
    // Start is called before the first frame update
    void Start()
    {
        lobbySceneManager = GameObject.Find("SceneManager").GetComponent<LobbySceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRequest(UserInLobby sender)
    {
        HTTPRequest request = new HTTPRequest(new Uri("http://34.74.31.140/nodejsApp/enterspecificlobbyqueue"), HTTPMethods.Post, OnRequestFinished);

        request.AddField("email", lobbySceneManager.connectionPoller.email);
       // for (int i = 0; i < lobbySceneManager.onlinePlayersContent.transform.childCount; i++)
        request.AddField("firstname", lobbySceneManager.connectionPoller.firstname);
        request.AddField("roomId", "" + GameObject.Find("SelectLobbyQueue(Clone)").GetComponent<LobbyQueueListItem>().roomId);

        request.Send();
    }

    void OnRequestFinished(HTTPRequest request, HTTPResponse response)
    {
        Debug.Log("Request Finished! Text received: " + response.DataAsText);
        //  if (response.DataAsText.Contains("success: \"OK\""))
        // {
        //LobbyQueueRoomManager
        //roomManager = GameObject.Find("LobbyQueueRoomManager").GetComponent<LobbyQueueListItem>();
        roomManager = Instantiate(roomManager);
        roomManager.gameObject.SetActive(true);
        RoomResponse roomResponse = RoomResponse.CreateFromJSON(response.DataAsText);
        roomManager.GetComponent<LobbyQueueListItem>().roomId = roomResponse.roomId;
        //roomManager.players.AddRange(GetComponent<LobbyQueueListItem>().players);
        roomManager.GetComponent<LobbyQueueListItem>().players.AddRange(lobbySceneManager.lobbyQueuePrefab.GetComponent<LobbyQueueListItem>().players);
        DontDestroyOnLoad(roomManager);
        gameObject.transform.SetParent(null);

        DontDestroyOnLoad(gameObject);
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
            return JsonUtility.FromJson<RoomResponse>(jsonString);
        }
    }
}
