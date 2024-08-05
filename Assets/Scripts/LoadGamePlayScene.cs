using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadGamePlayScene : MonoBehaviour
{
    public string targetScene;
    public float timeToLoadScene = 15f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadGamePlaySceneVoid", timeToLoadScene);
    }

    void LoadGamePlaySceneVoid()
    {
        SceneManager.LoadScene(targetScene);
    }
}
