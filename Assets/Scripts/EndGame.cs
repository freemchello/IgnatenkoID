using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider player) //��� �������������� � ��������� ���� �����������
    {
        if (player.CompareTag("Player"))
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("EndGame");
        }
    }
}
