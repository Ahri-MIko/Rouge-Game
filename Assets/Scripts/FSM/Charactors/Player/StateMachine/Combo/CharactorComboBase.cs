using UnityEngine;

public class CharactorComboBase
{

    protected Animator animator;

    protected PlayerReusableData reusableData;

    protected PlayerComboReusableData comboReusableData;

    public CharactorComboBase(Animator anim,PlayerReusableData PlayerRD,PlayerComboReusableData playerComboReusableData)
    {
        animator = anim;
        this.reusableData = PlayerRD;
        comboReusableData = playerComboReusableData;
    }


    public virtual bool canComboInput()
    {
        //目前一直都是永久可触发,后续细化需要修改触发条件
        if (!reusableData.canInput) { return false; }
        return true;
    }


    public virtual void LightComboInput()
    { 
        ExecuteBaseCombo();
    }

    public virtual void ExecuteBaseCombo()
    {
        comboReusableData.hasATKCommand.Value = true;
    }

    public virtual void UpdateComboAnimation()
    {


        if(comboReusableData.hasATKCommand.Value && animator.GetBool(AnimatorID.isGrounded))
        {
            comboReusableData.isCharging.Value = true;
        }

        if(comboReusableData.isCharging.Value == true)
        {
            comboReusableData.chargeTime += Time.deltaTime;
            if (comboReusableData.chargeTime >= comboReusableData.chargeThreshold) Debug.Log("蓄力当中!!!");
                animator.CrossFade("ChargUp", 0.111f, 0);//进入蓄力状态
        }
        if(CharactorInputSystem.Instance.AttackWasReleasedThisFrame)
        {
            if (comboReusableData.chargeTime < comboReusableData.chargeThreshold && animator.GetBool(AnimatorID.isGrounded))
                animator.CrossFade("Attack1", 0.111f, 0);//进入普通攻击状态
            else
                comboReusableData.isChargeComplete.Value = true;

             comboReusableData.isCharging.Value = false;
            comboReusableData.chargeTime = 0.0f;
        }

        //空中的状态
        if(comboReusableData.hasATKCommand.Value && animator.GetBool(AnimatorID.isGrounded) == false && comboReusableData.InputY > 0)
        {
            animator.CrossFade("UpAttack", 0.1111f, 0);
        }
        if(animator.GetBool(AnimatorID.isGrounded) == false)
            comboReusableData.hasATKCommand.Value = false;
    }


}