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

        public void loadScene(string scene)
        {
            Debug.Log("Cargando escena de " + scene);
            SceneManager.LoadScene(scene);
        }

        public IState getState()
        {
            return currentState;
        }

        public void setState(IState state)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = state;
            currentState.Enter();
        }

        

        //Botones Main Menu
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
            Application.Quit();
        }

        //Botones Créditos
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

        //Botones Pausa
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
