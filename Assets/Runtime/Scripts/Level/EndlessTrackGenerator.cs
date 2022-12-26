using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private TrackSegment[] trackPrefabEasy;
    [SerializeField] private TrackSegment[] trackPrefabHard;
    [SerializeField] private TrackSegment[] trackPrefabReward;

    [Header("Track Parameters")]
    [SerializeField] private TrackSegment firstTrack;
    [SerializeField] private int minTracksInFrontOfPlayer;
    [SerializeField] private int initialTrackCount;
    [SerializeField] private float minDistanceToConsiderInsideTrack;

    [Header("Difficulty Parameters")]
    [Range(0,1)]
    [SerializeField] private float hardTrackChance;    
    [SerializeField] private float minTracksToReward;
    [Range(0, 1)]
    [SerializeField] private float minTrackCountRewardChance;
    [Range(0, 1)]
    [SerializeField] private float maxTrackCountRewardChance;
    private float trackCountToReward = 0;


    List<TrackSegment> currentSegments = new List<TrackSegment>();

    void Start()
    {
        
        SpawnTrackSegment(firstTrack, null);

        SpawnTracks(initialTrackCount);
    }

    void Update()
    {
        UpdateTracks();

    }

    private void UpdateTracks()
    {
        int playerTrackIndex = FindTrackIndexWithPlayer();

        if (playerTrackIndex < 0)
        {
            //TODO: throw error
            return;
        }

        SpawnTracksInFrontOfPlayer(playerTrackIndex);

        DespawnTracksBehindPlayer(playerTrackIndex);
    }

    private void DespawnTracksBehindPlayer(int playerTrackIndex)
    {
        for (int i = 0; i < playerTrackIndex; i++)
        {
            TrackSegment track = currentSegments[i];
            Destroy(track.gameObject);
        }

        currentSegments.RemoveRange(0, playerTrackIndex);
    }

    private void SpawnTracksInFrontOfPlayer(int playerTrackIndex)
    {
        int tracksInFrontOfPlayer = currentSegments.Count - (playerTrackIndex + 1);
        if (tracksInFrontOfPlayer < minTracksInFrontOfPlayer)
        {
            SpawnTracks(minTracksInFrontOfPlayer - tracksInFrontOfPlayer);
        }
    }

    private int FindTrackIndexWithPlayer()
    {
        int playerTrackIndex = -1;
        for (int i = 0; i < currentSegments.Count; i++)
        {
            TrackSegment track = currentSegments[i];
            if (player.transform.position.z >= track.Start.position.z + minDistanceToConsiderInsideTrack
                && player.transform.position.z <= track.End.position.z)
            {
                playerTrackIndex = i;

                break;
            }
        }

        return playerTrackIndex;
    }

    void SpawnTracks(int trackCount)
    {
        TrackSegment previousTrack = currentSegments.Count > 0
            ? currentSegments[currentSegments.Count - 1] 
            : null;


        for (int i = 0; i < trackCount; i++)
        {

            TrackSegment track;
            if (trackCountToReward < minTracksToReward)
            {
                track = GetRandomTrack();
                trackCountToReward += Random.Range(minTrackCountRewardChance, maxTrackCountRewardChance);
            }
            else
            {
                track = trackPrefabReward[Random.Range(0, trackPrefabReward.Length)];
                trackCountToReward = 0;
            }

            previousTrack = SpawnTrackSegment(track, previousTrack);
        }
    }


    TrackSegment GetRandomTrack()
    {
        TrackSegment[] trackList = Random.value <= hardTrackChance
            ? trackPrefabHard 
            : trackPrefabEasy;
        return trackList[Random.Range(0,trackList.Length)];
    }

    TrackSegment SpawnTrackSegment(TrackSegment track, TrackSegment previousTrack)
    {

        TrackSegment trackInstance = Instantiate(track, transform);

        if (previousTrack != null)
        {
            trackInstance.transform.position = previousTrack.End.position
                + (trackInstance.transform.position - trackInstance.Start.position);

        }
        else
        {

            trackInstance.transform.localPosition = Vector3.zero;

        }

        currentSegments.Add(trackInstance);
        return trackInstance;
    }
}
