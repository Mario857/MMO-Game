public class PlayerCharacter : BaseCharacter {
	void Update(){
		Messenger<int , int>.Broadcast("player health update", 80 , 100);
		//Messenger<int , int>.Broadcast("mob health update", speed);
	}
}
