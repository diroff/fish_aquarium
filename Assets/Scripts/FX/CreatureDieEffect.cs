using UnityEngine;

public class CreatureDieEffect : MonoBehaviour
{
    private EffectCreator _creator;

    public void ShowEffect(Vector2 position)
    {
        transform.position = position;
    }

    public void HideEffect()
    {
        BackOnCreator();
        gameObject.SetActive(false);
    }

    public void SetEffectCreator(EffectCreator creator)
    {
        _creator = creator;
    }

    public void BackOnCreator()
    {
        if (_creator == null)
            return;

        _creator.ReturnToPool(this);
    }
}