using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public InputField nameInput;

    // Start is called before the first frame update
    void Start()
    {
        //DataPersistence.Instance.LoadPlayerName();
        nameInput.text = DataPersistence.Instance.playerName;
    }

    // Update is called once per frame
    void Update()
    {
        DataPersistence.Instance.playerName = nameInput.text;
    }

    //public void SaveNewName()
    //{
    //    DataPersistence.Instance.playerName = nameInput.GetComponent<InputField>().text;
    //    Debug.Log("PlayerName = " + DataPersistence.Instance.playerName);
    //}


    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        DataPersistence.Instance.PersistBestScores();
        //MainManager.Instance.SaveColor();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
