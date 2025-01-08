using Navegacion.State;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using IState = Navegacion.IState;

namespace Navegacion
{
    public class UIController : MonoBehaviour, IUI
    {
        //VARIABLES 
        private IState currentState;

        public GameObject pauseInstance;
        public GameObject panel_background;

        public GameObject dieInstance;
        public GameObject nextLevelInstance;
        public GameObject endGameInstance;

        public GameObject panel_integrantes;
        public GameObject panel_assets;

        public Transform canvas;


        //Función para cargar las escenes
        public void LoadScene(string scene)
        {
            Debug.Log("Cargando escena de " + scene);
            SceneManager.LoadScene(scene);
        }

        //Función para devolver el estado actual
        public IState GetState()
        {
            return currentState;
        }

        //Función para actualizar el estado
        public void SetState(IState state)
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
        public void OnStartButtonPressed() //Empezar el juego
        {
            SetState(new Level(this));
        }

        public void OnCreditsButtonPressed() //Ir a la escena de creditos
        {
            SetState(new Credits(this));
        }
        public void OnOptionsButtonPressed() //Ir a la escena de opciones
        {
            SetState(new Options(this));
        }

        public void OnQuitButtonPressed() //Cerrar el juego
        {
            Debug.Log("Saliendo del juego");
            Application.Quit(); //Salir del juego
        }

        //Botones Menu Créditos
        public void OnExitCreditsButtonPressed() //Volver al menu principal desde el menu de creditos
        {
            SetState(new MainMenu(this));
        }

        public void ShowPanelIntegrantes() //Mostrar panel de integrantes y tareas
        {
            panel_assets.SetActive(false); //Ocultarlo
            panel_integrantes.SetActive(true); //Mostrarlo
        }

        public void ShowPanelAssets() //Mostrar panel de assets
        {
            panel_integrantes.SetActive(false); //Ocultarlo
            panel_assets.SetActive(true); //Mostrarlo

        }

        //Botones Menu Pausa Nivel 1
        public void OnPauseButtonPressed() //Pausar el juego y mostrar el menu de pausa
        {
            SetState(new Pause(this));
        }
        public void OnResume1ButtonPressed() //Reanudar el nivel
        {
            pauseInstance.SetActive(false); //Se oculta el menu de pausa
            panel_background.SetActive(false); //Se oculta el panel 
            Time.timeScale = 1f;
        }
        public void OnRestart1ButtonPressed() //Reiniciar el nivel
        {
            SetState(new Level(this));
        }
        public void OnQuitLevelButtonPressed() //Voler al menu principal desde el nivel
        {
            SetState(new MainMenu(this));
        }

        //Boton menu Siguiente nivel
        public void OnStartLevel2ButtonPressed() //Empezar el nivel 2
        {
            SetState(new Level2(this));
        }

        //Botones Menu Pausa Nivel 2
        public void OnResume2ButtonPressed() //Reanudar el nivel
        {
            pauseInstance.SetActive(false); //Se oculta el menu de pausa
            panel_background.SetActive(false); //Se oculta el panel 
            Time.timeScale = 1f;
        }
        public void OnRestart2ButtonPressed() //Reiniciar el nivel
        {
            SetState(new Level2(this));
        }

        //Botones Menu Muerte jugador nivel 1
        public void OnDieRestartButtonPressed() //Reiniciar el nivel
        {
            SetState(new Level(this));
        }
        public void OnDieQuitLevelButtonPressed() //Voler al menu principal desde el nivel
        {
            SetState(new MainMenu(this));
        }

        //Boton Menu Muerte jugador nivel 2
        public void OnDie2RestartButtonPressed() //Reiniciar el nivel
        {
            SetState(new Level2(this));
        }

        //Botones Menu Opciones       
        public void OnExitOptionsButtonPressed() //Volver al menu principal desde el menu de opciones
        {
            SetState(new MainMenu(this));

        }


        //FUNCIONES MENU OPCIONES

    }



}
