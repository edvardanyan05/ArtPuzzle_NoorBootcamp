using UnityEngine;

public class PlayMainMusic : MonoBehaviour
{
    public void PlayMusic()
    {
        AudioManager.Instance?.PlayBackgroundMusic();
    }
}
