using Navegacion;
using Navegacion.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    [SerializeField] private int numEnemigos;
    //[SerializeField] private int enemigosElim;
    
    public UIController uiController;
    private Animator animator;

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        numEnemigos = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    private void EndLevelFlag()
    {
        animator.SetTrigger("End");
    }

    public void EliminatedEnemy()
    {
        numEnemigos -= 1;

        if (numEnemigos == 0)
        {
            EndLevelFlag();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Level")
        {
            if (collision.tag == "Player" && (numEnemigos == 0) && uiController != null)
            {
                Debug.Log("Tocando la bandera");
                uiController.setState(new NextLevel(uiController));
            }
        }
        else if (currentScene.name == "Level2")
        {
            if (collision.tag == "Player" && (numEnemigos == 0) && uiController != null)
            {
                Debug.Log("Tocando la bandera");
                uiController.setState(new EndGame(uiController));
            }
        }
        
    }

}
