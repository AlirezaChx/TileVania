using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSaves : MonoBehaviour
{
    void Awake()
    {
        int sceneSavesCounts = FindObjectsOfType<SceneSaves>().Length;
        if (sceneSavesCounts>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetSceneSaves()
    {
        Destroy(gameObject);
    }
}
