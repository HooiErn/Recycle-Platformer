using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FInishController : MonoBehaviour
{
    public void ReturnMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
