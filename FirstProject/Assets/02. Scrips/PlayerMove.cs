using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float rotateSpeed = 800.0f;
    // Start is called before the first frame update

    private float h => Input.GetAxis("Horizontal");
    private float v => Input.GetAxis("Vertical");

    private float r => Input.GetAxis("Mouse X");

    /// <summary>
    /// �ʱ�ȭ �̺�Ʈ
    /// Scene Load�� ȣ�� ( GameObject �� Ȱ��ȭ �Ǿ������� ȣ�� )
    /// ���� MoveBehavior �� ��Ȱ��ȭ �Ǿ��־ ȣ��
    /// �Ϲ� Ŭ������ ������ ������� �ַ� ���
    /// </summary>
    private void Awake()
    {
        
    }

    /// <summary>
    /// �ʱ�ȭ �̺�Ʈ
    /// GameObject �� Ȱ��ȭ �� ������ ȣ��
    /// </summary>
    private void OnEnable()
    {
        
    }

    /// <summary>
    /// �����ͻ󿡼� �ʱ�ȭ �̺�Ʈ
    /// GameObject �� �� MoveBehavior �� �߰������� ȣ�� (���� ȣ�⵵ ����)
    /// Play ��忡���� ȣ����� ����
    /// </summary>
    private void Reset()
    {
        
    }

    /// <summary>
    /// Fixed �����Ӹ��� ȣ��Ǵ� �̺�Ʈ
    /// ���������� ����Ǵ� ������ ó���Ҷ� ��� (����� ���ɿ� ������ ������ �ȵǴ� �����)
    /// </summary>
    public void FixedUpdate()
    {
        Vector3 dir = new Vector3(h, 0.0f, v).normalized;
        Vector3 deltaMove = dir * moveSpeed * Time.deltaTime;
        // transform.position += deltaMove;
        transform.Translate(deltaMove);

        Vector3 deltaRotate = Vector3.up * r * rotateSpeed * Time.deltaTime;
        transform.Rotate(deltaRotate);
    }

    /// <summary>
    /// �� �����Ӹ��� ȣ��Ǵ� �̺�Ʈ
    /// </summary>
    private void Update()
    {

    }

    /// <summary>
    /// �� �����Ӹ��� ȣ��Ǵ� �̺�Ʈ
    /// UpDate() ���Ŀ� ȣ���
    /// Ư�� Camera �̵����� � ���
    /// </summary>
    private void LateUpdate()
    {
        
    }

    /// <summary>
    /// Gizmos : ����� ���� ���ؼ� ȭ��� �׷����� ��� �׷����� ���
    /// Gizmos �� ������ �� ������ �����Ҷ����� ȣ��Ǵ� �Լ�
    /// </summary>
    private void OnDrawGizmos()
    {
        
    }

    /// <summary>
    /// �ش� MoveBehavior �� ������Ʈ�� ������ GameObject �� ���õǾ������� ȣ��Ǵ� �Լ�
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        
    }

    /// <summary>
    /// GUI : Graphical User Interface
    /// GUI �� �̺�Ʈ���� �ڵ鸵�ϴ� �Լ�
    /// </summary>
    private void OnGUI()
    {
        
    }

    /// <summary>
    /// ���� �Ͻ����� / �Ͻ����� ������ ȣ��
    /// </summary>
    /// <param name="pause"></param>
    private void OnApplicationPause(bool pause)
    {
        
    }

    /// <summary>
    /// ���� ���õ� ������ ����ɶ� ȣ�� ( ���ݾ��� ���õǸ� true, ���� �����ϸ� false )
    /// </summary>
    /// <param name="focus"></param>
    private void OnApplicationFocus(bool focus)
    {
        
    }

    /// <summary>
    /// ���� ����ɶ� ȣ��
    /// </summary>
    private void OnApplicationQuit()
    {
        
    }

    /// <summary>
    /// �� MoveBehavior �� ������Ʈ�� ������ GameObject�� ��Ȱ��ȭ �� �� ȣ��
    /// </summary>
    private void OnDisable()
    {
        
    }

    /// <summary>
    /// �� MoveBehavior �� ������Ʈ�� ������ GameObject �� �ı��ɶ� ȣ��
    /// </summary>
    private void OnDestroy()
    {
        // GameObject �� �����ϴ� ������ ���� �ȵ�
    }
}
