using System;
using System.AI;


bool isFighting;

void Start(){

    Transform hello;
    isFighting = true;

}

void Update(){
    if (isFighting)
    {
        isFighting = false;
    }
}