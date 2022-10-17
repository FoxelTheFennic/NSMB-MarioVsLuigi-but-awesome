using UnityEngine;

using NSMB.Utils;

public class VolumeWithDistance : MonoBehaviour {

    public float soundRange = 12f;

    public float maxVolume = 1f;

    public Enums.AudioType type = Enums.AudioType.None;
    public Transform soundOrigin;
    public AudioSource[] audioSources;

    public void Update() {

        GameManager inst = GameManager.Instance;
        Vector3 listener = (inst != null && inst.localPlayer) ? inst.localPlayer.transform.position : Camera.main.transform.position;

        float volume = Utils.QuadraticEaseOut(1 - Mathf.Clamp01(Utils.WrappedDistance(listener, soundOrigin.position) / soundRange)) * maxVolume;
        switch(type)
        {
            case Enums.AudioType.Music:
                volume *= Settings.Instance.VolumeMusic;
                break;
            case Enums.AudioType.SFX:
                volume *= Settings.Instance.VolumeSFX;
                break;
        }

        foreach (AudioSource source in audioSources)
            source.volume = volume;
    }
}