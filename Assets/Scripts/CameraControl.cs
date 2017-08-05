

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    [SerializeField]
    Vector3 distortion;
    [SerializeField]
    float Lerpage;

    PlayerMovement player;
    GameObject objectsInFront;
    

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
    }

	void Update () {
        Vector3 desiredPos = player.transform.position + distortion;
        this.transform.position = Vector3.Lerp(this.transform.position, desiredPos, Time.deltaTime * Lerpage);

        if (objectsInFront != null)
            objectsInFront.GetComponent<MeshRenderer>().enabled = true;
        objectsInFront = null;

        doRays();
	}

    void doRays()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(this.transform.position, (player.transform.position - this.transform.position), out hitInfo, 1000f))
        {
            if (hitInfo.transform.root.gameObject.tag == "hidable")
            {
                objectsInFront = (hitInfo.transform.root.gameObject);
                objectsInFront.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
