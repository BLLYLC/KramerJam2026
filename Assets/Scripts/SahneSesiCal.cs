using UnityEngine;

public class SahneSesiCal : MonoBehaviour
{
    public AudioClip acilisSesi;
    private AudioSource sesKaynagi;

    private void Start()
    {
        sesKaynagi = gameObject.AddComponent<AudioSource>();
        
        if (acilisSesi != null)
        {
            sesKaynagi.PlayOneShot(acilisSesi);
        }
    }
}