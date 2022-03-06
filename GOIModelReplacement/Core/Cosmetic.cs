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
			if (Type == CosmeticType.Body || Type == CosmeticType.Hammer)
			{
				foreach (Transform transform in gameObject.GetComponentsInChildren<Transform>(true))
				{
					GameObject gameObject = GameObject.Find(transform.name);
					if (gameObject != null)
					{
						transform.gameObject.AddComponent<Sync>().Init(gameObject.transform, SyncRotation, SyncPosition, SyncScale);
					}
				}
			}
			switch (Type)
			{
				case CosmeticType.Body:
					{
						Animator anim = gameObject.GetComponent<Animator>();
						if (anim != null)
						{
							Transform ReplacementTransform = anim.GetBoneTransform(HumanBodyBones.Hips);//transform.Find("mixamorig:Hips");
							if (ReplacementTransform == null) replacement = transform.Find("mixamorig:Hips");

							if (ReplacementTransform != null)
							{
								GameObject PlayerHips = GameObject.Find("Player/dude/mixamorig:Hips");
								Debug.Log(PlayerHips);
								Vector3 lossyScale = ReplacementTransform.lossyScale;
								ReplacementTransform.SetParent(PlayerHips.transform, false);
								ReplacementTransform.localPosition = Vector3.zero;
								ReplacementTransform.localRotation = Quaternion.Euler(Vector3.zero);
								ReplacementTransform.localScale = lossyScale;
							}
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
