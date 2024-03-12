using Core;
using Model;
using UnityEngine;

namespace Mechanics
{
    /// <summary>
    /// This class exposes the the game model in the inspector, and ticks the
    /// simulation.
    /// </summary> 
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        //This model field is public and can be therefore be modified in the 
        //inspector.
        //The reference actually comes from the InstanceRegister, and is shared
        //through the simulation and events. Unity will deserialize over this
        //shared reference when the scene loads, allowing the model to be
        //conveniently configured inside the inspector.
        public IsoModel model = Simulation.GetModel<IsoModel>();
        public bool pauseGame;

        void Start()
        {
            Time.timeScale = pauseGame ? 0 : 1;
        }

        void OnEnable()
        {
            Instance = this;
        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        void Update()
        {
            if (Instance == this) Simulation.Tick();
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            pauseGame = true;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
            pauseGame = false;
        }
    }
}