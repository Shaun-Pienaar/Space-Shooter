using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerHard : EnemyController {

	public override IEnumerator Evade(){
		yield return new WaitForSeconds (Random.Range (dodgeDelay.x, dodgeDelay.y));
		while(true){
			if(playerTransform != null)
				manouverTarget = playerTransform.position.x - transform.position.x;
			yield return new WaitForSeconds (Random.Range (dodgeTime.x, dodgeTime.y));
			manouverTarget = 0f;
			yield return new WaitForSeconds (Random.Range (timeBetweenDodges.x, timeBetweenDodges.y));
		}
	}

	public override void AdjustForDifficulty (){
		
	}
}
