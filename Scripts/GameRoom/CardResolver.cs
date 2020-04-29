using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardResolver : MonoBehaviour
{
    public Sprite spadesAce;
    public Sprite spadesTwo;
    public Sprite spadesThree;
    public Sprite spadesFour;
    public Sprite spadesFive;
    public Sprite spadesSix;
    public Sprite spadesSeven;
    public Sprite spadesEight;
    public Sprite spadesNine;
    public Sprite spadesTen;
    public Sprite spadesJack;
    public Sprite spadesQueen;
    public Sprite spadesKing;

    public Sprite clubsAce;
    public Sprite clubsTwo;
    public Sprite clubsThree;
    public Sprite clubsFour;
    public Sprite clubsFive;
    public Sprite clubsSix;
    public Sprite clubsSeven;
    public Sprite clubsEight;
    public Sprite clubsNine;
    public Sprite clubsTen;
    public Sprite clubsJack;
    public Sprite clubsQueen;
    public Sprite clubsKing;

    public Sprite heartsAce;
    public Sprite heartsTwo;
    public Sprite heartsThree;
    public Sprite heartsFour;
    public Sprite heartsFive;
    public Sprite heartsSix;
    public Sprite heartsSeven;
    public Sprite heartsEight;
    public Sprite heartsNine;
    public Sprite heartsTen;
    public Sprite heartsJack;
    public Sprite heartsQueen;
    public Sprite heartsKing;

    public Sprite diamondsAce;
    public Sprite diamondsTwo;
    public Sprite diamondsThree;
    public Sprite diamondsFour;
    public Sprite diamondsFive;
    public Sprite diamondsSix;
    public Sprite diamondsSeven;
    public Sprite diamondsEight;
    public Sprite diamondsNine;
    public Sprite diamondsTen;
    public Sprite diamondsJack;
    public Sprite diamondsQueen;
    public Sprite diamondsKing;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite resolveWhichCard(string suit, string rank)
    {
        if (rank == "Ace") {
            if (suit == "Spades")
            {
                return spadesAce;
            }
            else if (suit == "Hearts")
            {
                return heartsAce;
            }
            else if (suit == "Clubs")
            {
                return clubsAce;
            }
            else if(suit == "Diamonds")
            {
                return diamondsAce;
            }
        }
        if (rank == "2")
        {
            if (suit == "Spades")
            {
                return spadesTwo;
            }
            else if (suit == "Hearts")
            {
                return heartsTwo;
            }
            else if (suit == "Clubs")
            {
                return clubsTwo;
            }
            else if (suit == "Diamonds")
            {
                return diamondsTwo;
            }
        }
        if (rank == "3")
        {
            if (suit == "Spades")
            {
                return spadesThree;
            }
            else if (suit == "Hearts")
            {
                return heartsThree;
            }
            else if (suit == "Clubs")
            {
                return clubsThree;
            }
            else if (suit == "Diamonds")
            {
                return diamondsThree;
            }
        }

        if (rank == "4")
        {
            if (suit == "Spades")
            {
                return spadesFour;
            }
            else if (suit == "Hearts")
            {
                return heartsFour;
            }
            else if (suit == "Clubs")
            {
                return clubsFour;
            }
            else if (suit == "Diamonds")
            {
                return diamondsFour;
            }
        }

        if (rank == "5")
        {
            if (suit == "Spades")
            {
                return spadesFive;
            }
            else if (suit == "Hearts")
            {
                return heartsFive;
            }
            else if (suit == "Clubs")
            {
                return clubsFive;
            }
            else if (suit == "Diamonds")
            {
                return diamondsFive;
            }
        }

        if (rank == "6")
        {
            if (suit == "Spades")
            {
                return spadesSix;
            }
            else if (suit == "Hearts")
            {
                return heartsSix;
            }
            else if (suit == "Clubs")
            {
                return clubsSix;
            }
            else if (suit == "Diamonds")
            {
                return diamondsSix;
            }
        }
        if (rank == "7")
        {
            if (suit == "Spades")
            {
                return spadesSeven;
            }
            else if (suit == "Hearts")
            {
                return heartsSeven;
            }
            else if (suit == "Clubs")
            {
                return clubsSeven;
            }
            else if (suit == "Diamonds")
            {
                return diamondsSeven;
            }
        }
        if (rank == "8")
        {
            if (suit == "Spades")
            {
                return spadesEight;
            }
            else if (suit == "Hearts")
            {
                return heartsEight;
            }
            else if (suit == "Clubs")
            {
                return clubsEight;
            }
            else if (suit == "Diamonds")
            {
                return diamondsEight;
            }
        }
        if (rank == "9")
        {
            if (suit == "Spades")
            {
                return spadesNine;
            }
            else if (suit == "Hearts")
            {
                return heartsNine;
            }
            else if (suit == "Clubs")
            {
                return clubsNine;
            }
            else if (suit == "Diamonds")
            {
                return diamondsNine;
            }
        }
        if (rank == "10")
        {
            if (suit == "Spades")
            {
                return spadesTen;
            }
            else if (suit == "Hearts")
            {
                return heartsTen;
            }
            else if (suit == "Clubs")
            {
                return clubsTen;
            }
            else if (suit == "Diamonds")
            {
                return diamondsTen;
            }
        }
        if (rank == "Jack")
        {
            if (suit == "Spades")
            {
                return spadesJack;
            }
            else if (suit == "Hearts")
            {
                return heartsJack;
            }
            else if (suit == "Clubs")
            {
                return clubsJack;
            }
            else if (suit == "Diamonds")
            {
                return diamondsJack;
            }
        }
        if (rank == "Queen")
        {
            if (suit == "Spades")
            {
                return spadesQueen;
            }
            else if (suit == "Hearts")
            {
                return heartsQueen;
            }
            else if (suit == "Clubs")
            {
                return clubsQueen;
            }
            else if (suit == "Diamonds")
            {
                return diamondsQueen;
            }
        }

        if (rank == "King")
        {
            if (suit == "Spades")
            {
                return spadesKing;
            }
            else if (suit == "Hearts")
            {
                return heartsKing;
            }
            else if (suit == "Clubs")
            {
                return clubsKing;
            }
            else if (suit == "Diamonds")
            {
                return diamondsKing;
            }
        }
        return null;
    }
}
