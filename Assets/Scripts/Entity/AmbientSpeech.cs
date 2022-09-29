using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSpeech : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    public float timer = 0;
    private KillableEntity entity;

    public bool dead, didDeathSequence = false;

    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public AudioClip deathSFX;
    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        entity = GetComponent<KillableEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deathSFX && entity && entity.dead) dead = true;

        if (dead)
        {
            if (!didDeathSequence)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(deathSFX);
                didDeathSequence = true;
            }
            return;
        }
        if (audioSource.isPlaying)
        {
            timer = 0;
            return;
        }

        timer += Time.deltaTime;
        if (timer > 2.5f)
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length-1)];
            audioSource.Play();
        }
    }
}
