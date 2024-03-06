using UnityEngine;
using UnityEngine.UI;

namespace Mechanics
{
    public class GameplayTimer : MonoBehaviour
    {
        public Slider timerSlider;
        [Range(0, 10)]
        public int minutes = 5;
        public bool startCountDown = false;

        private float totalTime;
        private float currentTime;

        private void Start()
        {
            timerSlider.maxValue = minutes * 60;
            timerSlider.value = timerSlider.maxValue;

            totalTime = minutes * 60;
            currentTime = timerSlider.maxValue;
        }

        private void Update()
        {

            if (startCountDown)
            {
                currentTime -= Time.deltaTime;

                timerSlider.value = currentTime;

                if (currentTime <= 0)
                {
                    startCountDown = false;
                }
            }
            else
            {
                // Game Over, schedule an event.
                // transition to a game over screen
            }
        }

        private void OnDestroy()
        {
            // Save the current timer value when the object is destroyed (e.g., when changing scenes)
            PlayerPrefs.SetFloat("TimerValue", currentTime);
            PlayerPrefs.Save();
        }

        public void StartCountdown()
        {
            startCountDown = true;
        }

        public void StopCountdown()
        {
            startCountDown = false;
        }
    }
}