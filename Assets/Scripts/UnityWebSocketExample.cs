
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Text;

enum MsgType
{
	CREATE_USER_REQ = 11001,
	CREATE_USER_ACK = 11002,
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

	public void Connect()
	{
		//ws = new WebSocketClient(new Uri("ws://echo.websocket.org"));
		ws = new WebSocketClient(new Uri("ws://127.0.0.1:20000"));
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
		var type = Convert.ToInt32(str.Substring(0, 5));

		var ack = JsonUtility.FromJson<CreateUserAck>(str.Substring(5));
		text.text = ack.errCode.ToString();
	}
}
