namespace EndProject.API.Hub;
using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    private readonly string _botUser;
    private readonly IDictionary<string, UserConnection> _connections;
    public ChatHub(IDictionary<string, UserConnection> connections)
    {
        _botUser = "MyChat Bot";
        _connections = connections;

    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
        {
            _connections.Remove(Context.ConnectionId);
            Clients.Group(userConnection.Room)
                .SendAsync("ReceiveMessage", _botUser, $"{userConnection.User} has left");  
        }
        return base.OnDisconnectedAsync(exception);
    }

    public async Task JoinRoom(UserConnection userConnection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);
        _connections[Context.ConnectionId] = userConnection;

        await Clients.Groups(userConnection.Room).SendAsync("ReciveMessage", _botUser,
            $"{userConnection.User} has joined {userConnection.Room}");

        SendConnectedUsers(userConnection.Room);
    }


    public async Task SendMessage(string message)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
        {
            await Clients.Group(userConnection.Room)
                    .SendAsync("ReceiveMessage", userConnection.User, message);
        }
    }

    public Task SendConnectedUsers(string room)
    {
        var users = _connections.Values
                .Where(x => x.Room == room)
                .Select(x => x.User);

        return Clients.Group(room).SendAsync("UsersInRoom",users);
    }
}
