using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    public string color = "black";
    public GameObject black;
    public GameObject white;
    public GameObject selected;
    public float jumpHeight = 1.5f;
    private bool isSelected = false;

    Animation anim;

    int x;
    int y;

    void Start()
    {
        RenderColor();
    }

    public void SetBoardPosition(BoardPosition boardPosition)
    {
        this.x = boardPosition.x;
        this.y = boardPosition.y;
    }

    public BoardPosition GetBoardPosition ()
    {
        return new BoardPosition(x, y);
    }

    public bool isBlack()
    {
        return color == "black";
    }

    public bool isWhite()
    {
        return color == "white";
    }

    public void SetColor(string color)
    {
        this.color = color;
        Render();
    }

    public void Highlight()
    {
        isSelected = true;
        Render();
    }

    public void UnHighlight()
    {
        isSelected = false;
        Render();
    }

    public string GetColor()
    {
        return color;
    }

    public void MoveWithAnimatedTo(float toX, float toZ)
    {
        anim = GetComponent<Animation>();
        AnimationClip clip = CreatAnimationOfMove(toX, toZ);
        anim.AddClip(clip, clip.name);
        anim.Play(clip.name);
    }

    AnimationClip CreatAnimationOfMove(float toX, float toZ)
    {
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;
        AddCurvePositionX(toX, clip);
        AddCurvePositionZ(toZ, clip);
        AddCurvePositionY(clip);
        return clip;
    }
// TODO отрефакторить AddAddCurvePosition - сделать меньше повторяющегося
    void AddCurvePositionX(float toX, AnimationClip clip)
    {
        float x = this.transform.position.x;
        AnimationCurve curveX;

        Keyframe[] keysX;
        keysX = new Keyframe[2];
        keysX[0] = new Keyframe(0.0f, x);
        keysX[1] = new Keyframe(1.0f, toX);
        curveX = new AnimationCurve(keysX);
        clip.SetCurve("", typeof(Transform), "localPosition.x", curveX);
    }

    void AddCurvePositionZ(float toZ, AnimationClip clip)
    {
        float z = this.transform.position.z;
        AnimationCurve curveZ;

        Keyframe[] keysZ;
        keysZ = new Keyframe[2];
        keysZ[0] = new Keyframe(0.0f, z);
        keysZ[1] = new Keyframe(1.0f, toZ);
        curveZ = new AnimationCurve(keysZ);
        clip.SetCurve("", typeof(Transform), "localPosition.z", curveZ);
    }

    void AddCurvePositionY(AnimationClip clip)
    {
        AnimationCurve curveY;
        float y = this.transform.position.y;
        Keyframe[] keysY;
        keysY = new Keyframe[3];
        keysY[0] = new Keyframe(0.0f, y);
        keysY[1] = new Keyframe(0.5f, y + jumpHeight);
        keysY[2] = new Keyframe(1.0f, y);
        curveY = new AnimationCurve(keysY);
        clip.SetCurve("", typeof(Transform), "localPosition.y", curveY);
    }

    void CreatCurve()
    {
        AnimationCurve curveY;
        float y = this.transform.position.y;
        Keyframe[] keysY;
        keysY = new Keyframe[3];
        keysY[0] = new Keyframe(0.0f, y);
        keysY[1] = new Keyframe(0.5f, y + jumpHeight);
        keysY[2] = new Keyframe(1.0f, y);
        curveY = new AnimationCurve(keysY);
    }

    void RenderColor()
    {
        black.SetActive(isBlack());
        white.SetActive(isWhite());
        selected.SetActive(false);
    }

    void RenderSelected()
    {
        selected.SetActive(true);
        black.SetActive(false);
        white.SetActive(false);
    }

    void Render()
    {
        if (isSelected)
        {
            RenderSelected();
        }
        else
        {
            RenderColor();
        }
    }

    public void Delete()
    {
        var GO = this.gameObject;
        Debug.Log(GO);
        Destroy (GO);
    }
}
