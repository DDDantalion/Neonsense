using UnityEngine;

public class WalkMoveState : MoveState
{
    public override bool ShouldApplyGravity => !psm.IsGrounded;
    [SerializeField] private float groundAccelerationScalar = 8f, airAccelerationScalar=4f;

    public override void Update() {
        if (psm.IsGrounded && psm.TryingToStartSlide) { psm.ChangeState(psm.SlideState); return; 
        } else {
            if (psm.TryingToStartSlide) { psm.ChangeState(psm.SlamState);  return; }
            if (psm.WallRunState.RefreshWallEntry()) { psm.ChangeState(psm.WallRunState); return; }
        }
    }

    public override void MovePlayer() {
        float accScalar = psm.IsGrounded? groundAccelerationScalar : airAccelerationScalar;
        float maxAcceleration = psm.BaseSpeed * accScalar * Time.fixedDeltaTime;
        //float currentHeading = Vector3.Dot
        Vector3 acceleration = Vector3.ClampMagnitude(Vector3.ProjectOnPlane(psm.TargetDirection*psm.BaseSpeed - velocity, Vector3.up), maxAcceleration);
        rb.AddForce(Vector3.ProjectOnPlane(acceleration, psm.ContactNormal), ForceMode.VelocityChange);
    }
}