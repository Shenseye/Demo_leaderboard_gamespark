using UnityEngine;
using System.Collections;

public class LeaderBoardEntry : MonoBehaviour {

    public string rankString, usernameString, scoreString, facebookID = "";

    public UILabel rank, username, score;

    //Create this variable so we 
    //can define what textue we are updating
    public UITexture profilePic;

    // Use this for initialization
    void Start () {
        rank.text = rankString;
        username.text = usernameString;
        score.text = scoreString;
        // Defines when the image will be pulled
        if (facebookID != "")
        {
            StartCoroutine(getFBPicture());
        }
    }

    //this is the function that will pull the profile picture from 
    //facebook 
    public IEnumerator getFBPicture()
    {
            var www = new WWW("http://graph.facebook.com/" + facebookID + "/picture?type=square");

            yield return www;
 
            //make sure the dimensions of the new texture match what we set                 
            //up in the Leaderboard Entry prefab
            Texture2D tempPic = new Texture2D(25, 25);

            www.LoadImageIntoTexture(tempPic);
            profilePic.mainTexture = tempPic;
    }
}
