using BestHTTP;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbySceneManager : MonoBehaviour
{
    public ConnectionPoller connectionPoller;
    public GameObject onlinePlayersContent;
    public GameObject lobbyUserPrefab;
    public GameObject gameRoomContent;
    public GameObject lobbyQueuePrefab;
    public GameObject gameRoomPrefab;
    public UserInLobby uil;
    float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        connectionPoller = GameObject.Find("ConnectionPoller").GetComponent<ConnectionPoller>();
        PopulateOnlinePlayers();
        PopulateQueuesAndGameRooms();

    }

    // Update is called once per frame
    void Update()
    {
        
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 2f)
        {
            PopulateOnlinePlayers();
            PopulateQueuesAndGameRooms();
            elapsedTime = 0f;
        }
    }

    public void GotoLobbyQueue()
    {
       // var uil = Instantiate(useri)
       // DontDestroyOnLoad(gameObject);
        connectionPoller.gameStateManager.TransitionSceneState("LobbyQueue", connectionPoller.email, connectionPoller.password);
      

    }


    void PopulateOnlinePlayers()
    {
        HTTPRequest request = new HTTPRequest(new Uri("http://34.74.31.140/nodejsApp/populateonlinelist"), HTTPMethods.Post, OnPopulateOnlinePlayers);
        //request.AddField("email", email);

       // request.AddField("password", password);


        request.Send();
    }

    void PopulateQueuesAndGameRooms()
    {
        HTTPRequest request = new HTTPRequest(new Uri("http://34.74.31.140/nodejsApp/populatequeueandgameroomlist"), HTTPMethods.Post, OnPopulateQueuesAndGameRooms);
       // request.AddField("email", email);

        //request.AddField("password", password);


        request.Send();
    }

    void OnPopulateOnlinePlayers(HTTPRequest request, HTTPResponse response)
    {
        Debug.Log("Request Finished! Text received: " + response.DataAsText);
        //string responseData = response.DataAsText.Substring(1, response.DataAsText.Length - 2);
        //Debug.Log(responseData);
        //CurrentGameScene currentGameScene = CurrentGameScene.CreateFromJSON(responseData);
        //gameStateManager.chips = int.Parse(currentGameScene.chips);
        //gameStateManager.gameScene = currentGameScene.gameScene;
        for (int i = 0; i < onlinePlayersContent.transform.childCount; i++)
        {
            Destroy(onlinePlayersContent.transform.GetChild(i).gameObject);
        }

        
        LobbyPlayerRoot lobbyPlayers = LobbyPlayerRoot.CreateFromJSON("{\"players\":"  + response.DataAsText + "}");

        foreach(LobbyPlayer l in lobbyPlayers.players)
        {
            var prefab = Instantiate(lobbyUserPrefab, onlinePlayersContent.transform, false);
             uil = prefab.GetComponent<UserInLobby>();
            uil.email = l.email;
            uil.firstname = l.firstname;
            uil.lastname = l.lastname;
            prefab.GetComponent<Text>().text = "" + l.firstname + " " + l.lastname;
            
        }
    }
    void OnPopulateQueuesAndGameRooms(HTTPRequest request, HTTPResponse response)
    {
        Debug.Log("Request Finished! Text received: " + response.DataAsText);
        if(response.DataAsText.Contains("response: \"none\""))
        {
            return;

        }

        //for (int i = 0; i < gameRoomContent.transform.childCount; i++)
        //{
        //    Destroy(gameRoomContent.transform.GetChild(i).gameObject);
        //}


        //***

        RoomsAndQueuesRoot roomsAndQueues = RoomsAndQueuesRoot.CreateFromJSON("{\"root\":[" + response.DataAsText + "]}");
        if (roomsAndQueues != null)
        {
            if (roomsAndQueues.root[0].queues != null)
            {
                int currentLobbyQueue = 0;
                foreach (LobbyQueue q in roomsAndQueues.root[0].queues)
                {
                    int countLobbyQueues = 0;
                    for (int lq = 0; lq < gameRoomContent.transform.childCount; lq++)
                    {
                        if (gameRoomContent.transform.GetChild(lq).GetComponent<LobbyQueueListItem>() != null)
                        {
                            countLobbyQueues++;
                        }
                    }
                    //foreach (LobbyQueueListItem lq in gameRoomContent.)
                    if (countLobbyQueues < roomsAndQueues.root[0].queues.Count)
                    {
                        //if (int.Parse(q.roomId) > roomsAndQueues.root.queues.Length)
                        if (currentLobbyQueue >= countLobbyQueues)
                        {
                            var prefab = Instantiate(lobbyQueuePrefab, gameRoomContent.transform, false);
                            var listItem = prefab.GetComponent<LobbyQueueListItem>();
                            listItem.roomId = q.roomId;
                            listItem.players.AddRange(q.players);
                            StringBuilder textListing = new StringBuilder("");
                            if (q.players != null)
                            {
                                foreach (LobbyQueuePlayers p in q.players)
                                {
                                    textListing.Append(p.firstname + " ");
                                }
                                prefab.transform.GetChild(1).GetComponent<Text>().text = textListing.ToString();
                            }
                        }
                        //else {
                        //    var prefab = Instantiate(lobbyQueuePrefab, gameRoomContent.transform, false);
                        //    var listItem = prefab.GetComponent<LobbyQueueListItem>();
                        //    listItem.roomId = q.roomId;
                        //    listItem.players.AddRange(q.players);
                        //    StringBuilder textListing = new StringBuilder("");
                        //    if (q.players != null)
                        //    {
                        //        foreach (LobbyQueuePlayers p in q.players)
                        //        {
                        //            textListing.Append(p.firstname + " ");
                        //        }
                        //        prefab.transform.GetChild(1).GetComponent<Text>().text = textListing.ToString();
                        //    }
                        //}
                    }
                    //if (int.Parse(q.roomId) > roomsAndQueues.root.queues.Length + roomsAndQueues.root.rooms.Length)
                    //{
                    //    var prefab = Instantiate(lobbyQueuePrefab, gameRoomContent.transform, false);
                    //    var listItem = prefab.GetComponent<LobbyQueueListItem>();
                    //    listItem.roomId = q.roomId;
                    //    listItem.players.AddRange(q.players);
                    //    StringBuilder textListing = new StringBuilder("");
                    //    if (q.players != null)
                    //    {
                    //        foreach (LobbyQueuePlayers p in q.players)
                    //        {
                    //            textListing.Append(p.firstname + " ");
                    //        }
                    //        prefab.transform.GetChild(0).GetComponent<Text>().text = textListing.ToString();
                    //    }
                    //}
                    currentLobbyQueue ++;
                }
            }

            if (roomsAndQueues.root[0].rooms != null && roomsAndQueues.root[0].queues != null)
            {
                foreach (LobbyQueue q in roomsAndQueues.root[0].queues) {
                    foreach (GameRoom g in roomsAndQueues.root[0].rooms)
                    {
                        if (g.roomId == q.roomId)
                        {
                            for (int i = 0; i < gameRoomContent.transform.childCount; i++)
                            {
                                if(gameRoomContent.transform.GetChild(i).GetComponent<LobbyQueueListItem>() != null)
                                {
                                    if (gameRoomContent.transform.GetChild(i).GetComponent<LobbyQueueListItem>().roomId == q.roomId) {
                                        Destroy(gameRoomContent.transform.GetChild(i));
                                        var prefab = Instantiate(gameRoomPrefab, gameRoomContent.transform, false);
                                        var listItem = prefab.GetComponent<GameRoomListItem>();
                                        listItem.roomId = g.roomId;
                                        listItem.players.AddRange(g.players);
                                        StringBuilder textListing = new StringBuilder("");
                                        if (g.players != null)
                                        {
                                            foreach (GameRoomPlayers p in g.players)
                                            {
                                                textListing.Append(p.firstname + " ");
                                            }
                                            prefab.transform.GetChild(1).GetComponent<Text>().text = textListing.ToString();
                                        }
                                    }
                                }
                                //if (gameRoomContent.transform.GetChild(i).GetComponent<GameRoomListItem>() != null)
                                //{

                                //}
                            }
                          
                        }
                    }
                }
            }


            for(int i = 0; i < gameRoomContent.transform.childCount; i++)
            {
                if (gameRoomContent.transform.GetChild(i).GetComponent<LobbyQueueListItem>() != null)
                {
                    foreach (LobbyQueue q in roomsAndQueues.root[0].queues) {
                        if (q.roomId == gameRoomContent.transform.GetChild(i).GetComponent<LobbyQueueListItem>().roomId)
                        {
                            gameRoomContent.transform.GetChild(i).GetComponent<LobbyQueueListItem>().players.Clear();
                            gameRoomContent.transform.GetChild(i).GetComponent<LobbyQueueListItem>().players.AddRange(q.players);

                        }
                    }
                }
                if (gameRoomContent.transform.GetChild(i).GetComponent<GameRoomListItem>() != null)
                {
                    foreach (GameRoom g in roomsAndQueues.root[0].rooms)
                    {
                        if (g.roomId == gameRoomContent.transform.GetChild(i).GetComponent<GameRoomListItem>().roomId)
                        {
                            gameRoomContent.transform.GetChild(i).GetComponent<GameRoomListItem>().players.Clear();
                            gameRoomContent.transform.GetChild(i).GetComponent<GameRoomListItem>().players.AddRange(g.players);

                        }
                    }
                }
            }
            //{

            //if (roomsAndQueues.root.rooms != null)
            //{
            //    foreach (GameRoom g in roomsAndQueues.root.rooms)
            //    {
            //        if (int.Parse(g.roomId) > roomsAndQueues.root.rooms.Length)
            //        {
            //            var prefab = Instantiate(gameRoomPrefab, gameRoomContent.transform, false);
            //            var listItem = prefab.GetComponent<GameRoomListItem>();
            //            listItem.roomId = g.roomId;
            //            listItem.players.AddRange(g.players);
            //            StringBuilder textListing = new StringBuilder("");
            //            if (g.players != null)
            //            {
            //                foreach (GameRoomPlayers p in g.players)
            //                {
            //                    textListing.Append(p.firstname + " ");
            //                }
            //                prefab.transform.GetChild(0).GetComponent<Text>().text = textListing.ToString();
            //            }
            //        }
            //    }
            //}
        }

        ////Debug.Log("Request Finished! Text received: " + response.DataAsText);
        //string responseData = response.DataAsText.Substring(1, response.DataAsText.Length - 2);
        //Debug.Log(responseData);
        //CurrentGameScene currentGameScene = CurrentGameScene.CreateFromJSON(responseData);
        //gameStateManager.chips = int.Parse(currentGameScene.chips);
        //gameStateManager.gameScene = currentGameScene.gameScene;
    }


    //[System.Serializable]
    //public class RoomsAndQueuesRoot {

    //}
    [System.Serializable]
    public class RoomsAndQueuesRoot
    {
        public IList<RoomsAndQueues> root;
        public static RoomsAndQueuesRoot CreateFromJSON(string jsonString)
        {
            return JsonConvert.DeserializeObject<RoomsAndQueuesRoot>(jsonString);
        }

    }
    [System.Serializable]
    public class RoomsAndQueues {
        public IList<LobbyQueue> queues;
        public IList<GameRoom> rooms;
        //public static RoomsAndQueues CreateFromJSON(string jsonString)
        //{
        //    return JsonUtility.FromJson<RoomsAndQueues>(jsonString);
        //}

    }

    //[System.Serializable]
    //public class LobbyQueueRoot {
    //    public LobbyQueue[] lobbyQueues;

    //    //("{\"players\":"  + response.DataAsText + "}")
    //    public static LobbyQueueRoot CreateFromJSON(string jsonString)
    //    {
    //        return JsonUtility.FromJson<LobbyQueueRoot>(jsonString);
    //    }

    //}

    [System.Serializable]
    public class LobbyQueue {
        public string roomId;
        public IList<LobbyQueuePlayers> players;
        public string queueState;
    }

    [System.Serializable]
    public class LobbyQueuePlayers
    {
        public string email;
        public string firstname;
        //public string roomId;

    }

    //[System.Serializable]
    //public class GameRoomRoot {
    //    public GameRoom[] gameRooms;

    //}

    [System.Serializable]
    public class GameRoom {
        public string roomId;
        public IList<GameRoomPlayers>players;
    }

    [System.Serializable]
    public class GameRoomPlayers
    {
        public string email;
        public string firstname;
    }

    [System.Serializable]
    public class LobbyPlayer
    {
        public string firstname;
        public string lastname;
        public string email;
        //public float health;

     
    }

    [System.Serializable]
    public class LobbyPlayerRoot
    {
        public IList<LobbyPlayer> players;
        //public float health;

        public static LobbyPlayerRoot CreateFromJSON(string jsonString)
        {
            return JsonConvert.DeserializeObject<LobbyPlayerRoot>(jsonString);
        }
    }

}
