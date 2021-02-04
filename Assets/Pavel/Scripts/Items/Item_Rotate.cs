using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Rotate : MonoBehaviour, IIntercatable
{
    GameObject player;
    bool check = false;
    Transform rotates;
    List<Rotatable> rot_childs = new List<Rotatable>();
    Player_Movement player_Movement=null;
    Rigidbody2D plrb;
    Player_Health player_Health=null;
    Player_Attack player_Attack=null;
    // Start is called before the first frame update
    void Start()
    {
        rotates = transform.GetChild(1);
        int count  = rotates.childCount;
        for(int i=0;i<count;i++){
            rot_childs.Add(new Rotatable(rotates.transform.GetChild(i)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player_Health!=null && player_Health.isDamaged && check){
            Use(player);
        }
        if(check){
            Action();
        }
    }

    void Action(){
        float rot_pow = player_Movement.direction.x;
        Debug.Log(rot_pow);
        transform.Rotate(new Vector3(0,0,30*rot_pow*Time.deltaTime));
        foreach(Rotatable child in rot_childs){
            child.tr.position = child.pos;
            child.tr.rotation = child.rot;
            child.tr.Rotate(new Vector3(0,0,20*rot_pow*Time.deltaTime));
            child.rot = child.tr.rotation;
        }
    }

    public void Use(GameObject _player){
        if(!check && Player_Interact.player_Interact.isInteracting) return;
        check = !check;
        player = _player;
        if(check){
            if(player_Movement==null){
                player_Movement = player.GetComponent<Player_Movement>();
                player_Health = player.GetComponent<Player_Health>();
                player_Attack = player.transform.GetChild(2).gameObject.GetComponent<Player_Attack>();
                plrb = player.GetComponent<Rigidbody2D>();
            }
            player_Movement.direction= Vector2.zero;
            player_Movement.inertia=0;
            player_Movement.moveBlock=true;
            player_Movement.jump_block=true;
            player_Attack.blockAttack = true;
            Player_Interact.player_Interact.isInteracting = true;
            plrb.velocity=Vector2.zero;
        }
        else{
            player_Movement.moveBlock=false;
            player_Movement.jump_block=false;
            player_Attack.blockAttack = false;
            Player_Interact.player_Interact.isInteracting = false;
        }
    }
}

public class Rotatable{
    public Vector2 pos;
    public Quaternion rot;
    public Transform tr;

    public Rotatable(Transform t){
        tr=t;
        pos = tr.position;
        rot = tr.rotation;
    }

}
