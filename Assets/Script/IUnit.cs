using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public enum Team {None, P1, P2 }

public interface IUnit : IInitializable
{
    public Team Teams { get; }
    // ü��
    public float HP { get; }
    // ����
    public float Armor { get; }
    // ���� ��� �� �߻��ϴ� �̺�Ʈ�Դϴ�.
    public UnityEvent DieEvent { get; }

    /// <summary>
    /// �������� �޴� ���� ���� HP ���� ������ �����մϴ�.
    /// <see cref="Armor"/>���� �������� �޴� ������ ���� �����մϴ�.
    /// </summary>
    /// <param name="attack">������ �ް� �� ���Դϴ�.</param>
    /// <returns>�������� ���� �� ���� HP�� �Դϴ�.</returns>
    public float TakeDemage(Attack attack);
}
