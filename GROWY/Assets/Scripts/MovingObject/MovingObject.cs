using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [Header("OBJECT FEATURE")]
    public string objectName;
    public MovingObjectType objectType;

    [Header("WALK")]
    public float walkSpeed;
    public int walkCount;
    public int currentWalkCount;

    [Header("BOX COLLIDER")]
    public BoxCollider2D boxCollider;

    [Header("ANIMATOR")]
    public Animator animator;

    [Header("LAYER MASK")]
    public LayerMask layerMask;

    protected Vector3 dir_vector;
    protected Queue<string> moving_queue;

    protected bool bMoving = false;
    protected bool RefindPath = false;
    protected bool findPlayer = false;

    private bool notCoroutine = false;      // 코루틴의 중복실행 방지

    public void Move(string _dir, int _frequency = 5)
    {
        moving_queue.Enqueue(_dir);
        if (!notCoroutine)
        {
            notCoroutine = true;
            StartCoroutine(MoveCoroutine(_dir, _frequency));

            //GlobalValue.instance.SetSortOrder();
        }
    }

    protected IEnumerator MoveCoroutine(string _dir, int _frequency)
    {
        int tile_size = DefsGame.TileSize;

        while (moving_queue.Count != 0)
        {
            // 대기
            switch (_frequency)
            {
                case 1:
                    yield return new WaitForSeconds(4.0f);
                    break;
                case 2:
                    yield return new WaitForSeconds(3.0f);
                    break;
                case 3:
                    yield return new WaitForSeconds(2.0f);
                    break;
                case 4:
                    yield return new WaitForSeconds(1.0f);
                    break;
                case 5:
                    break;
            }

            string direction = moving_queue.Dequeue();
            dir_vector.Set(0, 0, dir_vector.z);     // x,y가 동시에 1을 가지면 안되게 하기위해

            switch (direction)
            {
                case "UP":
                    dir_vector.y = 1.0f;
                    break;
                case "DOWN":
                    dir_vector.y = -1.0f;
                    break;
                case "RIGHT":
                    dir_vector.x = 1.0f;
                    break;
                case "LEFT":
                    dir_vector.x = -1.0f;
                    break;
            }

            animator.SetFloat("DirX", dir_vector.x);
            animator.SetFloat("DirY", dir_vector.y);
            
            // 충돌 방지
            while (true)
            {
                if (CheckCollision())
                {
                    animator.SetBool("Walking", false);

                    // 다른 캐릭터와 부딪히면 0.1초동안 멈춤
                    yield return new WaitForSeconds(0.1f);
                }
                else
                {
                    break;
                }
            }

            animator.SetBool("Walking", true);

            // 방법 1
            //boxCollider.offset = new Vector2(vector.x * 0.7f * speed * walkCount, vector.y * 0.7f * speed * walkCount);

            // 방법 2
            boxCollider.size = new Vector2(Mathf.Abs(dir_vector.x) * tile_size + tile_size, Mathf.Abs(dir_vector.y) * tile_size + tile_size);

            float distance = 0.0f;

            while (currentWalkCount < walkCount)
            {
                bMoving = true;

                float w = walkSpeed * Time.deltaTime;
                distance += w;
                transform.Translate(dir_vector.x * w, dir_vector.y * w, 0);

                // 32를 넘었다면 넘은 값만큼 빼줘서 32에 맞춰준다.
                if (CorrectGap(distance))
                    break;

                currentWalkCount++;

                if (currentWalkCount / walkCount >= 0.7f)
                    boxCollider.size = new Vector2(tile_size, tile_size);

                yield return new WaitForSeconds(0.01f);
            }

            // 정확하게 32만큼 움직이지 못 했을 때, 마지막 프레임에 그만큼 보정해서 32를 맞춰준다.
            CorrectGap(distance);

            distance = 0.0f;
            currentWalkCount = 0;

            if (currentWalkCount == 0)
                bMoving = false;
            
            if (_frequency != 5)
                animator.SetBool("Walking", false);     // 애니메이션 중단
        }

        animator.SetBool("Walking", false);
        notCoroutine = false;
    }
    
    protected bool CheckCollision()
    {
        int tile_size = DefsGame.TileSize;

        RaycastHit2D hit;
        Vector2 start = transform.position;                                                             // 캐릭터의 현재 위치값
        Vector3 end = start + new Vector2(dir_vector.x * tile_size, dir_vector.y * tile_size);      // 캐릭터가 이동하고자 하는 목표 위치값

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, layerMask);
        boxCollider.enabled = true;

        RefindPath = false;
        findPlayer = false;

        if (hit.transform != null)
        {
            if (hit.transform.name.Contains("Player"))
                findPlayer = true;
            else
                RefindPath = true;

            return true;
        }

        return false;
    }

    // 이동한 거리가 정확히 object_size와 일치하게 보정
    private bool CorrectGap(float distance)
    {
        float tile_size = DefsGame.TileSize;

        if (distance != tile_size)
        {
            float gap = tile_size - distance;
            transform.Translate(dir_vector.x * gap, dir_vector.y * gap, 0);

            return true;
        }

        return false;
    }
    
    // Transform.position(Vector3)를 MAP의 x,y(Vector2Int)로 변환해주는 함수
    public Vector2Int GetPos()
    {   
        return new Vector2Int(Mathf.RoundToInt(transform.position.x / 32.0f), Mathf.RoundToInt(transform.position.y / 32.0f));
    }
}