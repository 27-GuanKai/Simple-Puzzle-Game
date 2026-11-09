using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class scenemanagerlvl1 : MonoBehaviour
{
    public void startgame1()
    {
        SceneManager.LoadScene("lvl 1");
    }
    public void exitgame()
    {
        Application.Quit();
    }
    public void startgame2()
    {
        SceneManager.LoadScene("lvl 2");
    }
    public void startgame3()
    {
       
    }
    public void startgame4()
    {

    }
    public void tutorial()
    {
        SceneManager.LoadScene("tutoriallvl");
    }
    public void menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

}
