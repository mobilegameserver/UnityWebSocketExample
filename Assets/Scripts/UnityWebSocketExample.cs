
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Text;

class CreateUserReq
{
	public string userName;
	public string passwd;
}

class CreateUserAck
{
	public ErrorCode errCode = ErrorCode.NONE;
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

		var type = new byte[4];
		Array.Copy(bytes, 0, type, 0, 4);
		Console.WriteLine(Encoding.UTF8.GetString(type));

		var body = new byte[bytes.Length - 4];
		Array.Copy(bytes, 4, body, 0, bytes.Length - 4);
		Console.WriteLine(Encoding.UTF8.GetString(body));
	}
}
