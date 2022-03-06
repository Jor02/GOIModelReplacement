using System;
using UnityEngine;

namespace GOIModelReplacement.Core
{
	public class Sync : MonoBehaviour
	{
		private Transform target;

		private bool SyncRotation = true;
		private bool SyncPosition = true;
		private bool SyncScale = true;

		private void LateUpdate()
		{
			if (SyncPosition) transform.position = target.position;
			if (SyncRotation) transform.rotation = target.rotation;
			if (SyncScale) transform.localScale = target.localScale;
		}

        public void Init(Transform target, bool syncRotation, bool syncPosition, bool syncScale)
        {
			this.target = target;
			SyncRotation = syncRotation;
			SyncPosition = syncPosition;
			SyncScale = syncScale;
        }
    }
}