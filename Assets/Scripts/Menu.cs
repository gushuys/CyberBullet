using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject levelButton;
    public GameObject LevelsMenu;
    public GameObject SettingPos;
    public GameObject MuteButton;
    public int noOfLevels;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Level_Save"))
            PlayerPrefs.SetInt("Level_Save", 1);
        LevelButtons();
    }
    private void LevelButtons()
    {
        for (int i = 0; i < noOfLevels; i++)
        {
            int x = new int();
            x = i + 1;
            GameObject lvlBurron = Instantiate(levelButton);
            lvlBurron.transform.SetParent(LevelsMenu.transform, false);
            lvlBurron.GetComponentInChildren<Text>().text= (i + 1).ToString();
            lvlBurron.GetComponent<Button>().onClick.AddListener(delegate
            {
                LoadLevel(int.Parse(lvlBurron.transform.GetChild(0).GetComponent<Text>().text));
            });

            if (i + 1 > PlayerPrefs.GetInt("Level_Save"))
            {
                lvlBurron.GetComponent<Button>().interactable = false;
            }
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Continue()
    {
        LoadLevel(PlayerPrefs.GetInt("Level_Save"));
    }
    public void LoadLevel(int scene_num)
    {
        SceneManager.LoadScene(scene_num);
    }
    public void Mute()
    {
        if (PlayerPrefs.GetInt("Volume") == 0 || !PlayerPrefs.HasKey("Volume"))
        {
            MuteButton.transform.GetChild(0).GetComponent<Text>().text = "Mute";
                PlayerPrefs.SetInt("Volume", 1);
                
        }
        else if(PlayerPrefs.GetInt("Volume") == 1)
        {
            PlayerPrefs.SetInt("Volume", 0);
            MuteButton.transform.GetChild(0).GetComponent<Text>().text = "Unmute";
        }
        PlayerPrefs.Save();
        AudioListener.volume = PlayerPrefs.GetInt("Volume");
    }
    public void ChangeJoystickPos()
    {
        if (!PlayerPrefs.HasKey("Pos"))
        {
            PlayerPrefs.SetString("Pos", "Left");
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.GetString("Pos")=="Left")
        {
            PlayerPrefs.SetString("Pos", "Right");
        }
        else
        {
            PlayerPrefs.SetString("Pos", "Left");
        }
        PlayerPrefs.Save();
        SettingPos.transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString("Pos");
    }
}
