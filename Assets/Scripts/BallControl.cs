using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallControl : MonoBehaviour
{

    public Rigidbody rb;

    public Image _levelBar;

    private Vector2 _firstPos;

    private Vector2 _secondPos;

    private Vector2 _currentPos;

    private GameManager gm;

    public float moveSpeed;

    public float currentGroundNumber;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        _levelBar.fillAmount = currentGroundNumber / gm._groundNumbers;
        if (_levelBar.fillAmount == 1)
        {
            gm.LevelUpdate();
        }
    }

    private void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _firstPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            _secondPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            _currentPos = new Vector2(
                _secondPos.x - _firstPos.x,
                _secondPos.y - _firstPos.y);
        } 

        _currentPos.Normalize();

        if (_currentPos.y < 0 && _currentPos.x > -0.5f && _currentPos.x < 0.5f) //back
        {
            rb.velocity = Vector3.back * moveSpeed;
        }
        else if (_currentPos.y > 0 && _currentPos.x > -0.5f && _currentPos.x < 0.5f) //forward
        {
            rb.velocity = Vector3.forward * moveSpeed;
        }
        else if (_currentPos.x < 0 && _currentPos.y > -0.5f && _currentPos.y < 0.5f) //left
        {
            rb.velocity = Vector3.left * moveSpeed;
        }
        else if (_currentPos.x > 0 && _currentPos.y > -0.5f && _currentPos.y < 0.5f) //right
        {
            rb.velocity = Vector3.right * moveSpeed;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<MeshRenderer>().material.color != Color.yellow)
        {
            if (other.gameObject.CompareTag("groundPiece"))
            {
                other.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
                SetConstraints();
                currentGroundNumber++;
            }
        }
       
    }

    private void SetConstraints()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }
}
