using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CraftingManager : MonoBehaviour {
    [SerializeField]
    GameObject[] allItems;
    

    public void merge(ItemSpot is1, ItemSpot is2)
    {
        var i1 = is1.item;
        var i2 = is2.item;
        foreach(GameObject GO in allItems)
        {
            Item i = GO.GetComponent<Item>();
            if ((i.child1Name == i1.name && i.child2Name == i2.name) || (i.child1Name == i2.name && i.child2Name == i1.name))
            {
                GameObject g = Instantiate(GO);//TODO: handle weird unity naming convention (Clone)

                g.GetComponent<MeshRenderer>().enabled = false;
                g.GetComponent<MeshCollider>().enabled = false;

                g.name = GO.name;

                Debug.Log("Succesfully created " + i.name + " from " + i1.name + " and " + i2.name+". Returning");
                is2.fillItem(i);
                GameObject.FindObjectOfType<EQ>().refreshItemSpots(is1.item);
                is1.item = null;
                i.OnMerge.Invoke();

                return;
            }
        }
        Debug.Log("Items " + i1.name + " and " + i2.name + " don't make anything. Returning");
    }


    public void GetDrone()
    {
        Debug.Log("you win");
    }
}
