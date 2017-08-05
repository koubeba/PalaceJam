using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveExample : Interactive {
    public ParticleSystem sys;

    void Start()
    {
        sys = GetComponentInChildren<ParticleSystem>();
    }

    public override void OnClicked()
    {
        sys.enableEmission = (sys.enableEmission == true) ? false : true;
    }
}
