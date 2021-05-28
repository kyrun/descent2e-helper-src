using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieAnimator : MonoBehaviour
{
	const float ROLL_DURATION = 0.5f;
	const float DELAY_PER_DIE = 0.5f;

	const float PULSE_SCALE = 0.1f;
	const float PULSE_DURATION = 0.2f;

	[SerializeField] Image _dieFace = default;

	public bool IsRolling { get; private set; }

	public void SetFaceVisible(bool show)
	{
		_dieFace.enabled = show;
	}

	public void Roll(DieDef die, int rollFaceIndex, int rollOrder)
	{
		StartCoroutine(AnimateRoll(die, rollFaceIndex, rollOrder));
	}

	IEnumerator AnimateRoll(DieDef die, int finalRollFaceIndex, int rollOrder)
	{
		IsRolling = true;

		_dieFace.enabled = false;

		yield return new WaitForSeconds(rollOrder * DELAY_PER_DIE);

		_dieFace.enabled = true;

		var ctr = 0f;
		var duration = ROLL_DURATION;
		int lastRandomFace = -1;
		do
		{
			int randomFace = -1;
			do
			{
				randomFace = Random.Range(0, die.NumFaces());
			}
			while (randomFace == lastRandomFace &&
				die.NumFaces() > 1); // prevent infinite loop
			_dieFace.sprite = die.GetFaceImage(randomFace);

			lastRandomFace = randomFace;

			yield return null;

			ctr += Time.deltaTime;
		} while (ctr < duration);

		_dieFace.sprite = die.GetFaceImage(finalRollFaceIndex);

		ctr = 0f;

		while (ctr < PULSE_DURATION)
		{
			var scale = Vector3.one * (1 + (PULSE_SCALE * Mathf.Sin(Mathf.PI * ctr/PULSE_DURATION)));
			_dieFace.transform.localScale = scale;

			yield return null;

			ctr += Time.deltaTime;
		}
		_dieFace.transform.localScale = Vector3.one;

		IsRolling = false;
	}

	public static IEnumerator WaitForUntilAllDiceFinishRolling(List<DieAnimator> dice)
	{
		bool finished;

		do
		{
			finished = true;
			foreach (var die in dice)
			{
				if (die.IsRolling)
				{
					finished = false;
					break;
				}
			}

			yield return null;
		}
		while (!finished);
	}
}
