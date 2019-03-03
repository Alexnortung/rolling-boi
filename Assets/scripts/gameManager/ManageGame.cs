using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour
{
    [SerializeField] public bool GameWon { get; set; }

    [SerializeField] public bool IsLevelWon = false;

    [SerializeField] public bool HasGameBegun;

    [SerializeField] public int baddiesKilled = 0;

    public float Timer = 0;


	// Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        GameObject other = GameObject.FindGameObjectWithTag("GameManager");
        //if (other != null && other != gameObject) Destroy(gameObject);
    }

	// Update is called once per frame
	void Update () {
	    if (IsLevelWon)
	    {
            ChangeLevel();
	        IsLevelWon = false;
	    }

	    if (HasGameBegun)
	    {
	        Timer += Time.deltaTime;
        }
	    
	}

    private void ChangeLevel()
    {

        if (SceneManager.GetActiveScene().buildIndex + 1 <= SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void SelectLevel(int levelInt)
    {
        Debug.Log(levelInt > 1);
        if (levelInt > 1)
        {
            HasGameBegun = !HasGameBegun;
        }

        SceneManager.LoadScene(levelInt, LoadSceneMode.Single);

        if (levelInt > 1)
        {
            HasGameBegun = !HasGameBegun;
        }
    }

    public int GetCurrentLevel()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void OpenGraphics()
    {
        
    }

    public void CloseGraphics()
    {
        
    }

    public void killBaddie()
    {
        baddiesKilled++;
    }

    
}
