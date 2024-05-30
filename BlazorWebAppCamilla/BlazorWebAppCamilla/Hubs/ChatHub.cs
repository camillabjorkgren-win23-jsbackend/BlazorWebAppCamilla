using Microsoft.AspNetCore.SignalR;

namespace BlazorWebAppCamilla.Hubs;


//public class ChatHub : Hub
//{
//    public override Task OnConnectedAsync()
//    {
//        return base.OnConnectedAsync();
//    }

//    public override Task OnDisconnectedAsync(Exception? exception)
//    {
//        return base.OnDisconnectedAsync(exception);
//    }

//    public async Task Typing(string userName)
//    {
//        await Clients.Others.SendAsync("UserTyping", userName);
//    }
//    public async Task SendMessageToAll(string userName, string message, DateTime dateTime)
//    {
//        try
//        {
//            Console.WriteLine($"Attempting to send message from {userName} at {dateTime}: {message}");
//            await Clients.All.SendAsync("ReceiveMessage", userName, message, dateTime);
//            Console.WriteLine("Message sent successfully.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex.Message);
//        }
//    }
//}