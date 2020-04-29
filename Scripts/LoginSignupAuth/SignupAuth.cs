using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BestHTTP;

public class SignupAuth : MonoBehaviour
{
    string email;
    string firstname;
    string lastname;
    string password;
    string cardNumber;
    string cardDate;
    string cardCVV;

    public Text firstNameText;
    public Text lastNameText;
    public Text passwordText;
    public Text confirmPasswordText;
    public Image popUp;
    public Text popUpText;

    public Text emailText;
    public Text cardNumberText;
    public Text cardMonthText;
    public Text cardYearText;

    public Image details;

    public Text cardCVVText;
    public Image popUpDetail;
    public Text popUpDetailText;

    public EncryptAndDecryptAES encryptor;
    public ConnectionPoller connectionPoller;
    //string chips;
    //string vipLevel;
    //string hasReceivedSigninBonus;
    //string hasReceivedPurchaseBonus;
    //string giftValuesOrMerchandiseAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Submit()
    {
        //bool containsInt = "your string".Any(char.IsDigit);
        if (firstNameText.text == "")
        {
            popUpText.text = "Enter first name!";
            popUp.gameObject.SetActive(true);
            return;
        }

        if (firstNameText.text.Any(char.IsDigit))
        {
            popUpText.text = "First name cannot have number!";
            popUp.gameObject.SetActive(true);
            return;
        }

        if (firstNameText.text.Any(char.IsSymbol))
        {
            popUpText.text = "First name cannot have symbol!";
            popUp.gameObject.SetActive(true);
            return;
        }

        if (firstNameText.text.Any(char.IsPunctuation))
        {
            popUpText.text = "First name cannot have punctuation!";
            popUp.gameObject.SetActive(true);
            return;
        }

        if (lastNameText.text == "")
        {
            popUpText.text = "Enter last name!";
            popUp.gameObject.SetActive(true);
            return;
        }

        if (lastNameText.text.Any(char.IsDigit))
        {
            popUpText.text = "Last name cannot have number!";
            popUp.gameObject.SetActive(true);
            return;
        }

        if (lastNameText.text.Any(char.IsSymbol))
        {
            popUpText.text = "Last name cannot have symbol!";
            popUp.gameObject.SetActive(true);
            return;
        }

        if (lastNameText.text.Any(char.IsPunctuation))
        {
            popUpText.text = "Last name cannot have punctuation!";
            popUp.gameObject.SetActive(true);
            return;
        }

        if (passwordText.text == "")
        {
            popUpText.text = "Enter password!";
            popUp.gameObject.SetActive(true);
            return;
        }

        if (confirmPasswordText.text == "")
        {
            popUpText.text = "Enter confirm password!";
            popUp.gameObject.SetActive(true);
            return;
        }

        if (passwordText.text != confirmPasswordText.text)
        {
            popUpText.text = "Password and Confirm mismatch!";
            popUp.gameObject.SetActive(true);
            return;
        }

        firstname = firstNameText.text;
        lastname = lastNameText.text;
        password = passwordText.text;

        details.gameObject.SetActive(true);
    }

    public void ContinueButton()
    {
        popUp.gameObject.SetActive(false);
    }

    public void SubmitDetail()
    {
        if (emailText.text == "")
        {
            popUpDetailText.text = "Enter email!";
            popUpDetail.gameObject.SetActive(true);
            return;
        }

        if (cardNumberText.text.Trim().Length != 16)
        {
            popUpDetailText.text = "Card Number must be 16 digits, no spaces!";
            popUpDetail.gameObject.SetActive(true);
            return;
        }

        if (!cardNumberText.text.All(char.IsDigit))
        {
            popUpDetailText.text = "Card number must be all numbers";
            popUpDetail.gameObject.SetActive(true);
            return;
        }

        if (cardMonthText.text.Trim().Length != 2)
        {
            popUpDetailText.text = "Card month must be 2 digits, no spaces!";
            popUpDetail.gameObject.SetActive(true);
            return;
        }

        if (!cardMonthText.text.All(char.IsDigit))
        {
            popUpDetailText.text = "Card month must be all numbers";
            popUpDetail.gameObject.SetActive(true);
            return;
        }
        if (cardYearText.text.Trim().Length != 2)
        {
            popUpDetailText.text = "Card month must be 2 digits, no spaces!";
            popUpDetail.gameObject.SetActive(true);
            return;
        }

        if (!cardYearText.text.All(char.IsDigit))
        {
            popUpDetailText.text = "Card year must be all numbers";
            popUpDetail.gameObject.SetActive(true);
            return;
        }

        if (cardCVVText.text.Trim().Length != 3)
        {
            popUpDetailText.text = "Card CVV must be 3 digits, no spaces!";
            popUpDetail.gameObject.SetActive(true);
            return;
        }

        if (!cardCVVText.text.All(char.IsDigit))
        {
            popUpDetailText.text = "Card CVV must be all numbers";
            popUpDetail.gameObject.SetActive(true);
            return;
        }

       

        email = emailText.text;
        cardNumber = cardNumberText.text;
        string cardDateString = cardMonthText.text + cardYearText.text;
        cardDate = cardDateString;
        cardCVV = cardCVVText.text;

        
          
        
        SendToServer();
    }

    public void ContinueDetail()
    {
        popUpDetail.gameObject.SetActive(false);
    }

    public void SendToServer()
    {
        //string encryptedmessage = encryptor.Encrypt("Hello");

        //Debug.Log(encryptedmessage);


        HTTPRequest request = new HTTPRequest(new Uri("http://34.74.31.140/nodejsApp/signup"), HTTPMethods.Post, OnRequestFinished);
        request.AddField("email", email);
        request.AddField("firstname", firstname);
        request.AddField("lastname", lastname);
        request.AddField("password", encryptor.Encrypt(password));
        request.AddField("cardNumber", encryptor.Encrypt("" + cardNumber));
        request.AddField("cardDate", encryptor.Encrypt("" + cardDate));
        request.AddField("cardCVV", encryptor.Encrypt ("" + cardCVV));
      
        request.Send();
    }

    void OnRequestFinished(HTTPRequest request, HTTPResponse response)
    {
        Debug.Log("Request Finished! Text received: " + response.DataAsText);
        if (response.DataAsText == "OK")
        {
            connectionPoller.email = email;
            connectionPoller.password = encryptor.Encrypt(password);
            connectionPoller.gameObject.SetActive(true);
            DontDestroyOnLoad(connectionPoller.gameObject);
            connectionPoller.gameStateManager.TransitionSceneState("MainMenu", connectionPoller.email, connectionPoller.password);
            //if (connectionPoller.gameStateManager.gameScene == "MainMenu")
            //{
            //    SceneManager.LoadScene("MainMenuScene");
            //}
        }
        else if (response.DataAsText == "Email Already Exists")
        {
            popUpDetailText.text = "Email Already Exists";
            popUpDetail.gameObject.SetActive(true);
        }
        else if (response.DataAsText == "Email checked, problem signing up")
        {
            popUpDetailText.text = "Email checked, problem signing up";
            popUpDetail.gameObject.SetActive(true);
            details.gameObject.SetActive(false);

        }
    }
}
