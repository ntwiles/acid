using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using IslandPuzzle.Interaction;
using System;

public class ActivatedTransformer : MonoBehaviour, ITriggerable
{
    // Settings
    [SerializeField] private GameObject activatingGameObject;
    private IActivateable activator;
    [SerializeField] private float duration = 2f;
    [SerializeField] private bool revertAfter = false;
    [SerializeField] private bool useOnlyOnce = false;

    private const float FINISHED_THRESHOLD = 0.1f;

    [Header("End Transform")]
    [SerializeField] Vector3 endPosition;
    [SerializeField] Vector3 endRotation, endScale;

    // State 
    Vector3 startPosition, startRotation, startScale;
    private bool isTransforming;
    private bool isFinished;
    private float timeWaited = 0;

    public event Action<bool> OnTriggered = delegate { };

    void Awake()
    {
        activator = activatingGameObject.GetComponent<IActivateable>();
        if (activator == null) return;

        activator.OnActivated += onActivatorTriggered;
    }

    void Start()
    {
        startPosition = transform.localPosition;
        startRotation = transform.localEulerAngles;
        startScale = transform.localScale;
    }

    public void onActivatorTriggered(bool isOn)
    {
        if (useOnlyOnce && isFinished) return;

        if (!isTransforming)
        {
            isTransforming = true;
            StartCoroutine(handleTransformations());
        }
    }

    private IEnumerator handleTransformations()
    {
        yield return doTransformation();

        // If we need to bounce back, turn it back on.
        if (revertAfter)
        {
            isTransforming = true;
            timeWaited = 0;
        }

        yield return doTransformation(true);

        OnTriggered(true);

        isFinished = true;
    }

    private IEnumerable doTransformation(bool reverse = false)
    {
        Vector3 positionA = reverse ? endPosition : startPosition;
        Vector3 positionB = reverse ? startPosition : endPosition;
        Vector3 rotationA = reverse ? endRotation : startRotation;
        Vector3 rotationB = reverse ? startRotation : endRotation;
        Vector3 scaleA = reverse ? endScale : startScale;
        Vector3 scaleB = reverse ? startScale : endScale;

        while (isTransforming)
        {
            timeWaited += Time.deltaTime;
            float ratio = timeWaited / duration;

            // TODO: Consider converting rotations to quaternions behind the scene
            // and lerping those instead.
            transform.localPosition = Vector3.Lerp(positionA, positionB, ratio);
            transform.localEulerAngles = Vector3.Lerp(rotationA, rotationB, ratio);
            transform.localScale = Vector3.Lerp(scaleA, scaleB, ratio);

            if (timeWaited > duration)
            {
                isTransforming = false;
            }

            yield return null;
        }
    }
}