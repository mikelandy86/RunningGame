using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    OpponentRunning _opponentRunning;
    public GameManager _gameManager;
    private static string _playerName;

    public GameObject _enterYourNameGameObject;
    public GameObject _buttonGoBack;
    public GameObject _startGameButton;
    public GameObject _howToPlayGameObject;
    public InputField _inputFieldText;

    public Button[] _menuButtons;
    public Button[] _difficultyButtons;

    private Color _selectedColor;

    private bool _clickedStartButton;
    private bool _easyDifficultySelected;
    private bool _normalDifficultySelected;
    private bool _hardDifficultySelected;

    // Adds sliders in inspector to be able to change the diffculty levels value

    [Range(10.0f, 25f)]
    public float _difficultyEasy = 12f;
    [Range(10.0f, 25f)]
    public float _difficultyNormal = 16f;
    [Range(10.0f, 25f)]
    public float _difficultyHard = 19f;
    [Range(10.0f, 25f)]
    public float _difficultyVeryHard = 23f;


    private void Start()
    {
        _clickedStartButton = false;
        DifficultyModeNormal();
    }

    // Method That Saves The Players Name in the static String Variable
    public void SetPlayerName(string namePlayer)
    {
        _playerName = namePlayer;
    }

    public static string GetPlayerName()
    {
        return _playerName;
    }

    public void EnterYourName()
    {
        foreach (Button item in _menuButtons)
        {
            item.interactable = false;
        }
        _menuButtons[0].GetComponent<Image>().color = new Color(255f, 0f, 0f);
        _enterYourNameGameObject.SetActive(true);
        _buttonGoBack.SetActive(true);
        StartCoroutine(EnterYournameCoroutine());
        _inputFieldText.characterLimit = 3;
    }


    public void ClickedStartButton()
    {
        _clickedStartButton = true;
    }


    IEnumerator EnterYournameCoroutine()
    {
        _inputFieldText.Select();
        _inputFieldText.ActivateInputField();
        while (true)
        {
            _inputFieldText.Select();
            _inputFieldText.ActivateInputField();
            _inputFieldText.text = _inputFieldText.text.ToUpper();
           
            // The Start Button only Displays if the user have enter three Characthers
            if (_inputFieldText.text.Length >= 3) 
            {
                _startGameButton.SetActive(true);
                SetPlayerName(_inputFieldText.text);
                if (_clickedStartButton)
                {
                    yield return new WaitForSeconds(1);
                    _gameManager.StartNormalMod();
                }
            }

            if (_inputFieldText.text.Length < 3)
            {
                _startGameButton.SetActive(false);
            }

            yield return 0f;
        }
    }


    // HighLights The selected Diffculty Button With a Color
    private void ChangeSelectedDifficultyColorButton(string buttonTag)
    {
        Color _nonSelectedColor = new Color(100f, 100f, 100f);
        Color _selectedColor = new Color(255f, 0f, 0f);

        foreach (Button b in _difficultyButtons)
        {
            b.GetComponent<Image>().color = _nonSelectedColor;
        }

        for (int i = 0; i < _difficultyButtons.Length; i++)
        {
            if (_difficultyButtons[i].tag == buttonTag)
            {
                _difficultyButtons[i].GetComponent<Image>().color = _selectedColor;
            }
        }
    }

    // Methods That changes the difficulty Levels
    public void DifficultyModeEasy()
    {
        ChangeSelectedDifficultyColorButton("Easy");
            ChangeDifficulty(_difficultyEasy, (_difficultyEasy * _difficultyEasy));
    }

    public void DifficultyModeNormal()
    {
        ChangeSelectedDifficultyColorButton("Normal");
            ChangeDifficulty(_difficultyNormal, (_difficultyNormal * _difficultyNormal));
    }

    public void DifficultyModeHard()
    {
        ChangeSelectedDifficultyColorButton("Hard");
            ChangeDifficulty(_difficultyHard, (_difficultyHard * _difficultyHard));
    }

    public void DifficultyModeVeryHard()
    {
        ChangeSelectedDifficultyColorButton("VeryHard");
            ChangeDifficulty(_difficultyVeryHard, (_difficultyVeryHard * _difficultyVeryHard));
        
    }
    
    // Methods That Changes speed of the Players Opponent Script
    private void ChangeDifficulty(float velocity, float speed)
    {
        OpponentRunning._maxVelocity = velocity;
        OpponentRunning._speedForce = speed;
    }


    public void GoBackToMenuFromInputNameStage()
    {
        _menuButtons[0].GetComponent<Image>().color = new Color(255f, 255f, 255f);
        _inputFieldText.text = "";
        _buttonGoBack.SetActive(false);
        _startGameButton.SetActive(false);
        foreach (Button item in _menuButtons)
        {
            item.interactable = true;
        }
        _enterYourNameGameObject.SetActive(false);
    }


    // Displays The HowToPlayUI TEXT
    public void HowToPlayShowUI()
    {
        if (_howToPlayGameObject)
        {
            _howToPlayGameObject.SetActive(true);
        }
    }
    // Hides The HowToPlayUI TEXT
    public void HowToPlayHideUI()
    {
        if (_howToPlayGameObject)
        {
            _howToPlayGameObject.SetActive(false);
        }
    }

}
