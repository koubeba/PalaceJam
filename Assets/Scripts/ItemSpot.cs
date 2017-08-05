using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpot : MonoBehaviour {
    public Item item;
    

    private void Start()
    {
        if (item != null) fillItem(item);
    }

    public void fillItem(Item i)
    {
        item = i;
        GetComponent<RawImage>().texture = item.thumbnail;
        //load graphics
    }

}
