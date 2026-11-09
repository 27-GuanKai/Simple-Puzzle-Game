using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CubeCollisionScript : MonoBehaviour
{
    public float score = 0;
    public Text ScoreText;
    public float scenechange = 10;
    public Text scenechangetext;
    public scenemanagerlvl1 sm;
    // Start is called before the first frame update
    void Start()
    {
        scenechange = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        maxscore();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ("CubeScore"))
        {
            score += 1;
            Destroy(other.gameObject);
            ScoreText.text = ("Score: " + score);
        }
    }
    void maxscore()
    {
        if (score == 10f)
        {
            scenechange -= Time.fixedDeltaTime;
            scenechangetext.text = ("Intermission to Next LVL in " + scenechange);
            if (scenechange <= 0)
            {
                scenechange += 0;
                sm.startgame2();
                scenechangetext.text = ("");
            }
        }
    }
}



