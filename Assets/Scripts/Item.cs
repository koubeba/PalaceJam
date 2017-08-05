using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Item : MonoBehaviour{
    public string child1Name, child2Name;
    [SerializeField] public Texture2D thumbnail;
    [SerializeField]
    public UnityEvent OnMerge;


    public void LeaveJustItem()
    {
        //Destroy(this.GetComponent<Collider>());
        //Destroy(this.GetComponent<MeshRenderer>());
        //Destroy(this.GetComponent<MeshFilter>());

        this.GetComponent<Collider>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;
    }
    

    public void ThrowOut(Vector3 position)
    {
        this.GetComponent<MeshCollider>().enabled = true;
        this.GetComponent<MeshRenderer>().enabled = true;

        this.transform.position = position;
    }
}
