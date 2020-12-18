// Barry Day, 11-23-20
// Code by: https://www.youtube.com/watch?v=6OT43pvUyfY
// Stores sound info

using UnityEngine;

[System.Serializable]
public class Sound
{

    const float VOLUME_MIN = 0f;
    const float VOLUME_MAX = 1f;
    const float PITCH_MIN = 0.1f;
    const float PITCH_MAX = 3f;

    public string name;

    public AudioClip clip;

    [Range(VOLUME_MIN, VOLUME_MAX)]
    public float volume;

    [Range(PITCH_MIN, PITCH_MAX)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

}
