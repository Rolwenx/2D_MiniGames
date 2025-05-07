using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
   
   [SerializeField] private float _speed;
   [SerializeField] private Renderer _bgRend;

   void Update(){
        _bgRend.material.mainTextureOffset += new Vector2(_speed*Time.deltaTime,0);
        
   }
}
