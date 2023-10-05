using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    enum State {
        Open,
        Closed
    }
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _textRenderer;
    [SerializeField] private GameObject _openCollider;
    [SerializeField] private GameObject _closedCollider;
    [SerializeField] private Sprite _openText;
    [SerializeField] private Sprite _closeText;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private Sprite closedSprite;
    private bool playerIsInteracting;

    private State state; 
    // Start is called before the first frame update
    void Start()
    {
        playerIsInteracting = false;
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
            _spriteRenderer.sprite = openSprite;
            _openCollider.SetActive(true);
            _closedCollider.SetActive(false);
            _textRenderer.sprite = _closeText;
        } 
        else if ( newState == State.Closed )
        {
            state = State.Closed;
            _spriteRenderer.sprite = closedSprite;
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
            SetState( State.Closed );
        } 
        else 
        {
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
