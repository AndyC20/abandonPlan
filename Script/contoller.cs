using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contoller : MonoBehaviour
{
    public CharacterController control;
    public float speed = 6f;
    [SerializeField] private float smoothTime = 0.05f;
    float turnSmoothVelocity;
    private Animator thisAnim;
    public Transform cam;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 50.0f;

    void Start()
    {
        thisAnim = GetComponent<Animator>();
    }

    void Update(){
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(h, 0f, v).normalized;

        thisAnim.SetFloat("TurningSpeed", h);
        thisAnim.SetFloat("Speed", v);

        if(direction.magnitude >= 0.1f){
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            control.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if(Input.GetMouseButtonDown(0))
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chaser"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("lose");
        }
        if(collision.gameObject.CompareTag("end")){
            UnityEngine.SceneManagement.SceneManager.LoadScene("win");
        }

        if (collision.gameObject.CompareTag("SpeedUp"))
        {
            speed += 1.0f;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("powerUp"))
        {   
            ammo ammoScript = GetComponent<ammo>();
            ammoScript.damageAmount *= 2;

            Destroy(collision.gameObject);
        }
    }
}
