﻿// Using Unity System
using System.Collections;
using UnityEngine;

// Script for the Character behaviour
public class CharacterBehaviour : MonoBehaviour
{
    #region private Accessor Unity

    /// <summary>
    /// Reference of the Rigibody for the character 
    /// </summary>
    [SerializeField]
    private Rigidbody2D rigidbody2D;

    /// <summary>
    /// Reference of the vulnerability for the character 
    /// </summary>
    [SerializeField]
    private float vulnerabilityTime = 5.5f;

    #endregion


    #region Private Members



    /// <summary>
    /// Reference for the character if is grounded
    /// </summary>
    private bool isGrounded = false;

    #endregion

    #region Public Membres

   /// <summary>
    ///  Get the reference for the rigidbody for the character
    /// </summary>
    public Rigidbody2D rbd2D
    {
        get
        {
            return rigidbody2D;
        }
    }
    
    /// <summary>
    /// Reference of the speed Character
    /// </summary>
    ///Use the serielzefield
    public float speedForCharacter;

    /// <summary>
    /// Reference of the speed Character
    /// </summary>
    ///Use the serielzefield
    public float MaxJump;

    #endregion

    #region Method for the Unity

    /// <summary>
    /// Get the first method for Update
    /// </summary>
    private void Awake()
    {
        SetVelocity(speedForCharacter, 0);
    }

    /// <summary>
    /// Methods principale for the Update
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown("space") && isGrounded )
        {
            JumpCharacter();  
        } 
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Method for the Jump Character
    /// </summary>
    void JumpCharacter()
    {
        rigidbody2D.velocity += new Vector2(0, MaxJump);
    }

    /// <summary>
    /// Set the velocity
    /// </summary>
    /// <param name="_xVelocity"> X velocioty</param>
    /// <param name="_yVelocity"> Y velocity</param>
    void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rigidbody2D.velocity = new Vector2(_xVelocity, _yVelocity);
    }

    /// <summary>
    /// Reference of the OnCollisionEnter2D for the Plaform
    /// </summary>
    /// <param name="collisionEnterCharcater"></param>
    void OnCollisionEnter2D(Collision2D collisionEnterCharcater)
    {
        if (collisionEnterCharcater.gameObject.CompareTag("platform"))
        {
            isGrounded = true;
        }
    }

    /// <summary>
    /// Reference of the OnCollisionExit2D for the Plaform
    /// </summary>
    /// <param name="collisionExitCharcater"></param>
    void OnCollisionExit2D(Collision2D collisionExitCharcater)
    {
        if (collisionExitCharcater.gameObject.CompareTag("platform"))
        {
            isGrounded = false;
        }
    }

    /// <summary>
    /// Reference of the OnCollisionExit2D for the Obstacle
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(GetObstacle());
        }
    }

    /// <summary>
    /// Use the Enumerator 
    /// </summary>
    /// <returns></returns>
    IEnumerator GetObstacle()
    {
        // Stop the the player
        yield return new WaitForSeconds(0.1f);
        SetVelocity(speedForCharacter / 2, 0);

        //Attack for the Monster
        GameObject.FindWithTag("Monster").GetComponent<MonsterBehaviour>().GoCloser();

        // start the player
        yield return new WaitForSeconds(0.5f);
        SetVelocity(speedForCharacter, 0);

        // No Attack for the Monster
        yield return new WaitForSeconds(vulnerabilityTime);
        GameObject.FindWithTag("Monster").GetComponent<MonsterBehaviour>().GoFurther();

    }

    #endregion
}
