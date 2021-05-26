using Tool.Model;

namespace Tool.Help
{
    public class Code
    {
        public static void Result(ref MessageModel msg, 编码 c, string str)
        {
            msg.errcode = (int)c;
            msg.msgtext = c.ToString() + "," + str;
        }
        public static void Result(ref MessageModel msg, int c, string str)
        {
            msg.errcode = c;
            msg.msgtext = str;
        }
    }

    public enum 编码
    {
        成功 = 0,
        用户身份错误 = -1001,
        消息头错误 = -1003,
        参数错误 = -1004,
        程序错误 = -8888,
        其他错误 = -9999
    }
}