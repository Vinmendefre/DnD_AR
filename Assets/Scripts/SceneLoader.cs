using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void loadScene(String parent)
    {
        if (parent.Equals("Forest"))
        {
            SceneManager.LoadScene("Plane Tracking Scene", LoadSceneMode.Single);
        } else if (parent.Equals("Dungeon"))
        {
            SceneManager.LoadScene("CombatScene", LoadSceneMode.Single);
        }
        
    }
}
