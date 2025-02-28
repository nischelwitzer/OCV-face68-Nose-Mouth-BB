using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyChangeScene : MonoBehaviour
{
    public Object scene;

    void Update()
    {
        if (Input.GetKey("s"))
        {
            if (scene.name != null)
            {
                Debug.Log("Go2Scene: " + scene.name);
                SceneManager.LoadScene(scene.name);
            }
        }
    }
}

