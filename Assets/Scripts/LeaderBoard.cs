using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameSparks.Api.Requests;

public class LeaderBoard : MonoBehaviour {

    public GameObject leaderboardEntryPrefab;

    //NGUI grid, this will re-organise the leaderboard whenever 
    //a new entry is added
    public UIGrid leaderboardGrid;

    //creates a list of GameObjects. whenever the leaderboard is called it w       //ill clear out the old enteries and pull in the new. This will also keep      //track of the enteries so they can be deleted in the future
    public List<GameObject> entries = new List<GameObject>();

    void Start()
    {
        //re-arranges all currently stored game objects that are 
        //children of the leaderboard grid
        leaderboardGrid.Reposition();
    }

    public void GetLeaderboard()
    {
        //the leaderboard entries could contain old data. 
        //First destroy all existing Leaderboard objects
        for (int i = 0; i < entries.Count; i++)
        {
            Destroy(entries[i]);
        }

        //because we deleted the objects contained on the list, the list                //now contains nul references and has to be cleared so the new 
        //high score objects can be added
        entries.Clear();

        //pulls in the Leaderboard information from Gamesparks, 
        //identified by the short code added when setting up the 
        //Leaderboard in the Gamesparks portal
        //SetEntryCount() will define how many entries you want to pull in one go
        new LeaderboardDataRequest_highScoreLeaderboard().SetEntryCount(10).Send((response) => {

            //what we will do with the information given by GameSparks
            foreach (var entry in response.Data)
            {
                //only children of the NGUI grid will be added 
                //to the grid. We make new objects with our
                //LeaderboardEntry script and add them as 
                //children to the NGUI Leaderboard Grid
                GameObject go = NGUITools.AddChild(leaderboardGrid.gameObject, leaderboardEntryPrefab);

                go.GetComponent<LeaderBoardEntry>().rankString = entry.Rank.ToString();
                go.GetComponent<LeaderBoardEntry>().usernameString = entry.UserName.ToString();
                //the score string has to be added as a number value 
                //based on the short code used for the attributed 
                //to the leaderboard we are pulling from
                go.GetComponent<LeaderBoardEntry>().scoreString = entry.GetNumberValue("score").ToString();
                //this is the line we add to pull the facebookID from GameSparks.
                go.GetComponent<LeaderBoardEntry>().facebookID = entry.ExternalIds.GetString("FB");

                //adds the gameobject to the list of entries
                entries.Add(go);

                //repositions the new object so it isn't stacked                                
                //over existing objects
                //created by the loop
                leaderboardGrid.Reposition();
                Debug.Log(entry.GetNumberValue("score").ToString());
            }
            Debug.Log(response);
        });


    }
}
