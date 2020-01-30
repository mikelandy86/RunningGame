using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalManager : MonoBehaviour
{
    private static List<Results> _resultsHighScoreList = new List<Results>();
    public GameManager _gameManager;
    private Menu _menu;

    public Text _textHighScoreNameUI;
    public Text _textHighScoreTimeUI;

    public GameObject _parentHighScoreUI;
    public GameObject[] _buttonsRestartAndBackToMenu;
    public GameObject[] _hideUiTexts;
    public GameObject[] _clothFinsishLine;

    public AudioSource _crowdCheerGoal;

    private int _currentPositionInRace = 0;



    private void Start()
    {
        Time.timeScale = 1f;

        foreach (GameObject go in _buttonsRestartAndBackToMenu)
        {
            go.SetActive(false);
        }
        _menu = FindObjectOfType<Menu>();
    }






    // Created a Results Class Which has methods that manages the highscoreInformation.
    public class Results
    {
        private GoalManager goal = new GoalManager();

        private float _timeResults;
        private string _nameOfPlayer;
        
        // Made a Method That Takes Two Arguments and sets Time and name of the current Player
        public void SetPlayerResults(float newTimeResults, string newNameOfGameObject)
        {
            _timeResults = newTimeResults;
            _timeResults = goal.RoundOffToDecimals(_timeResults);
            _nameOfPlayer = newNameOfGameObject;
        }

        public string GetPlayerName()
        {
            return _nameOfPlayer;
        }

        public float GetResultTime()
        {
            return _timeResults;
        }

        public string GetSpecificIndexName(int index)
        {
            return _resultsHighScoreList[index]._nameOfPlayer;
        }

        public float GetSpecificIndexTime(int index)
        {
            return _resultsHighScoreList[index]._timeResults;
        }

        public int GetListLength()
        {
            return _resultsHighScoreList.Count;
        }

        public void LoadSavedHighScoreToList(List<string[]> highscore)
        {
            for (int i = 0; i < highscore.Count; i++)
            {
                Results savedResults = new Results();
                savedResults.SetPlayerResults(float.Parse(highscore[i][0]), highscore[i][1]);
                _resultsHighScoreList.Add(savedResults);
            }
        }





        // Sorting the List of floats from fastest time to the slowest time
        public void SortingTheHighScoreList(List<Results> highscorelist)
        {
            List<Results> swapHelpList = new List<Results>();

            if (_resultsHighScoreList.Count > 1)
            {
                swapHelpList.Add(_resultsHighScoreList[0]);
            }


            for (int i = _resultsHighScoreList.Count - 1; i >= 1; i--)
            {
                if (_resultsHighScoreList[i].GetResultTime() < _resultsHighScoreList[i - 1].GetResultTime())
                {
                    swapHelpList[0] = _resultsHighScoreList[i];
                    _resultsHighScoreList[i] = _resultsHighScoreList[i - 1];
                    _resultsHighScoreList[i - 1] = swapHelpList[0];
                }
            }
        }

        // Method That returns a string Array With the Times And Names of all the records.
        public string[] GetHighScoreList()
        {
            string[] namesAndTimesArray = new string[] { "", "" };
            int positionInTimeScore = 0;
            SortingTheHighScoreList(_resultsHighScoreList);

            foreach (Results result in _resultsHighScoreList)
            {
                positionInTimeScore++;
                namesAndTimesArray[0] += positionInTimeScore.ToString() + "# " + " " + result.GetPlayerName() + "\n";
                namesAndTimesArray[1] += "Time: " + result.GetResultTime() + "\n";
            }
            return namesAndTimesArray;
        }


        public void ClearHighScoreList()
        {
            _resultsHighScoreList.Clear();
        }
    }


    public List<Results> GethighScoreList()
    {
        return _resultsHighScoreList;
    }
   


    public void AddSavedDataToHighScoreList(List<Results> list)
    {
        foreach (Results results in list)
        {
            _resultsHighScoreList.Add(results);
        }
    }



    // When the Player or an Opponent Enters The Goal on Trigger
    private void OnTriggerEnter(Collider other)
    {
        Results newResults = new Results();
        TriggerFinishedLineCloth();

        // If a opponent enters the goal
        if (other.gameObject.GetComponent<OpponentRunning>())
        {
            _currentPositionInRace++;
            float newTime = TimeScore._scoreTime;
            newTime = RoundOffToDecimals(newTime);
            _textHighScoreNameUI.text += _currentPositionInRace + "# " + other.gameObject.name + "\n";
            _textHighScoreTimeUI.text += "Time: " + newTime + "\n";
            other.GetComponent<Rigidbody>().drag = 1.5f;
        }


        // If the Player enters the goal
        if (other.gameObject.GetComponent<Player>())
        {
            SetGameToNormalSpeed();

            _currentPositionInRace++;
            float newTime = TimeScore._scoreTime;
            _crowdCheerGoal.Play();
            HideUIGameObjects();
            newTime = RoundOffToDecimals(newTime);
            other.gameObject.GetComponent<Player>()._speedForceMove = 0f;
            newResults.SetPlayerResults(newTime, Menu.GetPlayerName());
            _resultsHighScoreList.Add(newResults);
            _parentHighScoreUI.SetActive(true);
            _textHighScoreNameUI.text += "<color=red>" + _currentPositionInRace + "# " + Menu.GetPlayerName() + "</color>" + "\n";
            _textHighScoreTimeUI.text += "<color=red>" + "Time: " + newTime + "</color>" + "\n";
            ShowEndOfRaceButtons();
        }
    }

    // Method that Enables the component (Cloth) of the Finish Line Cloth
    private void TriggerFinishedLineCloth()
    {
        if (_clothFinsishLine[_clothFinsishLine.Length - 1].GetComponent<Cloth>().enabled == false)
        {
            foreach (GameObject go in _clothFinsishLine)
            {
                go.GetComponent<Cloth>().enabled = true;
            }
        }
    }

    // Hides The UI GameObjects That is in the "_hideUiTexts" Array
    private void HideUIGameObjects()
    {
        foreach (GameObject item in _hideUiTexts)
        {
            item.SetActive(false);
        }
    }

    // Displays The UI Buttons
    private void ShowEndOfRaceButtons()
    {
        foreach (GameObject go in _buttonsRestartAndBackToMenu)
        {
            go.SetActive(true);
        }
    }

    // Method That Sets The Game To Normal Speed
    public void SetGameToNormalSpeed()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }

    private float RoundOffToDecimals(float time)
    {
        float newTime = Mathf.Round(time * 100f) / 100f;
        return newTime;
    }
}

