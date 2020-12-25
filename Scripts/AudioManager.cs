using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    Sprite audioOn, audioOff;
    [SerializeField]
    AudioListener gameAudio;

    Image audioImageBtn;

    bool audioPaused = true;

    private void Start()
    {
        //prendo l'immagine dell'audio
        audioImageBtn = GetComponent<Image>();
    }

    //Funzione da invocare sul pulsante audio
    public void AudioButton()
    {
        if (audioPaused)
        {
            audioImageBtn.sprite = audioOn;
            audioPaused = false;
        }
        else
        {
            audioImageBtn.sprite = audioOff;
            audioPaused = true;
        }
        AudioListener.pause = audioPaused;
    }
}
