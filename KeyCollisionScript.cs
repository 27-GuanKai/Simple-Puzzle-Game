using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollisionScript : MonoBehaviour
{
    private GameObject Key;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y <= -0.2)
        {
            transform.position = new Vector3(0f, 1f, 0f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Door")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
