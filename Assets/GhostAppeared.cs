using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAppeared : MonoBehaviour {
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            gameManager.beginCutscene();
        }
    }
    public void destroyTheGhost()
    {
        StartCoroutine(FadeOut(this.transform.position));
    }

    public IEnumerator FadeOut(Vector3 StartingPosition)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            this.transform.position += Vector3.forward * Time.fixedDeltaTime * 1f;
            if (Vector3.Distance(this.transform.position, StartingPosition) > 5f) Destroy(this.gameObject);
        }
    }
}
