
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Text;

enum MsgType
{
	CONNECT_ACK = 11000,

	CREATE_USER_REQ = 11101,
	CREATE_USER_ACK = 11102,
}

class ConnectAck
{
	public ErrCode errCode;
}

class CreateUserReq
{
	public MsgType msgType = MsgType.CREATE_USER_REQ;

	public string userName;
	public string passwd;
}

class CreateUserAck
{
	public ErrCode errCode;
}

public class UnityWebSocketExample : MonoBehaviour
{
	public Text text;

	WebSocketClient ws;
	//StringBuilder sb;

	public void Connect()
	{
		//ws = new WebSocketClient(new Uri("ws://echo.websocket.org"));
		ws = new WebSocketClient(new Uri("ws://127.0.0.1:20001"));
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

		var req = new CreateUserReq();
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

		var str = Encoding.UTF8.GetString(bytes);
		var type = str.Substring(2, 5);

		var ack = JsonUtility.FromJson<ConnectAck>(str.Substring(9, str.Length - 10));
		text.text = ack.errCode.ToString();
	}
}
