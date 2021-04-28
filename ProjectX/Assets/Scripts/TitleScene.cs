using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Fade.Instance.LoadLevel("MainScene", 1.0f);
        }
    }

    void ChangeScene()
    {
        Fade.Instance.LoadLevel("MainScene", 1.0f);
    }
}
