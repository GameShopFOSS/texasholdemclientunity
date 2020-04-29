using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using System;
using UnityEngine.SceneManagement;

public class ConnectionPoller : MonoBehaviour
{
    public string email;
    public string password;
    float elapsedTime = 0;
    public GameStateManager gameStateManager;
    // Start is called before the first frame update
    void Start()
    {
        gameStateManager = new GameStateManager();
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 3f)
        {
            PollServer();
            elapsedTime = 0f;
        }
    }

    public void PollServer()
    {
        HTTPRequest request = new HTTPRequest(new Uri("http://34.74.31.140/nodejsApp/connectionpoll"), HTTPMethods.Post, OnRequestFinished);
        request.AddField("email", email);
        
        request.AddField("password", password);
    

        request.Send();
    }

    void OnRequestFinished(HTTPRequest request, HTTPResponse response)
    {
        //Debug.Log("Request Finished! Text received: " + response.DataAsText);
        string responseData = response.DataAsText.Substring(1, response.DataAsText.Length - 2);
        Debug.Log(responseData);
        CurrentGameScene currentGameScene = CurrentGameScene.CreateFromJSON(responseData);
        gameStateManager.chips = int.Parse(currentGameScene.chips);
        gameStateManager.gameScene = currentGameScene.gameScene;
    }

    [System.Serializable]
    public class CurrentGameScene {
        public string chips;
        public string gameScene;
        //public float health;

        public static CurrentGameScene CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<CurrentGameScene>(jsonString);
        }
    }
    [System.Serializable]
    public class GameStateManager {

        public int chips;
        public string gameScene;

       

        public GameStateManager() {

        }

        public void TransitionSceneState(string destinationState, string email, string password) {
            HTTPRequest request = new HTTPRequest(new Uri("http://34.74.31.140/nodejsApp/transitionscene"), HTTPMethods.Post, OnRequestFinished);
            request.AddField("email", email);

            request.AddField("password", password);
            request.AddField("destination", destinationState);

            request.Send();
        }

        void OnRequestFinished(HTTPRequest request, HTTPResponse response)
        {
            Debug.Log("Request Finished! Text received: " + response.DataAsText);

            if (gameScene == "MainMenu")
            {
                SceneManager.LoadScene("MainMenuScene");
            }

            else if (gameScene == "Lobby")
            {
                SceneManager.LoadScene("LobbyScene");
            }

            else if (gameScene == "GameRoom")
            {

                SceneManager.LoadScene("GameRoomScene");
            }
            else if (gameScene == "LobbyQueue")
            {
                SceneManager.LoadScene("LobbyQueueScene");
            }
            //string responseData = response.DataAsText.Substring(1, response.DataAsText.Length - 2);
            //Debug.Log(responseData);
            //CurrentGameScene currentGameScene = CurrentGameScene.CreateFromJSON(responseData);
            //gameStateManager.chips = int.Parse(currentGameScene.chips);
            //gameStateManager.gameScene = currentGameScene.gameScene;
        }

    }


}
