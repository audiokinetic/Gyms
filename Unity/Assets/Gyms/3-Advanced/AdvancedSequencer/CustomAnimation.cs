using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnimation : OnOffManager
{
    public float animationDuration = 4f;
    public AkAmbient ambientSound;

    private bool _doAnimation = false;
    private Transform _defaultTransform;
    private float _startZ = 0f;
    private float _endZ = 0f;
    private float _elapsedTime = 0f;
    private bool _isForward = true;

    void Start()
    {

    }

    void Update()
    {
        if (_doAnimation)
        {
            _elapsedTime += Time.deltaTime;

            float progress = Mathf.Clamp01(_elapsedTime / animationDuration);
            
            if (_isForward)
            {
                GetComponent<AdvancedSequencer_RTPCSequence>().RtpcValue = 1-progress;
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(_startZ, _endZ, progress));
            }
            else
            {
                GetComponent<AdvancedSequencer_RTPCSequence>().RtpcValue = progress;
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(_endZ, _startZ, progress));
            }

            // Check if the animation is complete
            if (progress >= 1.0f)
            {
                _isForward = !_isForward;
                _elapsedTime = 0f;
            }
        }
    }
    
    public override void OffAction()
    {
        _doAnimation = false;
        AkSoundEngine.ExecuteActionOnEvent(ambientSound.data.Id, AkActionOnEventType.AkActionOnEventType_Stop, gameObject);
    }

    public override void OnAction()
    {
        if (!_defaultTransform)
        {
            _defaultTransform = GetComponent<Transform>();
            _endZ = _defaultTransform.transform.position.z;
            _startZ = _endZ + 10f ;
        }
        _doAnimation = true;
        transform.position = _defaultTransform.position;
        ambientSound.data.Post(ambientSound.gameObject);
    }
}
