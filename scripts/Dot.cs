using Godot;

public partial class Dot : RigidBody2D
{
    public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
        // Move the dot
        var collisionInfo = MoveAndCollide(LinearVelocity);
        // Check for collisions and bounce the dot
        if(collisionInfo != null)
        {
            state.LinearVelocity = LinearVelocity.Bounce(collisionInfo.GetNormal());
        }
    }
}
