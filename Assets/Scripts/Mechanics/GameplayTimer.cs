using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanics
{
    public class GameplayTimer : MonoBehaviour
    {
        public TextMeshProUGUI timerText;
        public Slider timerSlider;
        [Range(0, 10)]
        public int minutes;
        public bool startCountDown;

        private float totalTime;
        private float currentTime;

        private const string TIMER_KEY = "Gameplay Timer";

        private void Start()
        {
            totalTime = minutes * 60;

            if (timerSlider != null)
            {
                timerSlider.maxValue = minutes * 60;
                timerSlider.value = timerSlider.maxValue;

                currentTime = PlayerPrefs.GetFloat(TIMER_KEY, timerSlider.value);
            }
            else
            {
                currentTime = PlayerPrefs.GetFloat(TIMER_KEY, totalTime);
            }
        }

        private void Update()
        {
            if (startCountDown)
            {
                currentTime -= Time.deltaTime;

                if (timerSlider != null) timerSlider.value = currentTime;

                UpdateTimerDisplay(currentTime);

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

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
            {
                Reset();
            }
        }

        public float GetCurrentTime()
        {
            return currentTime;
        }

        private void UpdateTimerDisplay(float timeElapsed)
        {
            if (timerText != null)
            {
                timerText.text = FormatTime(timeElapsed);
            }
        }

        private string FormatTime(float timeElapsed)
        {
            int minutes = (int)(timeElapsed % 3600 / 60f);
            int seconds = (int)(timeElapsed % 60f);

            return string.Format("{0:0}:{1:00}", minutes, seconds);
        }

        private void OnDestroy()
        {
            // Save the current timer value when the object is destroyed (e.g., when changing scenes)
            PlayerPrefs.SetFloat(TIMER_KEY, currentTime);
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

        private void Reset()
        {
            PlayerPrefs.DeleteKey(TIMER_KEY);
            timerSlider.value = timerSlider.maxValue;
            currentTime = timerSlider.value;
        }
    }
}