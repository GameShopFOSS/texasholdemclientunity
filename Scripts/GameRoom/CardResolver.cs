using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardResolver : MonoBehaviour
{
    public Image spadesAce;
    public Image spadesTwo;
    public Image spadesThree;
    public Image spadesFour;
    public Image spadesFive;
    public Image spadesSix;
    public Image spadesSeven;
    public Image spadesEight;
    public Image spadesNine;
    public Image spadesTen;
    public Image spadesJack;
    public Image spadesQueen;
    public Image spadesKing;

    public Image clubsAce;
    public Image clubsTwo;
    public Image clubsThree;
    public Image clubsFour;
    public Image clubsFive;
    public Image clubsSix;
    public Image clubsSeven;
    public Image clubsEight;
    public Image clubsNine;
    public Image clubsTen;
    public Image clubsJack;
    public Image clubsQueen;
    public Image clubsKing;

    public Image heartsAce;
    public Image heartsTwo;
    public Image heartsThree;
    public Image heartsFour;
    public Image heartsFive;
    public Image heartsSix;
    public Image heartsSeven;
    public Image heartsEight;
    public Image heartsNine;
    public Image heartsTen;
    public Image heartsJack;
    public Image heartsQueen;
    public Image heartsKing;

    public Image diamondsAce;
    public Image diamondsTwo;
    public Image diamondsThree;
    public Image diamondsFour;
    public Image diamondsFive;
    public Image diamondsSix;
    public Image diamondsSeven;
    public Image diamondsEight;
    public Image diamondsNine;
    public Image diamondsTen;
    public Image diamondsJack;
    public Image diamondsQueen;
    public Image diamondsKing;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(spadesAce);
        Instantiate(heartsAce);
        Instantiate(clubsAce);
        Instantiate(diamondsAce);
        Instantiate(spadesTwo);
        Instantiate(heartsTwo);
        Instantiate(clubsTwo);
        Instantiate(diamondsTwo);
        Instantiate(spadesThree);
        Instantiate(heartsThree);
        Instantiate(diamondsThree);
       
                 Instantiate(spadesFour);
           
                 Instantiate(heartsFour);
            
             Instantiate(clubsFour);
            
            Instantiate(diamondsFour);
        
       
                 Instantiate(spadesFive);
          
                 Instantiate(heartsFive);
          Instantiate(clubsFive);
             Instantiate(diamondsFive);
            
        

         Instantiate(spadesSix);
            Instantiate(heartsSix);
            Instantiate(clubsSix);
           Instantiate(diamondsSix);
         Instantiate(spadesSeven);
           Instantiate(clubsSeven);
             Instantiate(diamondsSeven);
         Instantiate(spadesEight);
             Instantiate(heartsEight);
            Instantiate(clubsEight);
            Instantiate(diamondsEight);
            Instantiate(spadesNine);
             Instantiate(heartsNine);
            Instantiate(clubsNine);
            Instantiate(diamondsNine);
        Instantiate(spadesTen);
             Instantiate(heartsTen);
            Instantiate(clubsTen);
             Instantiate(diamondsTen);
             Instantiate(spadesJack);
             Instantiate(heartsJack);
            Instantiate(clubsJack);
            Instantiate(diamondsJack);
            Instantiate(spadesQueen);
             Instantiate(heartsQueen);
           Instantiate(clubsQueen);
            Instantiate(diamondsQueen);
            Instantiate(spadesKing);
             Instantiate(heartsKing);
           Instantiate(clubsKing);
             Instantiate(diamondsKing);
            
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Image resolveWhichCard(string suit, string rank)
    {
        if (rank == "Ace") {
            if (suit == "Spades")
            {
                return (spadesAce);
            }
            else if (suit == "Hearts")
            {
                return (heartsAce);
            }
            else if (suit == "Clubs")
            {
                return (clubsAce);
            }
            else if(suit == "Diamonds")
            {
                return (diamondsAce);
            }
        }
        if (rank == "2")
        {
            if (suit == "Spades")
            {
                return (spadesTwo);
            }
            else if (suit == "Hearts")
            {
                return (heartsTwo);
            }
            else if (suit == "Clubs")
            {
                return (clubsTwo);
            }
            else if (suit == "Diamonds")
            {
                return (diamondsTwo);
            }
        }
        if (rank == "3")
        {
            if (suit == "Spades")
            {
                return (spadesThree);
            }
            else if (suit == "Hearts")
            {
                return (heartsThree);
            }
            else if (suit == "Clubs")
            {
                return (clubsThree);
            }
            else if (suit == "Diamonds")
            {
                return (diamondsThree);
            }
        }

        if (rank == "4")
        {
            if (suit == "Spades")
            {
                return (spadesFour);
            }
            else if (suit == "Hearts")
            {
                return (heartsFour);
            }
            else if (suit == "Clubs")
            {
                return (clubsFour);
            }
            else if (suit == "Diamonds")
            {
                return (diamondsFour);
            }
        }

        if (rank == "5")
        {
            if (suit == "Spades")
            {
                return (spadesFive);
            }
            else if (suit == "Hearts")
            {
                return (heartsFive);
            }
            else if (suit == "Clubs")
            {
                return (clubsFive);
            }
            else if (suit == "Diamonds")
            {
                return (diamondsFive);
            }
        }

        if (rank == "6")
        {
            if (suit == "Spades")
            {
                return (spadesSix);
            }
            else if (suit == "Hearts")
            {
                return (heartsSix);
            }
            else if (suit == "Clubs")
            {
                return (clubsSix);
            }
            else if (suit == "Diamonds")
            {
                return (diamondsSix);
            }
        }
        if (rank == "7")
        {
            if (suit == "Spades")
            {
                return (spadesSeven);
            }
            else if (suit == "Hearts")
            {
                return (heartsSeven);
            }
            else if (suit == "Clubs")
            {
                return (clubsSeven);
            }
            else if (suit == "Diamonds")
            {
                return (diamondsSeven);
            }
        }
        if (rank == "8")
        {
            if (suit == "Spades")
            {
                return (spadesEight);
            }
            else if (suit == "Hearts")
            {
                return (heartsEight);
            }
            else if (suit == "Clubs")
            {
                return (clubsEight);
            }
            else if (suit == "Diamonds")
            {
                return (diamondsEight);
            }
        }
        if (rank == "9")
        {
            if (suit == "Spades")
            {
                return (spadesNine);
            }
            else if (suit == "Hearts")
            {
                return (heartsNine);
            }
            else if (suit == "Clubs")
            {
                return (clubsNine);
            }
            else if (suit == "Diamonds")
            {
                return (diamondsNine);
            }
        }
        if (rank == "10")
        {
            if (suit == "Spades")
            {
                return (spadesTen);
            }
            else if (suit == "Hearts")
            {
                return (heartsTen);
            }
            else if (suit == "Clubs")
            {
                return (clubsTen);
            }
            else if (suit == "Diamonds")
            {
                return (diamondsTen);
            }
        }
        if (rank == "Jack")
        {
            if (suit == "Spades")
            {
                return (spadesJack);
            }
            else if (suit == "Hearts")
            {
                return (heartsJack);
            }
            else if (suit == "Clubs")
            {
                return (clubsJack);
            }
            else if (suit == "Diamonds")
            {
                return (diamondsJack);
            }
        }
        if (rank == "Queen")
        {
            if (suit == "Spades")
            {
                return (spadesQueen);
            }
            else if (suit == "Hearts")
            {
                return (heartsQueen);
            }
            else if (suit == "Clubs")
            {
                return (clubsQueen);
            }
            else if (suit == "Diamonds")
            {
                return (diamondsQueen);
            }
        }

        if (rank == "King")
        {
            if (suit == "Spades")
            {
                return (spadesKing);
            }
            else if (suit == "Hearts")
            {
                return (heartsKing);
            }
            else if (suit == "Clubs")
            {
                return (clubsKing);
            }
            else if (suit == "Diamonds")
            {
                return (diamondsKing);
            }
        }
        return null;
    }
}
