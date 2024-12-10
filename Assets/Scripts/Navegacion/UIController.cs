using Navegacion.State;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using IState = Navegacion.IState;

namespace Navegacion
{
    public class UIController : MonoBehaviour
    {
        //VARIABLES 

        private IState currentState;
        private UIController uiController;

        public GameObject pauseInstance;
        public GameObject panel_pause;

        public GameObject panel_integrantes;
        public GameObject panel_assets;

        public Transform canvas;


        void Start()
        {
            uiController = FindObjectOfType<UIController>();
        }

        //Función para cargar las escenes
        public void loadScene(string scene)
        {
            Debug.Log("Cargando escena de " + scene);
            SceneManager.LoadScene(scene);
        }

        //Función para devolver el estado actual
        public IState getState()
        {
            return currentState;
        }

        //Función para actualizar el estado
        public void setState(IState state)
        {
            if (currentState != null)
            {
                currentState.Exit(); //Sale del estado actual
            }

            currentState = state; //Actualiza el estado nuevo
            currentState.Enter(); //Entra en el estado nuevo
        }

        /// BOTONES 

        //Botones Menu Principal
        public void OnStartButtonPressed()
        {
            uiController.setState(new Level(uiController));
        }

        public void OnCreditsButtonPressed()
        {
            uiController.setState(new Credits(uiController));
        }

        public void OnQuitButtonPressed()
        {
            Debug.Log("Saliendo del juego");
            Application.Quit(); //Salir del juego
        }

        //Botones Menu Créditos
        public void OnExitCreditsButtonPressed()
        {
            uiController.setState(new MainMenu(uiController));
        }

        public void ShowPanelIntegrantes()
        {
            panel_assets.SetActive(false); //Ocultarlo
            panel_integrantes.SetActive(true); //Mostrarlo
        }

        public void ShowPanelAssets()
        {
            panel_integrantes.SetActive(false); //Ocultarlo
            panel_assets.SetActive(true); //Mostrarlo

        }

        //Botones Menu Pausa
        public void OnPauseButtonPressed()
        {
            uiController.setState(new Pause(uiController));
        }
        public void OnResumeButtonPressed()
        {
            uiController.setState(new Level(this));
        }
        public void OnRestartButtonPressed()
        {
            uiController.setState(new Level(uiController));
        }
        public void OnQuitLevelButtonPressed()
        {
            uiController.setState(new MainMenu(uiController));
        }

    }

    

}
