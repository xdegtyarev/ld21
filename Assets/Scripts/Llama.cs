using UnityEngine;
using System.Collections;

public class Llama : MonoBehaviour, ITriggerable
{
	public static Llama Instance;
	public tk2dAnimatedSprite sprite;
	public float animatedJumpHeight;
	public float jumpStartHeight;
	public float downAcceleration = 10f;
	public float fallSpeed;
	public float speed;
	public float maxSpeed = 10f;
	public float acceleration;
	float jumpTouchTimer;
	public float minimumJumpTouchTimer = 0.3f;
	bool waitForMinimumTime;
	float ground = 0f;
	public bool DieOnLanding;
	public bool isRunning;
	public bool isJumping;
	public bool isDoubleJump;

	public void StartGame()
	{
		speed = 1f;
		acceleration = 0.03f;
		transform.position = new Vector3(0f, 0f, 0f);
		StartCoroutine(RunningCoroutine());
	}

	IEnumerator RunningCoroutine()
	{
		isRunning = true;
		yield return new WaitForSeconds(5f);
	}

	void Awake()
	{
		Application.targetFrameRate = 60;
		Llama.Instance = this;
	}

	void OnEnable()
	{
		TouchManager.TouchBeganEvent += OnTouchBegan;
		TouchManager.TouchEndedEvent += OnTouchEnded;
	}

	void StopJumpAnimation()
	{
		animation.Stop("Jump");
		jumpStartHeight = transform.position.y;
		animation.Play("Top");
	}

	void OnTouchEnded(TouchInfo touch)
	{
		if (isJumping) {
			if (animation.IsPlaying("Jump")) {
				if (minimumJumpTouchTimer > jumpTouchTimer) {
					waitForMinimumTime = true;
				} else {
					StopJumpAnimation();
				}
			}
		}
	}

	public void OnTouchBegan(TouchInfo info)
	{
		if (isRunning) {
			Jump();
			jumpTouchTimer = 0f;
		}
	}

	void OnDisable()
	{
		TouchManager.TouchBeganEvent -= OnTouchBegan;
		TouchManager.TouchEndedEvent -= OnTouchEnded;
	}

	public void Jump()
	{
		if (isJumping) {
			if (isDoubleJump) {
				return;
			} else {
				isDoubleJump = true;
				fallSpeed = 0;
				jumpStartHeight = transform.position.y;
				animatedJumpHeight = 0f;
				animation.Stop("Jump");
				animation.Play("Jump");
				AudioController.Play("Jump");
				sprite.Play("LlamaJump");
			}
		} else {
			isJumping = true;
			fallSpeed = 0;
			jumpStartHeight = transform.position.y;
			animatedJumpHeight = 0f;
			animation.Stop("Jump");
			animation.Play("Jump");
			AudioController.Play("Jump");
			sprite.Play("LlamaJump");
		}

	}

	public void OnJumpComplete()
	{
		if (isJumping) {
			isJumping = false;
			isDoubleJump = false;
			sprite.Play("LlamaRun");
			if (DieOnLanding) {
				DieOnLanding = false;
				Die();
			}
		}
	}

	public void OnTriggerEnter(Collider collider)
	{
		if (isRunning) {
			var obstacle = collider.GetComponent<Obstacle>();
			obstacle.OnHit();
			if (obstacle.isFlower) {
			
			} else {
				Handheld.Vibrate();
				speed = speed - 0.1f;
				if (isJumping) {
					DieOnLanding = true;
					;
				} else {
					Die();
				}
			}
		}
	}

	public void Die()
	{
		acceleration = 0f;
		DieOnLanding = false;
		isRunning = false;
		StartGame();
	}

	public void Update()
	{
		if (isRunning) {
			if (isJumping) {
				if (animation.IsPlaying("Jump") || animation.IsPlaying("Top")) {
					if (animation.IsPlaying("Jump")) {
						transform.position = new Vector3(transform.position.x, jumpStartHeight + animatedJumpHeight, transform.position.z);
					}
				} else {
					if (transform.position.y > ground) {
						fallSpeed += downAcceleration * Time.deltaTime;
						transform.position += Vector3.down * fallSpeed * Time.deltaTime;
					} else {
						transform.position = new Vector3(transform.position.x, ground, transform.position.z);
						OnJumpComplete();
					}
				}
			}

			jumpTouchTimer += Time.deltaTime;
			if (waitForMinimumTime) {
				if (jumpTouchTimer >= minimumJumpTouchTimer) {
					StopJumpAnimation();
					waitForMinimumTime = false;
				}
			}

			speed = speed + acceleration * Time.deltaTime;
			if (speed > maxSpeed)
				speed = maxSpeed;
		}
	}
}