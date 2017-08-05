using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;

public enum gameState { Menu, InGame_Idle, InGame_Cutscene, Pause, EndGame_Fail, EndGame_Succ};

public class GameManager : MonoBehaviour {
    static GameManager instance;
    [SerializeField]
    gameState initialGameState; //The game state that is initially set.
    [SerializeField]
    gameState state; //Current game state.
    [SerializeField]
    Canvas[] UIElements;

    //CUTSCENES
    [SerializeField]
    Cutscene activeCutscene;
    [SerializeField] string firstCutscene;
    [SerializeField]
    RawImage cutsceneThumbnail;
    [SerializeField]
    Text cutsceneText;

    public gameState currentState() { return state;  }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this);

        ChangeGameState(initialGameState);
        setActiveCutscene(firstCutscene);
    }

    private void Start()
    {
        beginCutscene();
    }

    private void Update()
    {
        if (state == gameState.InGame_Idle && Input.GetKey(KeyCode.Escape)) Pause();
    }

    public void ChangeGameState(gameState newState)
    {
        state = newState;
        DisableAllExcept(state.ToString());
        switch(state)
        {
            case gameState.Menu:
            case gameState.EndGame_Fail:
            case gameState.EndGame_Succ:
                GameObject.FindObjectOfType<EQ>().GetComponentInChildren<Canvas>().enabled = false;
                Time.timeScale = 0f;
                break;
            case gameState.Pause:
                GameObject.FindObjectOfType<EQ>().GetComponentInChildren<Canvas>().enabled = false;
                Time.timeScale = 0.1f;
                break;
            case gameState.InGame_Cutscene:
                GameObject.FindObjectOfType<EQ>().GetComponentInChildren<Canvas>().enabled = false;
                Time.timeScale = 1f;
                break;
            case gameState.InGame_Idle:
                GameObject.FindObjectOfType<EQ>().GetComponentInChildren<Canvas>().enabled = true;
                Time.timeScale = 1f;
                break;
            default:
                Time.timeScale = 1f;
                break;
        }
    }

    private void DisableAllExcept(string name)
    {
        foreach (Canvas c in UIElements)
        {
            if (c.name == name) c.gameObject.SetActive(true);
            else c.gameObject.SetActive(false);
        }
    }

    private void DisableAllExcept(string[] names)
    {
        foreach (Canvas c in UIElements)
        {
            if (IsNameInArray(c.name, names)) c.gameObject.SetActive(true);
            else c.gameObject.SetActive(false);
        }
    }

    private bool IsNameInArray(string name, string[] array)
    {
        foreach (string s in array)
        {
            if (s == name) return true;
        }
        return false;
    }

    //BUTTON FUNCTIONS

    public void LoadMenu()
    {
        ChangeGameState(gameState.Menu);
        SceneManager.LoadScene(0);
    }
    public void LoadLevel(int index)
    {
        ChangeGameState(gameState.InGame_Idle);
        SceneManager.LoadScene(index);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    //UWAGA : można pauzować tylko IDLE
    public void Pause()
    {
        if (state == gameState.InGame_Idle) ChangeGameState(gameState.Pause);
    }
    public void UnPause()
    {
        ChangeGameState(gameState.InGame_Idle);
    }
    
    //CUTSCENES
    public void setActiveCutscene(string name)
    {
        activeCutscene = GameObject.FindObjectsOfType<Cutscene>().Where(n => n.name == name).FirstOrDefault();
    }
    public void beginCutscene()
    {
        if (activeCutscene!=null)
        {
            ChangeGameState(gameState.InGame_Cutscene);
            cutsceneThumbnail.texture = activeCutscene.speakerThumb;
            StartCoroutine(updateCutsceneText());                
        }
        
    }

    public IEnumerator updateCutsceneText()
    {
        foreach (string s in activeCutscene.Text)
        {
            cutsceneText.text = s;
            while (true)
            {
                yield return new WaitForSeconds(0.000001f);
                if (Input.GetKeyDown(KeyCode.Space)) break;
            }
        }
        bool isDialogue = activeCutscene.isDialogue;
        activeCutscene.eventous.Invoke();
        activeCutscene = activeCutscene.next;
        if (activeCutscene == null || !isDialogue) ChangeGameState(gameState.InGame_Idle);
        else beginCutscene();
    }

}
