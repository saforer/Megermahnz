using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {
    public BoxCollider2D OneWayObject;
    public int _roomWidth;
    public int _roomHeight;
    public int _tileSize;
    public string roomName;
    public Vector2 MinimapRoomCoordinates;
    public Transform CubeCol;
    public Transform LadderCol;
    public string roomFile;
    public TextAsset dataAsset;
    public List<BoxCollider2D> collisionBoxList = new List<BoxCollider2D>();
    
    // Use this for initialization
    void Start () {
        //TextAsset dataAsset = (TextAsset) Resources.Load (roomFile, typeof(TextAsset));
        
        if(!dataAsset) Debug.Log ("No room file!");
        
        Dictionary<string,object> hash = dataAsset.text.dictionaryFromJson();
        
        _roomWidth = int.Parse(hash["width"].ToString());
        _roomHeight = int.Parse(hash["height"].ToString());
        _tileSize = int.Parse(hash["tilewidth"].ToString());
        
        string[] pathSplit = roomFile.Split(new char[] {'/'});
        roomName = pathSplit[pathSplit.Length -1];
        
        List<object> layersList = (List<object>)hash["layers"];
        
        for (int i=0; i < layersList.Count; i++)
        {
            
            Dictionary<string,object> layerHash = (Dictionary<string,object>)layersList[i];
            
            
            if (layerHash["name"].ToString().Equals("Object Layer"))
            {
                
                // Load object data if it exists...
                List<object> objectList = (List<object>)layerHash["objects"];
                
                for (int j=0; j < objectList.Count; j++)
                {
                    
                    Dictionary<string,object> objHash = (Dictionary<string,object>)objectList[j];
                    
                    if (objHash["type"].ToString().ToUpper().Equals("COLLISION"))
                    {
                        //CollisionBox _cbox = new CollisionBox();
                        GameObject _cbox = new GameObject(objHash["name"].ToString());
                        var coll = _cbox.AddComponent("BoxCollider2D") as BoxCollider2D;
                        //_cbox.name = objHash["name"].ToString();
                        //Debug.Log (objHash["name"].ToString());
                        //_cbox.name = objHash["name"].ToString();
                        Vector3 _cboxcenter = new Vector3(0,0,0);
                        Vector3 _cboxsize = new Vector3(0,0,0);
                        //_cbox.box.x = int.Parse(objHash["x"].ToString()) - (GetRoomWidth() / 2);
                        _cboxcenter.x = int.Parse(objHash["x"].ToString()) - (GetRoomWidth() / 2);
                        //_cbox.box.y = -(int.Parse(objHash["y"].ToString()) - (GetRoomHeight() / 2));
                        _cboxcenter.y = int.Parse(objHash["y"].ToString()) - (GetRoomWidth() / 2);
                        //_cbox.box.width = int.Parse(objHash["width"].ToString());
                        _cboxsize.x = int.Parse(objHash["width"].ToString()) * 0.0625f ;
                        //_cbox.box.height = int.Parse(objHash["height"].ToString());
                        _cboxsize.y = int.Parse(objHash["height"].ToString()) * 0.0625f;
                        //_cbox.box.y = _cbox.box.y - _cbox.box.height;
                        _cboxcenter.y = _cboxcenter.y - _cboxsize.y;
                        coll.size = _cboxsize;
                        //coll.center = _cboxcenter;
                        _cbox.transform.position = new Vector3(int.Parse(objHash["x"].ToString()),-int.Parse(objHash["y"].ToString()),0)*(1.0f/16.0f);
                        coll.center = new Vector3(coll.size.x/2, -1*(coll.size.y/2),0);
                        _cbox.layer=10;
                        _cbox.tag="Ground";
                    }
                    
                    if (objHash["type"].ToString().ToUpper().Equals("LADDER"))
                    {
                        GameObject _cbox = new GameObject(objHash["name"].ToString());
                        var coll = _cbox.AddComponent("BoxCollider2D") as BoxCollider2D;
                        Vector3 _cboxcenter = new Vector3(0,0,0);
                        Vector3 _cboxsize = new Vector3(0,0,0);
                        _cboxcenter.x = int.Parse(objHash["x"].ToString()) - (GetRoomWidth() / 2);
                        _cboxcenter.y = int.Parse(objHash["y"].ToString()) - (GetRoomWidth() / 2);
                        _cboxsize.x = int.Parse(objHash["width"].ToString()) * 0.0625f ;
                        _cboxsize.y = int.Parse(objHash["height"].ToString()) * 0.0625f ;
                        _cboxcenter.y = _cboxcenter.y - _cboxsize.y;
                        coll.size = _cboxsize;
                        _cbox.transform.position = new Vector3(int.Parse(objHash["x"].ToString()),-int.Parse(objHash["y"].ToString()),0)*(1.0f/16.0f);
                        coll.center = new Vector3(coll.size.x/2, -1*(coll.size.y/2),0);
                        coll.isTrigger=true;
                        _cbox.layer=11;
                        _cbox.tag="Ladder";
                    }

                    if (objHash["type"].ToString().ToUpper().Equals("ONEWAY"))
                    {
                        Vector3 onewaylocation = new Vector3((int.Parse(objHash["x"].ToString()) * 0.0625f),(-int.Parse(objHash["y"].ToString()) * 0.0625f),0);
                        Rigidbody2D oneWayInstance = Instantiate(OneWayObject, onewaylocation, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
                    }
                }
            }
        }
    }
    
    public int GetRoomHeight()
    {
        return _roomHeight * _tileSize;
    }
    
    public int GetRoomWidth()
    {
        return _roomWidth * _tileSize;
    }
    
}