public async Task JoinRoom(string roomName)
{
    await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
    Clients.Group(roomName).addChatMessage(Context.User.Identity.Name + " joined.");
}
