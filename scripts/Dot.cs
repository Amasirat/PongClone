using Godot;

public partial class Dot : RigidBody2D
{
    public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
        var collisionInfo = MoveAndCollide(LinearVelocity);
        if(collisionInfo != null)
        {
            state.LinearVelocity = LinearVelocity.Bounce(collisionInfo.GetNormal());
        }
    }
}
