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
        //在地面上并且有攻击信号
        if(comboReusableData.hasATKCommand.Value && animator.GetBool(AnimatorID.isGrounded))
        {
            animator.CrossFade("Attack1", 0.111f, 0);
        }

        if(comboReusableData.hasATKCommand.Value && animator.GetBool(AnimatorID.isGrounded) == false && comboReusableData.InputY > 0)
        {
            animator.CrossFade("UpAttack", 0.1111f, 0);
        }
        if(animator.GetBool(AnimatorID.isGrounded) == false)
            comboReusableData.hasATKCommand.Value = false;
    }


}