using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    [SerializeField] SoundData[] soundDatas;

    // instance for singleton control
    public static AudioManager instance;

    // volume slider value
    [HideInInspector] public float sliderVal;

    // default music
    [SerializeField] string currentMusic = "music";

    [SerializeField] [Range(0.00001f,1f)]
    float defaultVolume = .25f;

    float timer;

    // Editor-viewable sound data
    [System.Serializable]
    public class SoundData
    {
        public string soundName; // name given in editor, used in PlaySound to play that sound
        public AudioSource soundSource;
        public float minPitch; // min and max pitch used for randomizing between the two values. to only have 1 pitch, set min and max to same values.
        public float maxPitch;
        public float minVol; // same as pitch ^
        public float maxVol;
        public float blockTime; // time in which the sound can't be played again
        [HideInInspector] public float lastTimePlayed; // time on the timer when the sound was last played
    }

    // internal dictionary used for sound lookup
    Dictionary<string, SoundData> sounds = new Dictionary<string, SoundData>();

    private void Awake()
    {
        // singleton control
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        timer = 0f;

        // set slider to default volume
        sliderVal = defaultVolume;

        foreach (SoundData s in soundDatas) {
            sounds.Add(s.soundName, s);
        }

        PlaySound(currentMusic);
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void PlaySound(string soundName)
    {
        if (sounds.ContainsKey(soundName))
        {
            // check that block time has been reached
            if (sounds[soundName].blockTime <= timer - sounds[soundName].lastTimePlayed) {
                // if sound is a music, put "music" in the name and it will stop the previous music when played
                if (soundName.Contains("music"))
                {
                    sounds[currentMusic].soundSource.Stop();
                    currentMusic = soundName;
                }
                sounds[soundName].soundSource.pitch = Random.Range(sounds[soundName].minPitch, sounds[soundName].maxPitch);
                sounds[soundName].soundSource.volume = Random.Range(sounds[soundName].minVol, sounds[soundName].maxVol);
                sounds[soundName].soundSource.Play();
                sounds[soundName].lastTimePlayed = timer;
            }
        }
        else
        {
            Debug.LogError("Sound requested does not exist");
        }
    }

    public void StopSound(string soundName) {
        if (sounds.ContainsKey(soundName))
        {
            sounds[soundName].soundSource.Stop();
        }
        else
        {
            Debug.LogError("Sound requested does not exist");
        }
    }

    public void SetPitchRange(string soundName, float newMinPitch, float newMaxPitch) {
        if (sounds.ContainsKey(soundName))
        {
            sounds[soundName].minPitch = newMinPitch;
            sounds[soundName].maxPitch = newMaxPitch;
        }
        else
        {
            Debug.LogError("Sound requested does not exist");
        }
    }
}
