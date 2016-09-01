
using UnityEngine;

using System;
using System.Collections;
using System.Text;

class CreateUserReq
{
	public string userName;
	public string passwd;
}

public class UnityWebSocketExample : MonoBehaviour
{

	void OnSendCompleted(bool result)
	{
		return;
	}

	// Use this for initialization
	IEnumerator Start()
	{
		var ws = new WebSocketClient(new Uri("ws://echo.websocket.org"));
		yield return StartCoroutine(ws.Connect());

		var req = new CreateUserReq();
		{
			req.userName = "userName";
			req.passwd = "passwd";
		}

		var bytes = Encoding.UTF8.GetBytes(JsonUtility.ToJson(req));
		ws.Send(bytes, OnSendCompleted);
	}
}
