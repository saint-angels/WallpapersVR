using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using TMPro.EditorUtilities;

public class UGuiTextToTextMeshPro : Editor
{
    [MenuItem("GameObject/UI/Convert To Text Mesh Pro", false, 4000)]
    static void DoIt()
    {
        foreach (GameObject go in Selection.objects) 
        {
            Debug.Log(go.name);
            Text uiText = go.GetComponent<Text>();

            if (uiText == null)
                continue;
            Text cpy = Instantiate(uiText);
            DestroyImmediate(uiText);
            uiText = cpy;
            
            TextMeshProUGUI tmp = go.AddComponent<TextMeshProUGUI>();

            if (tmp == null)
            {
                EditorUtility.DisplayDialog(
                    "ERROR!",
                    "Something went wrong! Text Mesh Pro did not select the newly created object.",
                    "OK",
                    "");
                return;
            }

            tmp.fontStyle = GetTmpFontStyle(uiText.fontStyle);

            tmp.fontSize = uiText.fontSize;
            tmp.fontSizeMin = uiText.resizeTextMinSize;
            tmp.fontSizeMax = uiText.resizeTextMaxSize;
            tmp.enableAutoSizing = uiText.resizeTextForBestFit;
            tmp.alignment = GetTmpAlignment(uiText.alignment);
            tmp.text = uiText.text;
            tmp.color = uiText.color;

            tmp.font = GameController.Instance.font;
            DestroyImmediate(cpy);
            DestroyImmediate(uiText);
        }
    }


	private static FontStyles GetTmpFontStyle(FontStyle uGuiFontStyle)
	{
		FontStyles tmp = FontStyles.Normal;
		switch (uGuiFontStyle)
		{
			case FontStyle.Normal:
			default:
				tmp = FontStyles.Normal;
				break;
			case FontStyle.Bold:
				tmp = FontStyles.Bold;
				break;
			case FontStyle.Italic:
				tmp = FontStyles.Italic;
				break;
			case FontStyle.BoldAndItalic:
				tmp = FontStyles.Bold | FontStyles.Italic;
				break;
		}

		return tmp;
	}


	private static TextAlignmentOptions GetTmpAlignment(TextAnchor uGuiAlignment)
	{
		TextAlignmentOptions alignment = TextAlignmentOptions.TopLeft;

		switch (uGuiAlignment)
		{
			default:
			case TextAnchor.UpperLeft:
				alignment = TextAlignmentOptions.TopLeft;
				break;
			case TextAnchor.UpperCenter:
				alignment = TextAlignmentOptions.Top;
				break;
			case TextAnchor.UpperRight:
				alignment = TextAlignmentOptions.TopRight;
				break;
			case TextAnchor.MiddleLeft:
				alignment = TextAlignmentOptions.MidlineLeft;
				break;
			case TextAnchor.MiddleCenter:
				alignment = TextAlignmentOptions.Midline;
				break;
			case TextAnchor.MiddleRight:
				alignment = TextAlignmentOptions.MidlineRight;
				break;
			case TextAnchor.LowerLeft:
				alignment = TextAlignmentOptions.BottomLeft;
				break;
			case TextAnchor.LowerCenter:
				alignment = TextAlignmentOptions.Bottom;
				break;
			case TextAnchor.LowerRight:
				alignment = TextAlignmentOptions.BottomRight;
				break;
		}

		return alignment;
	}
}