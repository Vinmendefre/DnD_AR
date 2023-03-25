using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void loadScene(String parent)
    {
        if (parent.Equals("Forest"))
        {
            SceneManager.LoadScene("FinalForesttMap", LoadSceneMode.Single);
        } else if (parent.Equals("Dungeon"))
        {
            SceneManager.LoadScene("FinalDungeonMap", LoadSceneMode.Single);
        } else if (parent.Equals("Desert"))
        {
            SceneManager.LoadScene("FinalDesertMap", LoadSceneMode.Single);
        }
    }
}
