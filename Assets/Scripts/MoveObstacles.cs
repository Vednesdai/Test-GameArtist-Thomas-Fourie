using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacles : MonoBehaviour
{
    public float speed = 5;
    private bool finished = true;

	private float _initialX;

	private void Start()
	{
		_initialX = transform.localPosition.x;
	}

	private void Update()
    {
        if (finished)
        {
            finished = false;
			StartCoroutine(MoveTo(Mathf.Abs(transform.localPosition.x - _initialX) <= 0.1f ? -_initialX : _initialX, speed));
        }
    }

    IEnumerator MoveTo(float xPos, float time)
    {
        float elapsedTime = 0;
        Vector3 startingPos = transform.localPosition;
        Vector3 endPos = new Vector3(xPos, transform.localPosition.y, transform.localPosition.z);

        while (elapsedTime < time)
        {
            transform.localPosition = Vector3.Lerp(startingPos, endPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        finished = true;
    }
}