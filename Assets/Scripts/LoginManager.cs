using UnityEngine;
using System.Collections;
using Facebook;
using GameSparks.Api.Requests;

public class LoginManager : MonoBehaviour
{

    // Use this for initialization
    public void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(FacebookLogin);
        }
        else
        {
            FacebookLogin();
        }
    }


    public void FacebookLogin()
    {
        if (!FB.IsLoggedIn)
        {
            FB.Login("", GameSparksLogin);
        }
    }

    public void GameSparksLogin(FBResult result)
    {
        if (FB.IsLoggedIn)
        {

            new FacebookConnectRequest().SetAccessToken(FB.AccessToken).Send((response) =>
            {
                if (response.HasErrors)
                {
                    Debug.Log("Something failed when connectiong with Facebook");
                }
                else
                {
                    Debug.Log("GameSparks Facebook Login Successful");
                }
            });

        }
    }
}
