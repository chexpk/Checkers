using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip sound;
    public AudioSource audioPlayer;
    public Board board;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(board.checkerMoveEvent);
        board.checkerMoveEvent.AddListener(PlaySound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlaySound(int fromX, int fromY, int toX, int toY )
    {
        audioPlayer.clip = sound;
        audioPlayer.Play();
    }
}
