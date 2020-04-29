using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomListItem : MonoBehaviour
{
    public string roomId;
    public List<LobbySceneManager.GameRoomPlayers> players;
    // Start is called before the first frame update
    void Start()
    {
        players = new List<LobbySceneManager.GameRoomPlayers>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    //public class GameRoomPlayers
    //{
    //    public string email;
    //    public string firstname;
    //}
}
