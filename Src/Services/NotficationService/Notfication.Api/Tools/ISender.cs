namespace Notfication.Api.Tools
{
    public interface ISender
    {
        Task<bool>Send(string from, string message);
    }
    public class MessageSender : ISender
    {
        public async Task<bool> Send(string from, string message)
        {
            Console.WriteLine("SendMessage from :" +from+"  message:" + message);
            return true;
        }
    }
}
