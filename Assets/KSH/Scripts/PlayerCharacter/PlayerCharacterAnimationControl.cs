using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterAnimationControl : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Animator anim;
    public AudioSource footSoundPlayer;
    public AudioClip[] footSound;
    bool isPush = false;
    bool isHook = false;
    bool isLadder = false;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void SetAxisX()
    {
        anim.SetFloat("AnimAxisX", rigid.velocity.x);
    }

    void SetAxisY()
    {
        anim.SetFloat("AnimAxisY", rigid.velocity.y);
    }

    public void SetPushAnimation(bool _value)
    {
        isPush = _value;
        anim.SetBool("AnimPush", _value);
    }

    public void SetHookAnim(bool _value)
    {
        isHook = _value;
        anim.SetBool("AnimHook", _value);
    }

    public void SetLadderAnim(bool _value)
    {
        isLadder = _value;
        anim.SetBool("AnimLadder", _value); 
    }

    public void PlayLFootSound()
    {
        footSoundPlayer.clip = footSound[0];
        footSoundPlayer.Play();
    }

    public void PlayRFootSound()
    {
        footSoundPlayer.clip = footSound[1];
        footSoundPlayer.Play();
    }

    private void Update()
    {
        anim.SetBool("isLanding", PlayerInfo.Instance.IsRanding);
        SetAxisX();
        SetAxisY();
    }
}
