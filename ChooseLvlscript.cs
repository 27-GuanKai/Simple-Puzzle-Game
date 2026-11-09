using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLvlscript : MonoBehaviour
{
    public GameObject startmenu;
    public GameObject lvlchoicemenu;
    // Start is called before the first frame update
    void Start()
    {
        startmenu.SetActive(true);
        lvlchoicemenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void chooselvlpressed()
    {
        startmenu.SetActive(false);
        lvlchoicemenu.SetActive(true);
    }
    public void back()
    {
        startmenu.SetActive(true);
        lvlchoicemenu.SetActive(false);
    }
}
