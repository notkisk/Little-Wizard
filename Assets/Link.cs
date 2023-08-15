using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour 
{
	public string link;
	public void OpenLinkJSPlugin()
	{
		openWindow(link);
    }

    [DllImport("__Internal")]
	private static extern void openWindow(string url);

}