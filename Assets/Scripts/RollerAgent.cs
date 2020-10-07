using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class RollerAgent : Agent
{
    public Transform target;
    Rigidbody rb;

    // 3～6のオーバーライドメソッドを追加

    /// <summary>
    /// 初期化時に呼ばれる
    /// </summary>
    public override void Initialize()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// エピソード開始時に呼ばれる
    /// </summary>
    public override void OnEpisodeBegin()
    {
        //床から落下している時
        if (this.transform.position.y < 0)
        {
            //位置と速度をリセット
            this.rb.angularVelocity = Vector3.zero;
            this.rb.velocity = Vector3.zero;
            this.transform.position = new Vector3(0f, 0.5f, 0f);
        }

        //Targetの位置リセット
        target.position = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);

    }

    /// <summary>
    /// 観察取得時に呼ばれる
    /// </summary>
    /// <param name="sensor"></param>
    public override void CollectObservations(VectorSensor sensor)
    {
        //TargetのXYZ座標
        sensor.AddObservation(target.position);

        //RollerのXYZ座標
        sensor.AddObservation(this.transform.position);

        //Rollerの速度X
        sensor.AddObservation(rb.velocity.x);

        //Rollerの速度Z
        sensor.AddObservation(rb.velocity.z);
    }

    /// <summary>
    /// 行動実行時に呼ばれる
    /// </summary>
    /// <param name="vectorAction"></param>
    public override void OnActionReceived(float[] vectorAction)
    {
        //RollerAgentに力を加える（進ませる）
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        rb.AddForce(controlSignal * 10);

        //RollerAgentがTargetに到着した時
        float distanceToTarget = Vector3.Distance(this.transform.position, target.position);

        if (distanceToTarget < 1.42f)
        {
            AddReward(1.0f);
            EndEpisode();
            SoundManager.instance.PlaySE(SoundManager.SE_Type.OK);
            StageManager.instance.SuccessCount();

            if (StageManager.instance.successCount % 5 == 0)
            {
                Debug.Log("BGM変更とお褒めのお言葉");
                SoundManager.instance.PlayBGM(SoundManager.BGM_Type.Game1);
                SoundManager.instance.PlayBGM(SoundManager.BGM_Type.Game2);
                SoundManager.instance.PlayVOICE(SoundManager.VOICE_Type.U1);

            }

        }

        //RollerAgentが床から落下した時
        if (this.transform.position.y < 0)
        {
            EndEpisode();
            SoundManager.instance.PlaySE(SoundManager.SE_Type.NG);
            StageManager.instance.FailureCount();

            if (StageManager.instance.failureCount % 5 == 0)
            {
                Debug.Log("Unityちゃんの激励");
                SoundManager.instance.PlayVOICE(SoundManager.VOICE_Type.U2);
            }

        }
    }

    /// <summary>
    /// ヒューリスティックモードの行動決定時に呼ばれる
    /// </summary>
    /// <param name="actionsOut"></param>
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
    }


}
