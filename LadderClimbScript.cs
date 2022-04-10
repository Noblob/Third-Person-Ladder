using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimbScript : MonoBehaviour
{
    public float open = 100f;
    public float range = 0.5f;
    public bool TouchingWall = false;
    public float UpwardSpeed = 3.3f;
    public Camera LadderCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();

        if(Input.GetKey("w") & TouchingWall == true)
        {
            StartCoroutine(StartClimb());
        }

        if(Input.GetKeyUp("w"))
        {
            GetComponent<Rigidbody>().isKinematic = false;
            TouchingWall = false;
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(LadderCam.transform.position, LadderCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                TouchingWall = true;
            }
        }
    }

    IEnumerator StartClimb()
    {
        transform.position += Vector3.up * Time.deltaTime * UpwardSpeed;
        GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(0.85f);
        TouchingWall = false;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
