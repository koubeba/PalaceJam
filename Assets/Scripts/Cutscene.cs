using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cutscene : MonoBehaviour {
    public Texture speakerThumb;
    public string[] Text;
    public Cutscene next;
    public bool isDialogue;
    public UnityEvent eventous;
}
