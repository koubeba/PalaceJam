using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intDoor : Interactive {

    bool isOpen = true;
    [SerializeField]
    Animator anim;
    float time=0;

    void Update()
    {
        time += Time.deltaTime;
    }

    public override void OnClicked()
    {
        if (time > 1f)
        {
            if (isOpen) anim.Play("Open"); else anim.Play("Close");

            isOpen = (isOpen == true) ? false : true;

            time = 0;
        }
    }
}
