using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyQueueListItem : MonoBehaviour
{

    public string roomId;
    public List<LobbySceneManager.LobbyQueuePlayers> players;
    // Start is called before the first frame update
    void Start()
    {
        players = new List<LobbySceneManager.LobbyQueuePlayers>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public class LobbyQueuePlayers
    //{
    //    public string email;
    //    public string firstname;
    //    //public string roomId;

    //}
}
