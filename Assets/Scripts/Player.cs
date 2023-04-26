using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private TimeManager timeManager;
    private SceneFader sceneFader;
    [SerializeField] private Volume volume;
    private ChromaticAberration aberration;
    private float targetChromatic;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D groundCheck;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    private float input;
    private bool grounded;

    private void Start()
    {
        aberration = ScriptableObject.CreateInstance<ChromaticAberration>();
        StartCoroutine(AudioManager.GetAudioManager().PlaySound(2, 2f));
        sceneFader = FindObjectOfType<SceneFader>();
    }

    private void FixedUpdate()
    {
        input = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime;

        Vector2 velocity = rb.velocity;
        velocity.x = input * speed * 100f;
        
        rb.velocity = velocity;

        if (input > 0)
        {
            spriteRenderer.flipX = true;
        }
        if (input < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            grounded = false;
            Vector2 velocity = rb.velocity;
            velocity.y = jumpSpeed;
            rb.velocity = velocity;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ShiftTime();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Reset();
        }

        aberration.intensity.value = Mathf.Lerp(aberration.intensity.value, targetChromatic, 2f * Time.deltaTime);
        volume.profile.TryGet<ChromaticAberration>(out aberration);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        grounded = true;
    }

    private void ShiftTime()
    {
        if (TimeManager.present)
        {
            targetChromatic = 0.5f;
            transform.position += new Vector3(0, 30f, 0);
            timeManager.ToFuture();
        }
        else
        {
            targetChromatic = 0f;
            timeManager.ToPresent();
            transform.position += new Vector3(0, -30f, 0);
        }
    }

    public static Player GetPlayer()
    {
        return FindObjectOfType<Player>();
    }

    private void Reset()
    {
        StartCoroutine(sceneFader.FadeAndLoadScene(SceneFader.FadeDirection.In, "Level " + SceneManager.GetActiveScene().buildIndex));
    }
}
