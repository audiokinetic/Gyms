/*******************************************************************************
The content of this file includes portions of the AUDIOKINETIC Wwise Technology
released in source code form as part of the SDK installer package.

Commercial License Usage

Licensees holding valid commercial licenses to the AUDIOKINETIC Wwise Technology
may use this file in accordance with the end user license agreement provided 
with the software or, alternatively, in accordance with the terms contained in a
written agreement between you and Audiokinetic Inc.

Apache License Usage

Alternatively, this file may be used under the Apache License, Version 2.0 (the 
"Apache License"); you may not use this file except in compliance with the 
Apache License. You may obtain a copy of the Apache License at 
http://www.apache.org/licenses/LICENSE-2.0.

Unless required by applicable law or agreed to in writing, software distributed
under the Apache License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES
OR CONDITIONS OF ANY KIND, either express or implied. See the Apache License for
the specific language governing permissions and limitations under the License.
*******************************************************************************/

using System.Collections;
using UnityEngine;

public class PatrolToPoints : MonoBehaviour
{
    [SerializeField] Transform[] patrolPointsArray;
    [SerializeField] private float Speed = 1;

    private Vector3 destination;
    private int currentPatrolPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentPatrolPointIndex = 0;
        StartCoroutine(MoveInPatrolPath());
    }

    private IEnumerator MoveInPatrolPath()
    {
        while (true)
        {
            destination = patrolPointsArray[currentPatrolPointIndex].position;
            float moveDuration = Vector3.Distance(transform.position, destination) / Speed;
            float timer = 0;
            Vector3 startingPosition = transform.position;

            while (timer < moveDuration)
            {
                transform.position = Vector3.Lerp(startingPosition, destination, timer / moveDuration);
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            transform.position = destination;
            currentPatrolPointIndex = currentPatrolPointIndex + 1;
            currentPatrolPointIndex = currentPatrolPointIndex % patrolPointsArray.Length;
        }
    }
}
