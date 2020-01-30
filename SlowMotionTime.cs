using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionTime : MonoBehaviour
{
    public GoalManager goalManager;

    public GameObject _finsishLineCamera;
    public GameObject _mainCamera;

    public AudioSource[] _slowMotionSoundFX;

    private bool _SlowmotionTriggeredOnce;
    private bool _slowMotionOn;
    

    
    IEnumerator SlowMotionEffectCoroutine()
    {
        foreach (AudioSource audioSource in _slowMotionSoundFX)
        {
            audioSource.Play();
        }

        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        yield return new WaitForSeconds(0.9f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }

    // If Triggered Entered By Player It Starts a Coroutine That changes The TimeScale To Slowmotion
    private void OnTriggerEnter(Collider other)
    {
        if (!_SlowmotionTriggeredOnce && other.GetComponent<Player>())
        {
            _SlowmotionTriggeredOnce = true;
            _mainCamera.SetActive(false);
            _finsishLineCamera.SetActive(true);
            StartCoroutine(SlowMotionEffectCoroutine());
        }

        if (!_SlowmotionTriggeredOnce && other.GetComponent<VRMovement>())
        {
            _SlowmotionTriggeredOnce = true;
            StartCoroutine(SlowMotionEffectCoroutine());
        }
    }
   
 private void Start()
    {
        _SlowmotionTriggeredOnce = false;
    }
}
