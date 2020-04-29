using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public Image firstCard;
    public Image secondCard;
    public Image flop;
    public Image turn;
    public Image river;
    public Image handOne;
    public Image handTwo;
    // Start is called before the first frame update
    void Start()
    {
        firstCard.gameObject.SetActive(false);
        secondCard.gameObject.SetActive(false);
        flop.gameObject.SetActive(false);
        turn.gameObject.SetActive(false);
        river.gameObject.SetActive(false);
        handOne.gameObject.SetActive(false);
        handTwo.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //stuff
    public void UpdateCards() {

    }
}
