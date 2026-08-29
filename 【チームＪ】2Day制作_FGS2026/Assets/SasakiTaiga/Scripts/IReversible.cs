using UnityEngine;

/// <summary>
/// 反転機能を持つオブジェクトが実装すべきインターフェース
/// </summary>
public interface IReversible
{
    /// <summary>
    /// 反転しているかどうか
    /// </summary>
    protected bool isRevered {  get; set; }

    /// <summary>
    /// 反転した瞬間に呼ばれる関数
    /// </summary>
    void OnReversed();
}