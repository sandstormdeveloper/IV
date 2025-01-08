using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Navegacion.State
{
    public class EndGame : Navegacion.AMenuState
    {
        private UIController uiController;


        public EndGame(UIController uiController)
        {
            this.uiController = uiController;
        }

        public override void Enter()
        {
            Debug.Log("Entrando al menu de muerte del jugador");

            uiController.endGameInstance.SetActive(true); //Se activa el menu de pausa

            Time.timeScale = 0f; //Se detiene el juego

        }

        public override void Exit()
        {
            Debug.Log("Saliendo del menu de muerte del jugador");

            uiController.endGameInstance.SetActive(false); //Se oculta el menu de pausa

            Time.timeScale = 1f; //Se reanuda el juego

        }
    }
}
