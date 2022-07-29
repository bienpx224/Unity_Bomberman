# Unity_Bomberman
Game Bomberman by Unity using Tilemaps, Sprites, Animation, GameLogic, Pool, Singleton. 

## Processing do project.

### Tilemaps : 
- ở Hierarchy ta tạo : 2D > Tilemaps > Regular : Tạo xong thì chọn tile cần vẽ ở trong tile Palatte (Chưa hiện thì vào mục Window chọn 2D > tilePalatte).
- Lưu ý nên setup các tile có kích cỡ per unit tương ứng với mỗi ô trên màn hình. Ví dụ ở game mình chọn sprites, chọn pixels per unit là 16 vì sprites có size là 16x16.
- Ở trong assets Tiles. Ta ấn vào rồi chọn collider phù hợp cho từng tile. Ví dụ Tile thảm cỏ thì chọn collider là none. còn Tile tường thì chọn collider là Sprite.
- Tạo tilemap: 1 tilemap background, và 1 tilemap có thể phá huỷ, kiểu chỉ là vật cản mà thôi. (indestructibles va destructibles)
- Thêm Tilemaps collider 2D vào cho 2 tilemap đó. Nếu muốn các collider liên kết thành khối với nhau thì add thêm component Composite Collider 2D, tuy nhiên chỉnh type là static collider để nó đứng yên ko move.

### Player : 
- Thêm Component Collider 2D vào cho Player để bắt va chạm, di chuyển. Collision Detection nên để là Continuous bởi vì mình cần bắt va chạm liên tục.
- Tạo file Movement.cs gắn vào trong Player. Get lấy rigidbody của player. Ta di chuyện là dựa vào việc tác động lực vật lý và rigidbody để làm di chuyển nhân vật.
- ở hàm Update() : ta thực hiện bắt keyCode user nhập vào để thực hiện di chuyển nhân vật, setup direction cho nhân vật. (Tìm hiểu về Update và FixUPdate : https://techlittleboy.wordpress.com/2019/01/09/unity3d/)
- Ở hàm FixUpdate() : Ta thực hiện các action liên quan đến vật lí. Ở đây là dựa vào direction và speed của nhân vật để di chuyển nhân vật : 
```c# 
// In Update()
if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up);
        }
// In FixUpdate()
Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
```
- fixedDeltaTime là thời gian cố định giữa các frame trong game. thường default là 0.02s. (Setup ở trong Player > Time.)

- File AnimatedSpriteRenderer để thực hiện di chuyển animation của các sprites. (Có thể thay thế bằng cách sử dụng animation và animator controller. )
InvokeRepeating để thực hiện lặp lại 1 hàm nào đó sau 1 khoảng thời gian. (Tìm hiểu thêm về Coroutine và Invoke https://phuongne.com/coroutine-invoke/) Invoked ko bị dừng khi enabled = false.
Invoke dùng sẽ tốt hơn, tối ưu hơn và đảm bảo FPS hơn sử dụng Coroutine WaitForSeconds.
- Khi mà đang ko ở trạng thái Idle, ta sẽ cho thực hiện việc hiển thị các sprite ở trong array tạo thành animation (Ở đây animationTime là 1/6 => 1 giây chạy 6 lần hoạt ảnh)
- Ở trong file Movement.cs : Ta thực hiện enabled spriteRender tương ứng khi user di chuyển nhận vật theo hướng nào đó. trong hàm SetDirection().

### Bomb Controller : 
- Thêm script BombController.cs vào trong Player để quản lý các bomb của player. 
- Setup số lượng, thời gian destroy bomb và collider2D cho bomb.
- Lưu ý : Trong Collider nếu để isTrigger true thì sẽ ko có tác động vật lý (VD: Player đâm vào bom sẽ đi xuyên qua chứ ko đẩy quả bom đó). 
- Muốn Player đẩy được bomb ta thì trong collider2D set isTrigger = true và trong BombController bắt event : Sau khi Player đi ra khỏi quả bom vừa sinh ra thì sẽ tắt isTrigger, cho phép tương tác vật lý.
```c# 
private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("bomb")){
            other.isTrigger = false; 
        }
    }
```

### Explosion : 
- Tạo GameObject có collider2D set isTrigger = true. bên trong tạo 3 SpirteRenderer chứa animatedSpriteRenderer của 3 state Start, Middle và End.
- Trong BombController, khi Destroy Bomb thì cho sinh ra Explosion tại ví trí quả bom.

### Cơ bản đã đủ kiến thức, tương tự với những phần khác như Item.
- Tạo GameManager.cs (Singleton) để quản lý điểm số, số lượng Player trong game, Các tính năng như Pause, Restart, ...v..v..


### Lưu ý : 
- Để 2 Player có thể đi xuyên qua nhau mà ko có collider -> Vào Project Settings > Physics2D > Layer Collision Matrix và bỏ tích chọn giữa Player với Player.


