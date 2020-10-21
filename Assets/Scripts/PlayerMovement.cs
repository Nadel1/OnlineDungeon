using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // photon related var here
    private PhotonView PV;
    private Vector2 input;
    private Rigidbody rb;
    [SerializeField]
    [Tooltip("Movement speed of the player")]
    [Range(50, Mathf.Infinity)]
    private float moveSpeed = 100;

    [SerializeField]
    [Tooltip("Speed at which the player rotates")]
    private float turnSpeed = 15;
    public GameObject cam;
    private Camera mainCamera;

    public int team = 3;
    private bool setPlayer;
    private GameObject team1Spawn;
    private GameObject team2Spawn;

    // Start is called before the first frame update
    void Start()
    {
        // photon code here
        PV = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        team1Spawn = GameObject.FindGameObjectWithTag("spawn1");
        team2Spawn = GameObject.FindGameObjectWithTag("spawn2");
        
            if (team == 0)
            {
                transform.position = team1Spawn.transform.position;
            }
            else
            {
                transform.position = team2Spawn.transform.position;
            }

    }

    private void FixedUpdate()
    {
        if (PV.IsMine)
        {
            Movement();
            Turning();
            cam.SetActive(true);
        }
        else
        {
            cam.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        

    }
    float x, y;
    private void Movement()
    {
        rb.AddForce(Vector3.down * Time.deltaTime * 10);
       
        rb.AddForce(transform.forward * input.y * moveSpeed * 100 * Time.fixedDeltaTime );
        rb.AddForce(transform.right * input.x * moveSpeed * 100 * Time.fixedDeltaTime);
        Mathf.Clamp(rb.velocity.magnitude, 0, 5);
    }
    private void Turning()
    {
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);

    }

}
