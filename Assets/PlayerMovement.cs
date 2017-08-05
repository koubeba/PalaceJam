using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] Vector3 moveDestination;
    [SerializeField] public bool isMoving = false;
    [SerializeField] float speed = 10f;
    [SerializeField] float stopRange = 1f;
    [SerializeField] Animator animator;
    GameManager gameManager;

    Item itemToSelect;
    

	void Start () {
        gameManager = GameObject.FindObjectOfType<GameManager>();
	}

    void Update()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("isMoving", isMoving);
        if (isMoving)
        {
            Vector3 dir = moveDestination - this.transform.position;
            this.transform.position += dir.normalized * speed * Time.deltaTime;
            this.transform.LookAt(new Vector3(moveDestination.x, this.transform.position.y, moveDestination.z));
            if (dir.sqrMagnitude < stopRange)
            {
                isMoving = false;
                if(itemToSelect!=null)
                {
                    if(GameObject.FindObjectOfType<EQ>().PickUpItem(itemToSelect)==true)
                    {
                        // Destroy(itemToSelect);
                        itemToSelect.LeaveJustItem();
                    }
                    itemToSelect = null;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && gameManager.currentState()==gameState.InGame_Idle)
            {
                Ray ray;
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
                {
                    GameObject hitObject = hitInfo.transform.root.gameObject;
                    if (hitObject.tag == "floor" ||(hitObject.tag == "hidable" && hitObject.GetComponent<MeshRenderer>().enabled == false))
                    {
                        moveDestination = new Vector3(hitInfo.point.x, this.transform.position.y, hitInfo.point.z);
                        isMoving = true;
                    }
                    if (hitObject.tag == "item")
                    {
                        moveDestination = new Vector3(hitInfo.point.x, this.transform.position.y, hitInfo.point.z);
                        isMoving = true;
                        itemToSelect = hitObject.GetComponent<Item>();
                    }

                    if(hitObject.tag=="interactive")
                    {
                        hitObject.GetComponent<Interactive>().OnClicked();
                    }
                }
            }
        }
    }
    public void Die()
    {
        Debug.Log("Death...");
        gameManager.ChangeGameState(gameState.EndGame_Fail);
    }
}
