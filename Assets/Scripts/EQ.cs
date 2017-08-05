using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EQ : MonoBehaviour {
    ItemSpot selectedSpot;
    CraftingManager CM;
    Button[] eqSpots;
    [SerializeField]
    public Texture2D basic;


    void Start()
    {
        selectedSpot = null;
        CM = GameObject.FindObjectOfType<CraftingManager>();
        eqSpots = GetComponentsInChildren<Button>();
    }

	public void ItemSpotClicked(ItemSpot IS)
    {
        Debug.Log(IS.name + " clicked");
        if(IS.item!=null)
        {
            if (selectedSpot == null)
                selectedSpot = IS;
            else
            {
                CM.merge(selectedSpot, IS);
                selectedSpot = null;
            }
        }
    }

    public bool PickUpItem(Item i)
    {
        foreach(Button b in eqSpots)
        {
            ItemSpot IS = b.GetComponent<ItemSpot>();
            if (IS.item == null)
            {
                IS.fillItem(i);
                return true;
            }
        }
        return false;
    }

    public void refreshItemSpots(Item i)
    {
        Debug.Log("refreshing");
        foreach(Button b in eqSpots)
        {
            ItemSpot IS = b.GetComponent<ItemSpot>();
            if (IS.item == i)
            {
                IS.GetComponent<RawImage>().texture = basic;
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(selectedSpot!=null)
            {
                selectedSpot.item.ThrowOut(GameObject.FindObjectOfType<PlayerMovement>().transform.position);
                
                refreshItemSpots(selectedSpot.item);
                selectedSpot.item = null;
                selectedSpot = null;
            }
        }
    }
}
