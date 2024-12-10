using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Navegacion.State
{
    public class Pause : Navegacion.AMenuState
    {
        private UIController uiController;
        private GameObject pausePrefab;
        private GameObject pauseInstance;
        private bool isPaused = false;

        public Pause(UIController uiControllerl, GameObject pausePrefab)
        {
            this.uiController = uiController;
            this.pausePrefab = pausePrefab;
        }

        public override void Enter()
        {
            Debug.Log("Entrando al menu de Pausa");
            uiController.loadScene("Pause");
            Time.timeScale = 0f; //Se detiene el juego
            ShowPause();
        }

        public override void Exit()
        {
            Debug.Log("Saliendo del Nivel");
            Time.timeScale = 1f; //Se reanuda el juego
            HidePause();
        }

        public override void FixedUpdate()
        {
<<<<<<< Updated upstream
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                uiController.setState()
            }
=======
            
>>>>>>> Stashed changes
        }
    }
}

