using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class RunningCocroach : MonoBehaviour {
    [SerializeField]
    float speed;
    Vector3 startingPoint;
    Vector3 dir;
    Stopwatch watch = Stopwatch.StartNew();
    
	void Start () {
        startingPoint = this.transform.position;
        System.Random rand = new System.Random();
        dir = new Vector3((float)rand.NextDouble(), 0f, (float)rand.NextDouble());
        dir = dir.normalized;
    }
	
	void Update () {
		if (watch.Elapsed.Seconds > 3f)
        {
            watch.Reset();
            watch.Start();
            if (Vector3.Distance(this.transform.position, startingPoint) < 5f) dir = (startingPoint - this.transform.position).normalized;
            else
            {
                System.Random rand = new System.Random();
                dir = new Vector3((float)rand.NextDouble(), 0f, (float)rand.NextDouble());
                dir = dir.normalized;
            }
        }
        this.transform.position += dir * speed * Time.deltaTime;
        this.transform.LookAt(dir);
	}
}
