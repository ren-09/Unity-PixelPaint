using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] GameObject ButtonSummary;
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject MenuBackgroundButton;
    GameObject[] levelButtons;

    public static int numberOfLevelButton;

    int score = GameSceneController.score;


    void Awake()
    {
        levelButtons = GameObject.FindGameObjectsWithTag("LevelButton");
        numberOfLevelButton = levelButtons.Length;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnMenuButton()
    {
        // GameObject MenuButton = GameObject.Find("MenuButton");
        if (MenuPanel.activeSelf)
        {
            MenuBackgroundButton.SetActive(false);
            MenuPanel.SetActive(false);
        }
        else
        {
            MenuBackgroundButton.SetActive(true);
            MenuPanel.SetActive(true);
        }
    }

    public void OnMenuBackgroundButton()
    {
        MenuBackgroundButton.SetActive(false);
        MenuPanel.SetActive(false);
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnXButton()
    {
        MenuPanel.SetActive(false);
        MenuBackgroundButton.SetActive(false);
    }

    public void OnFromStartButton()
    {
        GameSceneController.level = 1;
        SceneManager.LoadScene("GameScene");
    }
}
