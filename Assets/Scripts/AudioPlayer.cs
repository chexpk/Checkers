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
        board.checkerMoveEvent.AddListener(PlaySound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlaySound(BoardPosition fromBoardPosition, BoardPosition toBoardPosition)
    {
        audioPlayer.clip = sound;
        audioPlayer.Play();
    }
}
