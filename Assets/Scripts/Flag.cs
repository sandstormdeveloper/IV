using Navegacion;
using Navegacion.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        numEnemigos = 4;
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
        Debug.Log("No Tocando la bandera");

        if (collision.tag == "Player" && (numEnemigos == 0)  && uiController != null)
        {
            Debug.Log("Tocando la bandera");
            uiController.setState(new NextLevel(uiController));
        } 
    }

}
