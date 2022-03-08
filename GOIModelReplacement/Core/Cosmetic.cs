using System;
using UnityEngine;

namespace GOIModelReplacement.Core
{
	public class Cosmetic : MonoBehaviour
	{
		public CosmeticType Type;

		public bool SyncRotation = true;
		public bool SyncPosition = true;
		public bool SyncScale = true;

		public bool replacement;

		private void Start()
		{
			Animator anim = gameObject.GetComponent<Animator>();

			if (Type == CosmeticType.Body || Type == CosmeticType.Hammer)
			{
				foreach (Transform transform in gameObject.GetComponentsInChildren<Transform>(true))
				{
					GameObject gameObject = GameObject.Find(transform.name);

					if ((anim != null && transform != anim.GetBoneTransform(HumanBodyBones.Hips)) || gameObject.name != "mixamorig:Hips")
					if (gameObject != null && gameObject != transform.gameObject)
					{
						transform.gameObject.AddComponent<Sync>().Init(gameObject.transform, SyncRotation, SyncPosition, SyncScale);
					}
				}
			}
			switch (Type)
			{
				case CosmeticType.Body:
					{
						Transform replacementTransform = null;
						if (anim != null) replacementTransform = anim.GetBoneTransform(HumanBodyBones.Hips);//transform.Find("mixamorig:Hips");
						if (replacementTransform == null) replacementTransform = transform.RecursiveFindChild("mixamorig:Hips");

						if (replacementTransform != null)
						{
							Transform PlayerHips = GameObject.Find("Player/dude/mixamorig:Hips").transform;
							Vector3 lossyScale = replacementTransform.lossyScale;
							Vector3 localPos = PlayerHips.transform.InverseTransformPoint(PlayerHips.parent.parent.position + replacementTransform.position);
							replacementTransform.SetParent(PlayerHips.transform, true);
							replacementTransform.localPosition = localPos;
							replacementTransform.localRotation = Quaternion.Euler(Vector3.zero);
							replacementTransform.localScale = lossyScale;
						}
						if (replacement)
						{
							GameObject.Find("Player/dude/Body").GetComponent<SkinnedMeshRenderer>().material.color = new Color(0f, 0f, 0f, 0f);
							GameObject.Find("Player/dude/Eyelashes").SetActive(false);
							GameObject.Find("Player/dude/Eyes").SetActive(false);
							return;
						}
						return;
					}
				case CosmeticType.Pot:
					{
						Transform pot = GameObject.Find("Player/Pot").transform;
						Vector3 localPos = pot.InverseTransformPoint(pot.parent.position + transform.position);
						transform.SetParent(pot, true);
						transform.localPosition = localPos;
						if (replacement)
						{
							GameObject.Find("Player/Pot/Mesh").SetActive(false);
							return;
						}
						return;
					}
				case CosmeticType.Hammer:
					Debug.Log("Hammer");
					return;
				default:
					return;
			}
		}

		public enum CosmeticType
		{
			Body,
			Pot,
			Hammer
		}
	}
}
