using Godot;
using System;

public class PlayerBehScript : KinematicBody2D
{

    //Movement variables
    Vector2 dir;
    float movementSpeed = 1000;

    //gravity varriables
    float gravity = 50;
    float maxFallSpeed = 1000;
    float minFallSpeed = 5;

    //jump variables
    float jumpForce = 1000;

    //animation variables
    Sprite sprite;
    AnimationPlayer animationPlayer;

    public override void _Ready()
    {
        sprite = (Sprite)GetNode("Sprite");
        animationPlayer=(AnimationPlayer)GetNode("AnimationPlayer");
    }

    public override void _PhysicsProcess(float delta)
    {
        Gravity();
        PlayerMovement();
        FlipSprite();
        Animator();
    }

    private void PlayerMovement()
    {
        dir.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        dir.x *= movementSpeed;
        Jump();
        dir = MoveAndSlide(dir, Vector2.Up);
    }

    private void Gravity()
    {
        dir.y += gravity;
        if(dir.y>maxFallSpeed){
            dir.y = maxFallSpeed;
        }
        if(IsOnFloor()){
            dir.y = minFallSpeed;
        }
    }

    private void Jump()
    {
        if(IsOnFloor()&&Input.IsActionJustPressed("jump"))
        {
            dir.y= -jumpForce;
        }
    }

    private void FlipSprite()
    {
        if(dir.x>0)
        {
            sprite.FlipH = false;
        }
        else if(dir.x<0)
        {
            sprite.FlipH = true;
        }
    }

    private void Animator()
    {
        if(IsOnFloor()&&dir.x == 0)
        {
            animationPlayer.Play("PlayerAnime");
        }
        if(IsOnFloor()&&dir.x != 0)
        {
            animationPlayer.Play("PLayerRun");
        }
        if(!IsOnFloor())
        {
            animationPlayer.Play("PlayerJump");
        }
    }
}
