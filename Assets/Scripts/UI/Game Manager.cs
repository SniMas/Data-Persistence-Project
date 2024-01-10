using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;


#if UNITY_EDITOR
using UnityEditor;
#endif
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerData playerData;
    public PlayerData recordData;

    public TextMeshProUGUI inputField;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        this.playerData = new PlayerData();
        LoadRecord();
    }

    public void SetName()
    {
        Instance.playerData.playerName = inputField.text;
    }
    public void LoadGame()
    {
        SetName();
        Debug.Log(GameManager.Instance.playerData.playerName);
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    [System.Serializable]
    public class PlayerData
    {
        public string playerName;
        public int playerScore;
    }

    public void SaveRecord()
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        recordData = playerData;
    }
    public void LoadRecord()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            recordData = JsonUtility.FromJson<PlayerData>(json);
        }
    }
}
