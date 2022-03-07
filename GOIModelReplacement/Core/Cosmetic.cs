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
						Transform ReplacementTransform = null;
						if (anim != null) ReplacementTransform = anim.GetBoneTransform(HumanBodyBones.Hips);//transform.Find("mixamorig:Hips");
						if (ReplacementTransform == null) ReplacementTransform = transform.RecursiveFindChild("mixamorig:Hips");

						Debug.Log(ReplacementTransform == null);
						if (ReplacementTransform != null)
						{
							GameObject PlayerHips = GameObject.Find("Player/dude/mixamorig:Hips");
							Vector3 lossyScale = ReplacementTransform.lossyScale;
							Vector3 localPos = PlayerHips.transform.InverseTransformPoint(PlayerHips.transform.position - new Vector3(0f, -0.3f, 0.6f) + ReplacementTransform.position);
							ReplacementTransform.SetParent(PlayerHips.transform, true);
							ReplacementTransform.localPosition = localPos;
							ReplacementTransform.localRotation = Quaternion.Euler(Vector3.zero);
							ReplacementTransform.localScale = lossyScale;
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
						GameObject gameObject3 = GameObject.Find("Player/Pot");
						transform.parent = gameObject3.transform;
						transform.localPosition = Vector3.zero;
						transform.localRotation = Quaternion.Euler(Vector3.zero);
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
