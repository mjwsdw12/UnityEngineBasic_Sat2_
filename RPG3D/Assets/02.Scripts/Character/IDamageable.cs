using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal interface IDamageable
{
    public float hp { get; set; }
    public float hpMin { get; }
    public float hpMax { get; }

    public event Action<float> OnHPChanged;
    public event Action OnHPMin;
    public event Action OnHPMax;

    public void Damage(float amount);
}