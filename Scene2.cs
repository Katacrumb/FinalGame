using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2 : MonoBehaviour

{

    public int sceneNum;
    public void OnTriggerEnter2D(Collider2D collider)
    {
        SceneManager.LoadScene(sceneNum);
    }

}
