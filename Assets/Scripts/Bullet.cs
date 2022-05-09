using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private FixedJoystick fixed_joystick;
    [SerializeField] private float sas;
    public float turn_speed;

    public float bullet_speed;
    public GameObject joystick;
    public Text gameOver;
    public Button Reset;
    public Button Next_Level;
    public Button Menu;

    private bool IsEndScene = false;
    private int end_scene_count = 0;
    private void Start()
    {
        AudioListener.volume = PlayerPrefs.GetInt("Volume");
        IsEndScene = false;
        RectTransform rect = joystick.GetComponent<RectTransform>();
        if (PlayerPrefs.GetString("Pos") == "Right")
        {
            rect.position = new Vector2(-264.1f, 269.3f);
            rect.anchorMax = new Vector2(1, 0);
            rect.anchorMin = new Vector2(1, 0);
        }
        else
        {
            rect.position = new Vector2(267.22f, 269.3f);
            rect.anchorMax = new Vector2(0, 0);
            rect.anchorMin = new Vector2(0, 0);
        }
    }
    void FixedUpdate()
    {
        sas = PlayerPrefs.GetInt("Level_Save");
        transform.position += transform.forward * bullet_speed;
        if ((fixed_joystick.Horizontal != 0) || (fixed_joystick.Vertical != 0))
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(-1 * fixed_joystick.Vertical * turn_speed, fixed_joystick.Horizontal * turn_speed, transform.rotation.z));
        if (IsEndScene)
        {
            end_scene_count++;
            Camera.main.transform.LookAt(gameObject.transform);
            if (end_scene_count == 120)
            {
                IsEndScene = false;
                Destroy(gameObject);
            }
        }
        /*        else
                {
                    if (Input.touchCount == 1)
                    {
                        // GET TOUCH 0
                        Touch touch0 = Input.GetTouch(0);

                        // APPLY ROTATION
                        if (touch0.phase == TouchPhase.Moved)
                        {
                            transform.Rotate(0f, touch0.deltaPosition.x, 0f);
                        }

                    }
                }*/
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                Destroy(joystick);
                Camera.main.transform.parent = null;
                gameOver.gameObject.SetActive(true);
                Next_Level.gameObject.SetActive(true);
                Menu.gameObject.SetActive(true);
                gameOver.text = "You won!";
                if (PlayerPrefs.GetInt("Level_Save") < SceneManager.GetActiveScene().buildIndex + 1)
                {
                    PlayerPrefs.SetInt("Level_Save", SceneManager.GetActiveScene().buildIndex + 1);
                    PlayerPrefs.Save();
                }
                Destroy(gameObject);
                break;
            case "Border":
                Destroy(joystick);
                Camera.main.transform.parent = null;/*
                camera_2 = Instantiate(gameObject.GetComponentsInChildren<Camera>()[0]);
                camera_2.transform.rotation = gameObject.GetComponentsInChildren<Camera>()[0].transform.rotation;
                camera_2.transform.position = gameObject.GetComponentsInChildren<Camera>()[0].transform.position;
                camera_2.transform.LookAt(gameObject.transform);*/
                Reset.gameObject.SetActive(true);
                gameOver.gameObject.SetActive(true);
                IsEndScene = true;
                Menu.gameObject.SetActive(true);
                Destroy(gameObject.GetComponent<Camera>());
                break;
            case "Portal":
                break;
            default:
                Destroy(joystick);
                Camera.main.transform.parent = null;
                Reset.gameObject.SetActive(true);
                gameOver.gameObject.SetActive(true);
                Menu.gameObject.SetActive(true);
                Destroy(gameObject);
                break;
        }
    }
    public static void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public static void MenuLoad()
    {
        SceneManager.LoadScene("Menu");
    }
}
