using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace GOIModelReplacement.Core
{
    class MenuButtons : MonoBehaviour
    {
        private GameObject template { get; set; }
        private Transform menu { get; set; }

        public void Init(Transform Base, Transform parent)
        {
            template = Base.gameObject;
            menu = parent;
        }

        public Transform AddButton(string name, UnityEngine.Events.UnityAction uAction)
        {
            GameObject tmpButton = Instantiate(template, menu);
            tmpButton.name = name;

            tmpButton.transform.SetAsFirstSibling();
            Transform buttonText = tmpButton.transform.GetChild(0);
            Destroy(buttonText.GetComponent<I2.Loc.Localize>());
            buttonText.GetComponent<TextMeshProUGUI>().text = name;
            
            Button btn = tmpButton.GetComponent<Button>();
            btn.onClick = new Button.ButtonClickedEvent();
            btn.onClick.AddListener(uAction);

            return tmpButton.transform;
        }
    }
}
