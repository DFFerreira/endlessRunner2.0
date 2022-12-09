using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private TrackSegment[] trackPrefab;

    [SerializeField] private TrackSegment firstTrack;
    [SerializeField] private int initialTrackCount;

    void Start()
    {
        TrackSegment initialTrack = Instantiate(firstTrack, transform);
        TrackSegment previousTrack = initialTrack;
        
        for (int i = 0; i < initialTrackCount; i++)
        {
            int index = Random.Range(0, trackPrefab.Length);
            TrackSegment track = trackPrefab[index];
            TrackSegment trackInstance = 
                Instantiate(track, transform);
            trackInstance.transform.position = previousTrack.End.position
                + (trackInstance.transform.position - trackInstance.Start.position);

            previousTrack = trackInstance;
        }
    }
}
