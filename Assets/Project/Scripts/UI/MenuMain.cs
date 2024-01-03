using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour
{
    public void OnClickStartBtn()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
