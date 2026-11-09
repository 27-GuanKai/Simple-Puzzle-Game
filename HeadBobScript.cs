using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobScript : MonoBehaviour
{
    [Range(10f, 100f)]

    Vector3 startpos;
    public float Smooth = 10f;
    public PlayerMovement PlayerMovementScript;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForHeadbobTrigger();
    }
    private void CheckForHeadbobTrigger()
    {
        
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

        if (inputMagnitude > 0&& PlayerMovementScript.Air_Check == false && PlayerMovementScript.Crouch_Check == false && PlayerMovementScript.Walk_Check == true)
        {
            startwalkheadbob();
        }
        if (inputMagnitude > 0 && PlayerMovementScript.Air_Check == false && PlayerMovementScript.Crouch_Check == false &&PlayerMovementScript.Run_Check == true)
        {
            startRunheadbob();
        }
        if (inputMagnitude > 0 && PlayerMovementScript.Air_Check == false && PlayerMovementScript.Crouch_Check == true)
        {
            startCrouchheadbob();
        }
        else if(inputMagnitude > 0 && PlayerMovementScript.Air_Check == true)
        {
            stopheadbob();
        }
    }
    private Vector3 startwalkheadbob()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time *10f) * 0.0035f * 1.4f, Smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.y, Mathf.Cos(Time.time * 10f/ 2f) * 0.0035f * 1.6f, Smooth * Time.deltaTime);
        transform.localPosition += pos;

        return pos;
    }
    private Vector3 startRunheadbob()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * 20f) * 0.005f * 1.4f, Smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.y, Mathf.Cos(Time.time * 20f / 2f) * 0.005f * 1.6f, Smooth * Time.deltaTime);
        transform.localPosition += pos;

        return pos;
    }
    private Vector3 startCrouchheadbob()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * 5f) * 0.0035f * 1.4f, Smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.y, Mathf.Cos(Time.time * 5f / 2f) * 0.0035f * 1.6f, Smooth * Time.deltaTime);
        transform.localPosition += pos;

        return pos;
    }
    private void stopheadbob()
    {
        if (transform.localPosition == startpos) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, startpos, 1 * Time.deltaTime);
    }
}
