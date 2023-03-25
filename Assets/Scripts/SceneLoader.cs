using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void loadScene(String parent)
    {
        if (parent.Equals("Forest"))
        {
            //add scenes to be loaded once finished
            SceneManager.LoadScene("CombatScene", LoadSceneMode.Single);
        } else if (parent.Equals("Dungeon"))
        {
            SceneManager.LoadScene("FinalDungeonMap", LoadSceneMode.Single);
        } else if (parent.Equals("Dungeon"))
        {
            SceneManager.LoadScene("CombatScene", LoadSceneMode.Single);
        }
    }
}
