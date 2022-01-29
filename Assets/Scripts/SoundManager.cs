/*
 * Author: Simon Fortier Drouin
 * email: simon_fortier_drouin@live.ca
 * 
 * Description: This program manages audio and soundtrack layering
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;


[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{

    [System.Serializable]
    public struct SoundClips{
        public string name;
        public AudioClip clip;
    }
    public SoundClips[] SoundClipsArray;

    private GameObject player;

    private bool playOnce = true;

    public float musicVolume = 0.1f;
    public float ingredientsVolume = 1.0f;

    private AudioSource IngredientAudio;
    private AudioSource GrenouilleAudio;
    private AudioSource muzikAudio;
    private AudioSource scieAudio;
    private AudioSource waterAudio;
    private AudioSource llamaAudio;
    private AudioSource bumperAudio;
    private AudioSource trampolineAudio;

    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SoundManager>();
            }
            return _instance;
        }
    }
    void Awake()
    {
        trampolineAudio = gameObject.AddComponent<AudioSource>();
        bumperAudio = gameObject.AddComponent<AudioSource>();
        llamaAudio = gameObject.AddComponent<AudioSource>();
        waterAudio = gameObject.AddComponent<AudioSource>();
        scieAudio = gameObject.AddComponent<AudioSource>();
        IngredientAudio = gameObject.AddComponent<AudioSource>();
        GrenouilleAudio = gameObject.AddComponent<AudioSource>();
        muzikAudio = gameObject.AddComponent<AudioSource>();
        muzikAudio.volume = musicVolume;
        muzikAudio.loop = true;
        trampolineAudio.volume = ingredientsVolume;
        bumperAudio.volume = ingredientsVolume;
        llamaAudio.volume = ingredientsVolume;
        waterAudio.volume = ingredientsVolume;
        scieAudio.volume = ingredientsVolume;
        GrenouilleAudio.volume = ingredientsVolume;
        player = GameObject.FindGameObjectWithTag("Player");
        DontDestroyOnLoad(gameObject);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        PlaySound("theme", muzikAudio);
        //PlaySound("theme");
    }
    private void PlaySound(string SoundName,AudioSource source)
    {
        for (int i = 0; i < SoundClipsArray.Length; i++)
        {
            if (SoundClipsArray[i].name == SoundName)
            {
                source.clip = SoundClipsArray[i].clip;
                source.Play();
            }
        }
    }
    private void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        /*
         * character sounds
         * really hacky but it works for now
         */
        if (player != null)
        {
            if (player.GetComponent<PlayerController>().GetJumpState() == false && Input.GetKey(KeyCode.Space))
            {
                PlaySound("jump", this.GetComponent<AudioSource>());
            }
            if (player.GetComponent<PlayerController>().GetLandingState() == true)
            {
                PlaySound("land", this.GetComponent<AudioSource>());
            }

            if (player.GetComponent<PlayerController>().GetWalkingState() == true && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (playOnce)
                {
                    PlaySound("step", this.GetComponent<AudioSource>());
                    playOnce = false;
                }
                this.GetComponent<AudioSource>().loop = true;
            }
            else
            {
                this.GetComponent<AudioSource>().loop = false;
                playOnce = true;
            }
        }
        else
        {
            //Debug.Log("Could not find player in scene");
        }

        /*
        * ingredients audio
        */
        if (GameObject.Find("LanceFlamme"))
        {
            if (GameObject.Find("LanceFlamme").GetComponent<LanceFlamme>().GetFlameState())
            {
                if (IngredientAudio.isPlaying == false)
                {
                    PlaySound("flamethrower", IngredientAudio);
                }
            }
        }
        if (GameObject.Find("Grenouille")) {
            if (GameObject.Find("Grenouille").GetComponent<Grenouille>().GetTongueShooting())
            {
                if (GrenouilleAudio.isPlaying == false)
                {
                    PlaySound("FrogShoot", GrenouilleAudio);
                }
            }
            if (GameObject.Find("Grenouille").GetComponent<Grenouille>().GetTongueHit())
            {
                if (GrenouilleAudio.isPlaying == false)
                {
                    PlaySound("FrogHit", GrenouilleAudio);
                }
            }
        }
        if (GameObject.Find("Scie"))
        {
            if (scieAudio.isPlaying == false)
            {
                scieAudio.loop = true;
                PlaySound("Scie", scieAudio);
            }
        }
        if (GameObject.Find("Waterfall"))
        {
            if (waterAudio.isPlaying == false)
            {
                waterAudio.loop = true;
                PlaySound("water", waterAudio);
            }
        }
        if (GameObject.Find("Lama-gun"))
        {
            if(llamaAudio.isPlaying == false)
            {
                PlaySound("lama", llamaAudio);
            }
        }
        if (GameObject.Find("Bumper"))
        {
            if (GameObject.Find("Bumper").GetComponent<Bumper>().GetBumperState() == true)
            {
                if (bumperAudio.isPlaying == false)
                {
                    PlaySound("bumper", bumperAudio);
                }
            }
        }
        if (GameObject.Find("Trampoline"))
        {
            if (GameObject.Find("Trampoline").GetComponent<Trampoline>().GetTrampolineStatus() == true)
            {
                if (trampolineAudio.isPlaying == false)
                {
                    PlaySound("bumper", trampolineAudio);
                }
            }
        }
    }
}
