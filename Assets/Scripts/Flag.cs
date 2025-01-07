using Navegacion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField] private int numEnemigos;
    [SerializeField] private int enemigosElim;
    
    private UIController uiController;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        numEnemigos = GameObject.FindGameObjectsWithTag("Enemy").Length - 1;
    }

    private void EndLevelFlag()
    {
        animator.SetTrigger("End");
    }

    public void EliminatedEnemy()
    {
        enemigosElim += 1;

        if (enemigosElim == numEnemigos)
        {
            EndLevelFlag();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && enemigosElim == numEnemigos)
        {
            uiController.loadScene("Level2");
        }
    }

}
