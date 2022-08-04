# Unity_Bomberman
Game Bomberman by Unity using Tilemaps, Sprites, Animation, GameLogic, Pool, Singleton. 

## PHOTON PUN2 INTEGRATE : 
- Package Manager download Package Pun2 Free and Import it.
- Sau khi import xong sẽ hiện Popup để đăng kí account hoặc là điền application id (Lấy ở trên tk Photon trên web khi tạo application).
- Tạo script LobbyNetworkManager.cs để ở trong Scene Lobby, dùng để connect to server photon khi Start(). 

### In Lobby : 
- Trên scene tạo UI để tạo Room gồm chỗ nhập tên và button tạo room. 
- Trong LobbyNetworkManager.cs ta bắt sự kiện OnJoinedRoom() và tạo hàm CreateRoom() để thực hiện tạo room trong Photon. 
- Khi tạo room thành công, OnJoinedRoom() sẽ được kích hoạt để biết ta chính ta đã vào Room vừa tạo và ta có thể lấy name của Room thông qua PhotonNetwork.CurrentRoom.Name (nếu khi CreateRoom ko truyền name lên thì sẽ mặc định là chuỗi id)
- ĐÃ tạo Room rồi thì sẽ ko thể tạo room tiếp nữa, ta sẽ cần lắng nghe sự kiện OnJoinedLobby hoặc OnConnectToMaster để biết user đó đã đang ở ngoài và free, có thể tạo dc room.
- Để ý commit "+ Add PUN2 - Lobby actions with Room" : Tại đây đã có 1 base project với các function từ scene Menu, điền tên, vào scene Lobby, kết nối Photon2, và thực hiện các actions cần thiết như Tạo room, Rời Room, List các Room, List Player in Room... Đọc code trong đó. đơn giản. 



