using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackedImageManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI testText;
    [SerializeField] private GameObject trackedImagesContainer;
    [SerializeField] private GameObject[] imagesPrefabs;

    private ARTrackedImageManager _arTrackedImageManager;
    private Transform _lastTrackedImage = null;
    private readonly Dictionary<String, GameObject> _imagePrefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();

        // * Create dictionary of prefabs to instance on image detection
        foreach (var prefab in imagesPrefabs)
        {
            var instance = Instantiate(prefab);
            _imagePrefabs.Add(prefab.name, instance);
            instance.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }


    private void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        Transform trackedImageT = trackedImage.transform;
        String trackedImageName = trackedImage.referenceImage.name;

        String newText =
            $"trackedImageName: {trackedImageName}\n" +
            $"trackingState: {trackedImage.trackingState}\n" +
            $"position: {trackedImageT.position.ToString()}\n" +
            $"Reference size: {trackedImage.referenceImage.size * 100f} cm\n" +
            $"Detected size: {trackedImage.size * 100f} cm\n";

        testText.text = newText;

        if (_lastTrackedImage)
        {
            // newText = newText +
            //           $"\n lastTrackedImage name: {_lastTrackedImage.name}";
        }


        if (trackedImageName != _lastTrackedImage.name)
        {
            trackedImage.transform.localScale = new Vector3(trackedImage.size.x, 1f, trackedImage.size.y);

            if (_lastTrackedImage)
            {
                _lastTrackedImage.gameObject.SetActive(false);
            }

            trackedImage.transform.Find(trackedImageName).gameObject.SetActive(true);
            _lastTrackedImage = trackedImageT;

            // newText = newText +
            //           $"\n NEW trackedImage name: {trackedImageT.name}";
        }
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            print(trackedImage.name);
            // trackedImage.transform.localScale = new Vector3(.01f, 0.01f, .01f);
            _imagePrefabs[trackedImage.referenceImage.name].transform.SetParent(trackedImage.transform);
            _imagePrefabs[trackedImage.referenceImage.name].transform.localPosition = Vector3.zero;
            UpdateTrackedImage(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            // UpdateTrackedImage(trackedImage);
            _imagePrefabs[trackedImage.referenceImage.name].SetActive(true);
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            // UpdateTrackedImage(trackedImage);
            _imagePrefabs[trackedImage.referenceImage.name].SetActive(false);
        }
    }

    // [System.Serializable]
    // public struct ImagesPrefabs
    // {
    //     public string id;
    //     public GameObject prefab;
    // }
}