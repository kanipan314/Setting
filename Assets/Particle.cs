using UnityEngine;

public class Particle : MonoBehaviour
{
    //消滅するまでの時間
    private float lifeTime;
    //消滅するまでの残り時間
    private float leftlifeTime;
    //移動量
    private Vector3 velocity;
    //初期Scale
    private Vector3 defaultScale;
    // 初期位置
    public Vector3 initialPosition;

    public void PlayerPosition(Vector3 MoveToPosition)
    {
        initialPosition.x = MoveToPosition.x;
        initialPosition.y = MoveToPosition.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        //消滅するまでの時間を0.3秒とする
        lifeTime = 0.3f;
        //残り時間を初期化
        leftlifeTime = lifeTime;
        //現在のScaleを記録
        defaultScale = transform.localScale;
        //ランダムで決まる移動量の最大値
        float maxVelocity = 5;
        //各方向へランダムで飛ばす
        velocity = new Vector3(
            Random.Range(-maxVelocity, maxVelocity),
            Random.Range(-maxVelocity, maxVelocity),
            0
            );

    }

    // Update is called once per frame
    void Update()
    {
        //残り時間をカウントダウン
        leftlifeTime -= Time.deltaTime;
        // 自身の座標を移動
        transform.position += velocity * Time.deltaTime;
        //残り時間でドンドン小さく
        transform.localScale = Vector3.Lerp(new Vector3(0,0,0),defaultScale, leftlifeTime / lifeTime);

        //残り時間が０以下になったら自身のゲームオブジェクトを消滅
        if (leftlifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
