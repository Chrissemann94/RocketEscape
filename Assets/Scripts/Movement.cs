using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
       
    [SerializeField] float thrustSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftthrusterParticles;
    [SerializeField] ParticleSystem rightthrusterParticles;

    Rigidbody rb;
    AudioSource rocketSound;

    #region MainMethods
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rocketSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        RotationHandler();
    }

    void StopThrusting()
    {
        rocketSound.Stop();
        mainEngineParticles.Stop();
    }

    #endregion

    #region HelperMethods
    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * thrustSpeed);
        if (!rocketSound.isPlaying)
        {
            rocketSound.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    void RotationHandler()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
            if (!leftthrusterParticles.isPlaying)
            {
                leftthrusterParticles.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
            if (!rightthrusterParticles.isPlaying)
            {
                rightthrusterParticles.Play();
            }
        }
        else
        {
            leftthrusterParticles.Stop();
            rightthrusterParticles.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;   // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false;  // unfreezing rotation so physics system can take over
    }

    #endregion
}
