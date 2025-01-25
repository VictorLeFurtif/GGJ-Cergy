using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
   public static SoundManager instance;
   [SerializeField] private AudioSource _audioSource;
  public AudioClip running;
   public void PlaySound(AudioClip clip, Vector3 pos)
   {
      PlayClipAtPoint(clip, pos, 1, Random.Range(0.8f, 1.15f), 60);
   }
   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
      }
      else
      {
         Destroy(gameObject);
      }
   }
   public static void PlayClipAtPoint(AudioClip clip, Vector3 position, [UnityEngine.Internal.DefaultValue("1.0F")] float volume, float pitch, float range)
   {
      GameObject gameObject = new GameObject("One shot audio");
      gameObject.transform.position = position;
      AudioSource audioSource = (AudioSource) gameObject.AddComponent(typeof (AudioSource));
      audioSource.clip = clip;
      audioSource.spatialBlend = 1f;
      audioSource.volume = volume;
      audioSource.pitch = pitch;
      audioSource.rolloffMode = AudioRolloffMode.Linear;
      audioSource.maxDistance = range;
      audioSource.Play();
      Destroy(gameObject, clip.length * ((double) Time.timeScale < 0.009999999776482582 ? 0.01f : Time.timeScale));
   }
   
}
