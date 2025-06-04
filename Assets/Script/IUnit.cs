using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public enum Team {None, P1, P2 }

public interface IUnit : IInitializable
{
    public Team Teams { get; }
    // 체력
    public float HP { get; }
    // 방어력
    public float Armor { get; }
    // 유닛 사망 시 발생하는 이벤트입니다.
    public UnityEvent DieEvent { get; }

    /// <summary>
    /// 데미지를 받는 쪽의 실제 HP 감소 연산을 수행합니다.
    /// <see cref="Armor"/>값이 높을수록 받는 데미지 양은 감소합니다.
    /// </summary>
    /// <param name="attack">데미지 받게 할 양입니다.</param>
    /// <returns>데미지를 받은 후 남은 HP양 입니다.</returns>
    public float TakeDemage(Attack attack);
}
