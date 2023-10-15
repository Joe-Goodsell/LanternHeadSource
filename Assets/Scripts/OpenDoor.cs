using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class OpenDoor : MonoBehaviour
{
    enum State {
        Open,
        Closed
    }
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _closeSideRenderer;
    [SerializeField] private SpriteRenderer _textRenderer;
    [SerializeField] private GameObject _openCollider;
    [SerializeField] private GameObject _closedCollider;
    [SerializeField] private Sprite _openText;
    [SerializeField] private Sprite _closeText;
    [SerializeField] private Sprite openSprite1;
    [SerializeField] private Sprite closedSprite1;
    [SerializeField] private Sprite openSprite2;
    [SerializeField] private Sprite closedSprite2;
    [SerializeField] private AudioClip doorOpen;
    [SerializeField] private AudioClip doorClose;
    private AudioSource audioSource;
    private bool playerIsInteracting;

    private State state; 
    // Start is called before the first frame update
    void Start()
    {
        playerIsInteracting = false;
        audioSource = GetComponent<AudioSource>();
        SetState(State.Closed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "LanternHead")
        {
            playerIsInteracting = true;
            _textRenderer.enabled = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "LanternHead")
        {
            playerIsInteracting = false;
            _textRenderer.enabled = false;
        }
    }


    void SetState(State newState)
    {
        if ( newState == State.Open )
        {
            state = State.Open;
            _spriteRenderer.sprite = openSprite1;
            _closeSideRenderer.sprite = openSprite2;
            _openCollider.SetActive(true);
            _closedCollider.SetActive(false);
            _textRenderer.sprite = _closeText;
        } 
        else if ( newState == State.Closed )
        {
            state = State.Closed;
            _spriteRenderer.sprite = closedSprite1;
            _closeSideRenderer.sprite = closedSprite2;
            _openCollider.SetActive(false);
            _closedCollider.SetActive(true);
            _textRenderer.sprite = _openText;
            
        }
    }

    void FlipState()
    {
        Debug.Log( "flipping door state" );
        if ( state == State.Open )
        {
            audioSource.clip = doorClose;
            audioSource.Play();
            SetState( State.Closed );
        } 
        else 
        {
            audioSource.clip = doorOpen;
            audioSource.Play();
            SetState( State.Open );
        }
    }
    

    // Update is called once per frame
    void Update()
    {
       if ( playerIsInteracting )
       {
            if ( Input.GetKeyDown(KeyCode.E) )
            {
                _textRenderer.enabled = false;
                //SetState(State.Open);
                FlipState();
            }
       } 
    }
}
