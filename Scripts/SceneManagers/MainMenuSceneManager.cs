using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSceneManager : MonoBehaviour
{
    public ConnectionPoller connectionPoller;
    public Text chipAmount;
    public Text moneyAmount;
    // Start is called before the first frame update
    void Start()
    {
        connectionPoller = GameObject.Find("ConnectionPoller").GetComponent<ConnectionPoller>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateValues();
    }

    public void UpdateValues()
    {
        chipAmount.text = connectionPoller.gameStateManager.chips.ToString();
        long multiplied = connectionPoller.gameStateManager.chips * 1000;
        moneyAmount.text = multiplied.ToString();
    }
    public void GotoLobby()
    {
        connectionPoller.gameStateManager.TransitionSceneState("Lobby", connectionPoller.email, connectionPoller.password);
        
        
    }

    public void GotoJustPlay()
    {

    }

    public void GotoShop()
    {
        //SceneManager.LoadScene("ShopScene");
    }

    public void GotoChat()
    {

    }

    public void GotoFriends() {
       // SceneManager.LoadScene("FriendsScene");
    }

    public void GotoSettings()
    {
       // SceneManager.LoadScene("SettingsScene");
    }
}
