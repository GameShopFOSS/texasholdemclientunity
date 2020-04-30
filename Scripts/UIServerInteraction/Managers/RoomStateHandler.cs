using BestHTTP;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomStateHandler : MonoBehaviour
{
    public ConnectionPoller connectionPoller;
    public CardManager cardManager;
    public CardResolver cardResolver;
    public PlayerUIManager playerUIManager;
    public PlayerTurnManager playerTurnManager;
    float elapsedTime = 0;
    bool gameStarted = false;
    float gameStartedElapsedTime = 0;
    bool lockGameStarted = false;
    //LobbyQueueListItem
    // Start is called before the first frame update
    void Start()
    {
        connectionPoller = GameObject.Find("ConnectionPoller").GetComponent<ConnectionPoller>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 0.5f)
        {
            if (connectionPoller.gameStateManager.gameScene == "LobbyQueue")
            {
                UpdateRoom();
            }
            else if (connectionPoller.gameStateManager.gameScene == "GameRoom")
            {
                cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
                cardResolver = GameObject.Find("CardResolver").GetComponent<CardResolver>();
                playerUIManager = GameObject.Find("PlayerUIManager").GetComponent<PlayerUIManager>();
                playerTurnManager = GameObject.Find("PlayerTurnManager").GetComponent<PlayerTurnManager>();
                //SceneManager.LoadScene("GameRoomScene");
                UpdateGameRoom();
            }
            
            
            elapsedTime = 0f;
        }
        if (gameStarted)
        {
            gameStartedElapsedTime += Time.deltaTime;
            if (gameStartedElapsedTime > 5f && !lockGameStarted)
            {
                gameStarted = false;
                lockGameStarted = true;
                MultiplayerGameStart();
                
            }
            
        }
        
    }

    void UpdateGameRoom()
    {
        HTTPRequest request = new HTTPRequest(new Uri("http://34.74.31.140/nodejsApp/getgameroomstate"), HTTPMethods.Post, OnUpdateGameRoom);

        request.AddField("roomId", GetComponent<LobbyQueueListItem>().roomId);

        request.AddField("email", connectionPoller.email);

        request.Send();
    }

    void OnUpdateGameRoom(HTTPRequest request, HTTPResponse response)
    {
        Debug.Log("Request Finished! Text received: " + response.DataAsText);
        //if (response.DataAsText.Contains("success: \"OK\""))
        //{
        //LobbyQueueRoomManager
        //roomManager = GameObject.Find("LobbyQueueRoomManager").GetComponent<LobbyQueueListItem>();
        //roomManager.gameObject.SetActive(true);
        //roomManager.roomId = GetComponent<LobbyQueueListItem>().roomId;
        //roomManager.players.AddRange(GetComponent<LobbyQueueListItem>().players);
        //DontDestroyOnLoad(roomManager);
        // lobbySceneManager.GotoLobbyQueue();
        // }
        //for (int i = 0; i < GameObject.Find("").transform.childCount; i++)
        //{
        //    Destroy(onlinePlayersContent.transform.GetChild(i).gameObject);
        //}
        
        DecodeGameState.GameStateRoot gameState = DecodeGameState.GameStateRoot.CreateFromJSON(response.DataAsText);


        playerUIManager.player1.transform.Find("Text").gameObject.GetComponent<Text>().text = gameState.gameData[0].players[0].firstname;
        playerUIManager.player2.transform.Find("Text").gameObject.GetComponent<Text>().text = gameState.gameData[0].players[1].firstname;
        playerUIManager.player3.transform.Find("Text").gameObject.GetComponent<Text>().text = gameState.gameData[0].players[2].firstname;
        playerUIManager.player4.transform.Find("Text").gameObject.GetComponent<Text>().text = gameState.gameData[0].players[3].firstname;
        playerUIManager.player5.transform.Find("Text").gameObject.GetComponent<Text>().text = gameState.gameData[0].players[4].firstname;
        playerUIManager.player6.transform.Find("Text").gameObject.GetComponent<Text>().text = gameState.gameData[0].players[5].firstname;
        playerUIManager.player7.transform.Find("Text").gameObject.GetComponent<Text>().text = gameState.gameData[0].players[6].firstname;
        playerUIManager.player8.transform.Find("Text").gameObject.GetComponent<Text>().text = gameState.gameData[0].players[7].firstname;

        playerUIManager.pot.text = gameState.gameData[0].dealer.currentPot;

        if (gameState.gameData[0].cards.Count > 0)
        {
            cardManager.firstCard.sprite = cardResolver.resolveWhichCard(gameState.gameData[0].cards[0].suit, gameState.gameData[0].cards[0].rank);
            cardManager.firstCard.gameObject.SetActive(true);
        }
        if (gameState.gameData[0].cards.Count > 1)
        {
            cardManager.secondCard.sprite = cardResolver.resolveWhichCard(gameState.gameData[0].cards[1].suit, gameState.gameData[0].cards[1].rank);
            cardManager.secondCard.gameObject.SetActive(true);
        }
        if (gameState.gameData[0].cards.Count > 2)
        {
            cardManager.flop.sprite = cardResolver.resolveWhichCard(gameState.gameData[0].cards[2].suit, gameState.gameData[0].cards[2].rank);
            cardManager.flop.gameObject.SetActive(true);
        }
        if (gameState.gameData[0].cards.Count > 3)
        {
            cardManager.turn.sprite = cardResolver.resolveWhichCard(gameState.gameData[0].cards[3].suit, gameState.gameData[0].cards[3].rank);
            cardManager.turn.gameObject.SetActive(true);
        }
        if (gameState.gameData[0].cards.Count > 4)
        {
            cardManager.river.sprite = cardResolver.resolveWhichCard(gameState.gameData[0].cards[4].suit, gameState.gameData[0].cards[4].rank);
            cardManager.river.gameObject.SetActive(true);
        }

        playerUIManager.chipAmount.text = connectionPoller.gameStateManager.chips.ToString();
        playerUIManager.dollarAmount.text = (connectionPoller.gameStateManager.chips * 1000).ToString();
        playerUIManager.whosTurn.text = "Player "+ gameState.gameData[0].dealer.playerTurn + "'s Turn";
        playerUIManager.timeLeft.text = (30 - int.Parse(gameState.gameData[0].dealer.turnElapsedTime)).ToString();
        foreach (DecodeGameState.GameStatePlayer p in gameState.gameData[0].players)
        {
            if (p.email == connectionPoller.email)
            {
                if (gameState.gameData[0].dealer.playerTurn == p.clockWisePositionFromButton)
                {
                    playerTurnManager.isTurn = true;
                    playerTurnManager.chipsBet = long.Parse(p.chipsBlind);
                    playerTurnManager.currentBet = long.Parse(gameState.gameData[0].dealer.currentBlind);
                    //if (p.chipsBlind >= currentBet) {
                    playerTurnManager.totalChips = connectionPoller.gameStateManager.chips;
                    playerUIManager.whosTurn.text = "Your Turn";
                    break;
                    //}
                }
                else {
                    playerTurnManager.isTurn = false;
                    //playerUIManager.whosTurn.text =
                }
            }
            
        }

      //  playerUIManager.pot.text = gameState.gameData.dealer.currentBlind;
        
        //RoomGameDataRoot roomData = RoomGameDataRoot.CreateFromJSON(response.DataAsText);
        //// var images = GameObject.Find("Canvas").GetComponentsInChildren<Image>();
        //int counting = -1;
        //foreach (Image i in GameObject.Find("Canvas").GetComponentsInChildren<Image>())
        //{
        //    if (!i.name.Contains("PlayerAvatar"))
        //    {
        //        continue;
        //    }
        //    counting++;
        //    i.GetComponentInChildren<Text>().text = roomData.gameData.players[counting].firstname;
        //}
        ////LobbyPlayerRoot lobbyPlayers = LobbyPlayerRoot.CreateFromJSON("{\"players\":" + response.DataAsText + "}");

        //if (roomData.gameData.players.Length == 8)
        //{
        //    MultiplayerGameStart();
        //}

        //string responseData = response.DataAsText.Substring(1, response.DataAsText.Length - 2);
        //Debug.Log(responseData);
        //CurrentGameScene currentGameScene = CurrentGameScene.CreateFromJSON(responseData);
        //gameStateManager.chips = int.Parse(currentGameScene.chips);
        //gameStateManager.gameScene = currentGameScene.gameScene;
    }



    void UpdateRoom() {
        HTTPRequest request = new HTTPRequest(new Uri("http://34.74.31.140/nodejsApp/getlobbyqueuestate"), HTTPMethods.Post, OnRequestFinished);

        request.AddField("roomId", gameObject.GetComponent<LobbyQueueListItem>().roomId);
        
        request.Send();
    }

    void OnRequestFinished(HTTPRequest request, HTTPResponse response)
    {
        Debug.Log("Request Finished! Text received: " + response.DataAsText);
        //if (response.DataAsText.Contains("success: \"OK\""))
        //{
        //LobbyQueueRoomManager
        //roomManager = GameObject.Find("LobbyQueueRoomManager").GetComponent<LobbyQueueListItem>();
        //roomManager.gameObject.SetActive(true);
        //roomManager.roomId = GetComponent<LobbyQueueListItem>().roomId;
        //roomManager.players.AddRange(GetComponent<LobbyQueueListItem>().players);
        //DontDestroyOnLoad(roomManager);
        // lobbySceneManager.GotoLobbyQueue();
        // }
        //for (int i = 0; i < GameObject.Find("").transform.childCount; i++)
        //{
        //    Destroy(onlinePlayersContent.transform.GetChild(i).gameObject);
        //}
        //RoomGameDataRoot roomData = new RoomGameDataRoot();
        RoomGameDataRoot roomData = RoomGameDataRoot.CreateFromJSON(response.DataAsText);
        // var images = GameObject.Find("Canvas").GetComponentsInChildren<Image>();
        //int counting = -1;
        for (int i = 3; i < 11; i++)
        {
            GameObject avatar = GameObject.Find("Canvas").transform.GetChild(i).gameObject;
            Debug.Log(avatar.name);
            Text t = avatar.transform.GetChild(0).gameObject.GetComponent<Text>();
            Debug.Log(t.name);
            Debug.Log(roomData);
            if (roomData.gameData[0].players != null)
            {
                if (roomData.gameData[0].players.Count > i - 3)
               // if(roomData.gameData.players[i - 3].firstname != "")
                {

                    t.text = roomData.gameData[0].players[i - 3].firstname;
                    Debug.Log(t.text);
                }
            }
        }
        //foreach (Image i in GameObject.Find("Canvas").GetComponentsInChildren<Image>()) {
        //    if (!i.name.Contains("PlayerAvatar"))
        //    {
        //        continue;
        //    }
        //    counting++;
        //    if (roomData.gameData.players != null)
        //    {
        //        if (roomData.gameData.players.Length < counting)
        //        {
               
        //        i.GetComponentInChildren<Text>().text = roomData.gameData.players[counting].firstname;
        //        }
        //    }
           
        //}
        //LobbyPlayerRoot lobbyPlayers = LobbyPlayerRoot.CreateFromJSON("{\"players\":" + response.DataAsText + "}");
        //  
        if (roomData.gameData[0].players != null)
        {
            if (roomData.gameData[0].players.Count == 8 && !gameStarted)
            {
                //yield return new WaitForSeconds(5);
                gameStarted = true;
               
            }
        }
        //string responseData = response.DataAsText.Substring(1, response.DataAsText.Length - 2);
        //Debug.Log(responseData);
        //CurrentGameScene currentGameScene = CurrentGameScene.CreateFromJSON(responseData);
        //gameStateManager.chips = int.Parse(currentGameScene.chips);
        //gameStateManager.gameScene = currentGameScene.gameScene;
    }

    public void MultiplayerGameStart()
    {
        connectionPoller.gameStateManager.TransitionSceneState("GameRoom", connectionPoller.email, connectionPoller.password);
        
        DontDestroyOnLoad(gameObject);
    }

    public class DecodeGameState
    {

        [System.Serializable]
        public class GameStateRoot
        {
            public GameStateData[] gameData;
            //  public string roomId;
            //public float health;

            public static GameStateRoot CreateFromJSON(string jsonString)
            {
                return JsonConvert.DeserializeObject<GameStateRoot>(jsonString);
            }
        }

        [System.Serializable]
        public class GameStateData
        {
            public IList<GameStatePlayer> players;// = new List<GameStatePlayer>();
            public IList<GameStateCard> cards;// = new List<GameStateCard>();
            public GameStateDealer dealer;
            //public string roomId;
            //public float health;
            //public GameStateData(List<GameStatePlayer> p, List<GameStateCard> c)
            //{
            //    players.AddRange(p);
            //    cards.AddRange(c);
            //}

        }

        [System.Serializable]
        public class GameStatePlayer
        {
            //{email: data.email, firstname: data.firstname, chipsStocked: "0", chipsBlind: "0", cardsInHand : [], clockWisePositionFromButton: '' + i});
            public string email;
            public string firstname;
            public string chipsStocked;
            public string chipsBlind;
            public IList<GameStateCard> cardsInHand;// = new List<GameStateCard>();
            public string clockWisePositionFromButton;
            //public RoomGamePlayer[] players;
            //public string roomId;
            //public float health;
            //public GameStatePlayer(List<GameStateCard> l)
            //{
            //    cardsInHand.AddRange(l);
            //}

        }

        [System.Serializable]
        public class GameStateCard
        {
            public string suit;
            public string rank;
            //public RoomGamePlayer[] players;
            //public string roomId;
            //public float health;


        }
        [System.Serializable]
        public class GameStateDealer
        {
            //public RoomGamePlayer[] players;
            //public string roomId;
            //public float health;
            //{playerTurn: playerTurn, playState: playState, turnElapsedTime: "0", currentBlind: "0"};
            public string playerTurn;
            public string playState;
            public string turnElapsedTime;
            public string currentBlind;
            public string currentPot;
        }

    }





    [System.Serializable]
    public class RoomGamePlayer
    {
        public string email;
        public string firstname;
        //public float health;

        public override string ToString()
        {
            return email + firstname;
        }
    }
    [System.Serializable]
    public class RoomGameData
    {
        public IList<RoomGamePlayer> players;// = new List<RoomGamePlayer>();
        //public string roomId;
        //public float health;
        //public RoomGameData(List<RoomGamePlayer> l)
        //{
        //    players.AddRange(l);
        //}

        public override string ToString()
        {
            if (players.Count > 0)
            {
                return players[0].ToString();
            }
            return "empty";
        }

    }
    [System.Serializable]
    public class RoomGameDataRoot
    {
        public RoomGameData[] gameData;
      //  public string roomId;
        //public float health;

        public static RoomGameDataRoot CreateFromJSON(string jsonString)
        {
            return JsonConvert.DeserializeObject<RoomGameDataRoot>(jsonString);
        }

        public override string ToString()
        {
            return gameData.ToString();
        }
    }
}
