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

		public Vector3 scaleMultiplier = Vector3.one;

		private void LateUpdate()
		{
			if (SyncPosition) transform.position = target.position;
			if (SyncRotation) transform.rotation = target.rotation;
			if (SyncScale) transform.localScale = Vector3.Scale(target.localScale, scaleMultiplier);
		}

        public void Init(Transform target, bool syncRotation, bool syncPosition, bool syncScale)
        {
			this.target = target;
			SyncRotation = syncRotation;
			SyncPosition = syncPosition;
			SyncScale = syncScale;

			scaleMultiplier = transform.localScale;
        }
    }
}