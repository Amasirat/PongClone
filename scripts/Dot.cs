using Godot;

public partial class Dot : RigidBody2D
{
    public override void _Ready()
    {
        soundPlayer = GetNode<AudioStreamPlayer2D>("SoundEffect");
    }
    
    public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
        GD.Print($"Force: ");
        // Move the dot
        var collisionInfo = MoveAndCollide(LinearVelocity);
        // Check for collisions and bounce the dot
        if(collisionInfo != null)
        {
            state.LinearVelocity = LinearVelocity.Bounce(collisionInfo.GetNormal());
            // Play sound upon collision
            var rng = new RandomNumberGenerator();
            soundPlayer.PitchScale = rng.RandfRange(0.5f, 1.0f);
            soundPlayer.Play();
            
            if (collisionInfo.GetCollider().GetType().ToString() == "Guard")
            {
                ApplyForce(GetBoosted(state.LinearVelocity));
            }
        }
        GD.Print(state.LinearVelocity);
    }

    private Vector2 GetBoosted(Vector2 velocity)
    {
        Vector2 force = velocity.Normalized();
        float boost = 0.05f;
        if (velocity.X < 0.0f)
        {
            force.X -= boost;
        }
        else
        {
            force.X += boost;
        }

        return force;
    }

    public void Respawn(Vector2 pos)
    {
        LinearVelocity = new Vector2(6, 4);
        Position = pos;
    }
    
    private AudioStreamPlayer2D soundPlayer;
}
