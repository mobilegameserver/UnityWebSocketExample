
using System;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

class ConnectAck
{
	public ErrCode errCode;
}

public class UnityWebSocketExample : MonoBehaviour
{
	public Text text;

	WebSocketClient ws;

	public void Connect()
	{
		ws = new WebSocketClient(new Uri("ws://echo.websocket.org"));
		//ws = new WebSocketClient(new Uri("ws://127.0.0.1:20001"));
		ws.Connect();
	}

	void OnSendCompleted(bool result)
	{
		return;
	}

	public void Send()
	{
		if (ws.isConnected == false)
		{
			return;
		}

		var req = new UserCreateReq();
		{
			req.userName = "userName";
			req.passwd = "passwd";
		}

		var bytes = Encoding.UTF8.GetBytes(JsonUtility.ToJson(req));
		ws.Send(bytes, OnSendCompleted);
	}

	public void Receive()
	{
		var bytes = ws.Receive();
		if (bytes == null)
		{
			return;
		}

        var ack = JsonUtility.FromJson<ConnectAck>(Encoding.UTF8.GetString(bytes));
		text.text = ack.errCode.ToString();
	}
}
