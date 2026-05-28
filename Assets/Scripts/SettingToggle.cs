using UnityEngine;
using UnityEngine.UI;

public class SettingToggle : MonoBehaviour
{
    [Header("Music")]
    public GameObject musicOn;
    public GameObject musicOff;

    [Header("SFX")]
    public GameObject sfxOn;
    public GameObject sfxOff;

    [Header("Vibration")]
    public GameObject vibrationOn;
    public GameObject vibrationOff;

    void OnEnable()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (AudioManager.Instance == null) return;

        musicOn.SetActive(AudioManager.Instance.IsMusicEnabled());
        musicOff.SetActive(!AudioManager.Instance.IsMusicEnabled());

        sfxOn.SetActive(AudioManager.Instance.IsSFXEnabled());
        sfxOff.SetActive(!AudioManager.Instance.IsSFXEnabled());

        vibrationOn.SetActive(AudioManager.Instance.IsVibrationEnabled());
        vibrationOff.SetActive(!AudioManager.Instance.IsVibrationEnabled());
    }

    public void ToggleMusic()
    {
        AudioManager.Instance?.SetMusic(!AudioManager.Instance.IsMusicEnabled());
        UpdateUI();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance?.SetSFX(!AudioManager.Instance.IsSFXEnabled());
        UpdateUI();
    }

    public void ToggleVibration()
    {
        AudioManager.Instance?.SetVibration(!AudioManager.Instance.IsVibrationEnabled());
        UpdateUI();
    }
}