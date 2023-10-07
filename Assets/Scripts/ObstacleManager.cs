using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] pipes;
    public GameObject[] grounds;

    // 초당 장애물이 움직이는 속도
    public float moveSpeedX = 3f;
    
    // 장애물 통과할 수 있는 높이의 범위
    public float pipePosMaxY = 1.5f;
    public float pipePosMinY = 0f;

    public void StartGame()
    {
        // 모든 파이프 위치 및 높이 설정
        for(int idx = 0; idx < pipes.Length; idx++)
        {
            Vector2 pos = pipes[idx].transform.position;
            pos.x = 4f + (idx * 2.5f);
            pos.y = Random.Range(pipePosMaxY, pipePosMinY);
            pipes[idx].transform.position = pos;
        }

        // 모든 땅 위치 설정
        for(int idx = 0; idx < grounds.Length; idx++)
        {
            Vector2 pos = grounds[idx].transform.position;
            pos.x = (idx * 6.24f);
            grounds[idx].transform.position = pos;
        }
    }

    public void ResetGame()
    {
        // 모든 파이프 위치 설정
        for (int idx = 0; idx < pipes.Length; idx++)
        {
            pipes[idx].transform.position = new Vector2(50, 0);
        }

        // 모든 땅 위치 설정
        for (int idx = 0; idx < grounds.Length; idx++)
        {
            Vector2 pos = grounds[idx].transform.position;
            pos.x = (idx * 6.24f);
            grounds[idx].transform.position = pos;
        }
    }

    public void update(float timeElapsed)
    {
        // 현재 프레임에 이동하는 거리
        float moveDistance = moveSpeedX * timeElapsed;

        // 파이프 이동
        for (int idx = 0; idx < pipes.Length; idx++)
        {
            MovePipe(idx, moveDistance);
        }

        // 땅 이동
        for (int idx = 0; idx < grounds.Length; idx++)
        {
            MoveGround(idx, moveDistance);
        }
    }

    // 파이프 이동시키는 함수
    private void MovePipe(int index, float moveDistance)
    {
        GameObject pipe = pipes[index];

        Vector2 pos = pipe.transform.position;
        pos.x -= moveDistance;

        if (pos.x <= -3.6)
        {
            int pipeArraySize = pipes.Length;
            int nextIndex = (index - 1 + pipeArraySize) % pipeArraySize;

            Vector2 lastPos = pipes[nextIndex].transform.position;
            pos.x = lastPos.x + 2.5f;
            pos.y = Random.Range(pipePosMinY, pipePosMaxY);
        }

        pipe.transform.position = pos;
    }

    // 땅 이동시키는 함수
    private void MoveGround(int index, float moveDistance)
    {
        GameObject ground = grounds[index];

        Vector2 pos = ground.transform.position;
        pos.x -= moveDistance;

        if (pos.x <= -6)
        {
            int groundArraySize = grounds.Length;
            int nextIndex = (index - 1 + groundArraySize) % groundArraySize;

            Vector2 lastPos = grounds[nextIndex].transform.position;
            pos.x = lastPos.x + 6.24f;
        }

        ground.transform.position = pos;
    }
}
