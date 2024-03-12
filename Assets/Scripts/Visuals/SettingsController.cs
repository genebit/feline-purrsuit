using Core;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace Visuals
{
    public class SettingsController : MonoBehaviour
    {
        public PostProcessVolume volume;

        public Toggle bloomToggle;
        public Toggle vignetteToggle;
        public Slider musicSlider;

        private void Start()
        {
            volume = volume.GetComponent<PostProcessVolume>();
            volume.profile.TryGetSettings(out Bloom bloom);
            volume.profile.TryGetSettings(out Vignette vignette);

            bool bloomSetting = PlayerPrefs.GetInt(SaveKeys.BLOOM) == 1;
            bool vignetteSetting = PlayerPrefs.GetInt(SaveKeys.VIGNETTE) == 1;

            // Set the bloom effect to the saved value
            bloom.active = bloomSetting;

            // Set the vignette effect to the saved value
            vignette.active = vignetteSetting;

            if (bloomToggle != null && vignette != null)
            {
                // Set the toggles to the saved values
                bloomToggle.isOn = bloomSetting;

                // Set the toggles to the saved values
                vignetteToggle.isOn = vignetteSetting;
            }

            musicSlider.value = PlayerPrefs.GetFloat(SaveKeys.BACKGROUND_MUSIC, 100f);
        }

        private void Update()
        {
            // set the volume of the background music to the value of the slider
            AudioListener.volume = musicSlider.value / 100;
            PlayerPrefs.SetFloat(SaveKeys.BACKGROUND_MUSIC, musicSlider.value);
        }

        public void ToggleBloom()
        {
            volume.profile.TryGetSettings(out Bloom bloom);
            bloom.active = bloomToggle.isOn;

            PlayerPrefs.SetInt(SaveKeys.BLOOM, bloom.active ? 1 : 0);
        }

        public void ToggleVignette()
        {
            volume.profile.TryGetSettings(out Vignette vignette);
            vignette.active = vignetteToggle.isOn;

            PlayerPrefs.SetInt(SaveKeys.VIGNETTE, vignette.active ? 1 : 0);
        }
    }
}