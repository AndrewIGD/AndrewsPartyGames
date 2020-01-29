using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MutatorScene : MonoBehaviour
{
    public string mutatorScene;
    public void ChangeScene()
    {
        SceneManager.LoadScene(mutatorScene);
    }
}
