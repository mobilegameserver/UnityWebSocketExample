
class UserCreateReq
{
    public MsgType msgType = MsgType.USER_CREATE_REQ;

    public string userName;
    public string passwd;
}

class UserCreateAck
{
    public MsgType msgType = MsgType.USER_CREATE_ACK;
    public ErrCode errCode;
}