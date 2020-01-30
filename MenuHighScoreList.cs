using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHighScoreList : MonoBehaviour
{
    public List<string[]> _listOfCompleteHighscoreCopy = new List<string[]>();

    private GoalManager _goalObject;
    private GoalManager.Results results = new GoalManager.Results();

    public Text _highScoreListNames;
    public Text _highScoreListTimes;

    private float[] _highScoreListArray;
    private int _highscoreMenuListLength = 8;



    private void Start()
    {
        PlayerData data = SaveSystem.LoadHighScoreList();

        // After the user plays the first game after started the game it saves The new Highscore And Updates The List
        if (results.GetListLength() > 0)
        {
            SaveHighScoreList(); 
            LoadSavedHighscoreList();
            FillEmptySpaceInHighScore(results.GetListLength());
        }


        // When Game starts. It Loads The Saved Highscore List From The Binary File
        else if (results.GetListLength() == 0)
        {
            LoadSavedHighscoreList();
            FillEmptySpaceInHighScore(data._highScoreList.Count);
        }
    }




    // Makes a copy of the static Highscore List in GoalManager.
    private void CopyOrginalHighScoreList()
    {
            _listOfCompleteHighscoreCopy.Clear();
        for (int i = 0; i < results.GetListLength(); i++)
        {
            string[] _timesAndNames = new string[2];
            _timesAndNames[0] = results.GetSpecificIndexTime(i).ToString();
            _timesAndNames[1] = results.GetSpecificIndexName(i);
            _listOfCompleteHighscoreCopy.Add(_timesAndNames);
        }
    }




    // Copys The orginal static Highscore list from GoalManager and then Saves The List With The Save System Class
    public void SaveHighScoreList()
    {
        CopyOrginalHighScoreList();
        SaveSystem.SaveHighScoreList(this);
    }




    // Reset The HighScore List And Saves The Empty List 
    public void ResetHighScoreList()
    {
        results.ClearHighScoreList();
        _listOfCompleteHighscoreCopy.Clear();
        SaveSystem.SaveHighScoreList(this);
        _highScoreListNames.text = "";
        _highScoreListTimes.text = "";
        FillEmptySpaceInHighScore(0);
    }




    // Method That Fills The Empty Space of The Highscore List in the menu with dots instead of names And Times
    private void FillEmptySpaceInHighScore(int list)
    {
        int rank = list;
        for (int i = 0; i < _highscoreMenuListLength; i++)
        {
             rank++;
            _highScoreListNames.text += rank.ToString() + "# " + " " + "...." + "\n";
            _highScoreListTimes.text += "Time: " + "...." + "\n";
        }
    }




    // Loads The Highscore List from The SaveSystem Class 
    public void LoadSavedHighscoreList()
    {
        PlayerData data = SaveSystem.LoadHighScoreList();

        if (data._highScoreList != null)
        {
            int positionInTimeScore = 0;
            results.ClearHighScoreList();
            _highScoreListNames.text = "";
            _highScoreListTimes.text = "";
            results.LoadSavedHighScoreToList(data._highScoreList);
            for (int i = 0; i < data._highScoreList.Count; i++)
            {
                positionInTimeScore++;

                _highScoreListNames.text = results.GetHighScoreList()[0];
                _highScoreListTimes.text = results.GetHighScoreList()[1];
            }
        }

    }
}
