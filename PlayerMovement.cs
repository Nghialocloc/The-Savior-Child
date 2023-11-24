using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] GameObject _direction; 
    float speed = 10;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float x = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float y = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        characterController.Move(y*_direction.transform.right + x *_direction.transform.forward);
        transform.forward = _direction.transform.forward;
        characterController.Move(new Vector3(0, -0.1f, 0));
    }
}
