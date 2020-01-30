using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<string[]> _highScoreList = new List<string[]>();

    public PlayerData (MenuHighScoreList highscore)
    {
        for (int i = 0; i < highscore._listOfCompleteHighscoreCopy.Count; i++)
        {
            _highScoreList.Add(highscore._listOfCompleteHighscoreCopy[i]); // Copys and save the Highscore List from the "MenuHighScoreList Class" to Binary Files
        }
    }
}
