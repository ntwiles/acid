using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using IslandPuzzle.Interaction;

public class TriggeredTransformer : MonoBehaviour
{
    // Settings
    [SerializeField] private GameObject triggeringGameObject;
    private ITriggerable triggerer;
    [SerializeField] private float duration = 2f;

    [Header("End Transform")]
    [SerializeField] Vector3 endPosition;
    [SerializeField] Vector3 endRotation, endScale;

    // State 
    Vector3 startPosition, startRotation, startScale;
    private bool isTriggered;
    private float timeWaited = 0;

    void Awake()
    {
        triggerer = triggeringGameObject.GetComponent<ITriggerable>();
        if (triggerer == null) return;

        triggerer.OnTriggered += onTriggerFired;
    }

    void Start()
    {
        startPosition = transform.localPosition;
        startRotation = transform.localEulerAngles;
        startScale = transform.localScale;
    }

    void Update()
    {
        if (!isTriggered) return;

        print("moving!");

        // We've already done our transform;
        if (timeWaited > duration) return;

        timeWaited += Time.deltaTime;
        float ratio = timeWaited / duration;

        // TODO: Consider converting rotations to quaternions behind the scene
        // and lerping those instead.
        transform.localPosition = Vector3.Lerp(startPosition, endPosition, ratio);
        transform.localEulerAngles = Vector3.Lerp(startRotation, endRotation, ratio);
        transform.localScale = Vector3.Lerp(startScale, endScale, ratio);
    }

    public void onTriggerFired(bool isOn)
    {
        print("Object triggered.");
        isTriggered = true;
    }
}
