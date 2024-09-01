using Godot;

public partial class Dot : RigidBody2D
{
    public override void _Ready()
    {
        soundPlayer = GetNode<AudioStreamPlayer2D>("SoundEffect");
        LinearVelocity = InitialVelocity;
    }
    
    public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
        // Move the dot
        var collisionInfo = MoveAndCollide(LinearVelocity);
        // Check for collisions and bounce the dot
        if(collisionInfo != null)
        {
            LinearVelocity = LinearVelocity.Bounce(collisionInfo.GetNormal());
            // Play sound upon collision
            PlaySound();
            // Speed up Dot on Guard collision
            if (collisionInfo.GetCollider().GetType().ToString() == "Guard")
            {
                ApplyForce(GetBoostedForce(LinearVelocity));
            }
        }
    }

    public void Respawn(Vector2 pos, int direction)
    {
        Vector2 velocity = new Vector2(InitialVelocity.X * direction, InitialVelocity.Y);
        LinearVelocity = velocity;
        Position = pos;
    }
    // Get applicable force from the given velocity. Used to give to ApplyForce to Speed up dot.
    private Vector2 GetBoostedForce(Vector2 velocity)
    {
        Vector2 force = velocity.Normalized();
        // Whatever the direction of Velocity, force must be in that direction to boost Dot
        if (velocity.X < 0.0f)
        {
            force.X -= BoostAmount;
        }
        else
        {
            force.X += BoostAmount;
        }
        return force;
    }

    private void PlaySound()
    {
        if (Sound)
        {
            var rng = new RandomNumberGenerator();
            soundPlayer.PitchScale = rng.RandfRange(0.5f, 1.0f);
            soundPlayer.Play();
        }
    }
    [Export] public Vector2 InitialVelocity { get; set; }
    [Export] public float BoostAmount { get; set; }
    [Export] public bool Sound { get; set; } = true;
    
    private AudioStreamPlayer2D soundPlayer;
}
