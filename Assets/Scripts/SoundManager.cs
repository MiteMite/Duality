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

    private AudioSource IngredientAudio = new AudioSource();

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
        player = GameObject.Find("Player");
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
        //PlaySound("theme");
    }

    private void PlaySound(string SoundName)
    {
        for (int i = 0; i < SoundClipsArray.Length; i++)
        {
            if (SoundClipsArray[i].name == SoundName)
            {
                AudioSource myAudioClip = GetComponent<AudioSource>();
                myAudioClip.clip = SoundClipsArray[i].clip;
                myAudioClip.Play();
            }
        }

    }

    private void Update()
    {
        /*
         * character sounds
         * really hacky but it works for now
         */
        if (player != null)
        {
            if (player.GetComponent<PlayerController>().GetJumpState() == false&&Input.GetKey(KeyCode.Space))
            {
                PlaySound("jump");
            }
            if (player.GetComponent<PlayerController>().GetLandingState() == true)
            {
                PlaySound("land");
            }
            
            if (player.GetComponent<PlayerController>().GetWalkingState() == true &&Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))
            {
                if (playOnce)
                {
                    PlaySound("step");
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
            Debug.Log("Could not find player in scene");
        }

        /*
        * ingredients audio
        */

    }
}
