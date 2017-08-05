using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingZyrandol : Interactive {

    bool wasClicked = false;
    [SerializeField] GameObject fallParticles;
    [SerializeField] GameObject ItemPrefab;
    

    public override void OnClicked()
    {
        wasClicked = true;
        GetComponent<Rigidbody>().useGravity = true;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag=="floor")
        {
            Instantiate(fallParticles, this.transform.position, Quaternion.Euler(-90f,0,0));
            Instantiate(ItemPrefab, this.transform.position, Quaternion.identity).name="zyrandol";
            Destroy(gameObject);
        }
        if(col.tag=="player")
        {
            col.GetComponent<PlayerMovement>().Die();
        }
    }
}
