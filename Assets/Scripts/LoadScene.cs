using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    public string targetScene;
 public void LoadBotScene()
    {
        SceneManager.LoadScene(targetScene);
    }
}
