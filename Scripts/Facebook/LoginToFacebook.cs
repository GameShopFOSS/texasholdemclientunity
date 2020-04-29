using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginToFacebook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var perms = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }
}
