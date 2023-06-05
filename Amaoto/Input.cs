using DxLibDLL;

namespace Amaoto
{
    /// <summary>
    /// 入力を管理するクラス。
    /// </summary>
    public class Input
    {
        /// <summary>
        /// 入力を管理するクラス。
        /// </summary>
        public Input()
        {
            Keys = new int[256];
            Buffer = new byte[256];
            BeforeBuffer = new byte[256];
        }

        /// <summary>
        /// キー入力の状態をチェックします。毎フレーム呼び出す必要があります。
        /// </summary>
        public void Update()
        {
            DX.GetHitKeyStateAll(Buffer);
            for (int i = 0; i < 256; i++)
            {
                if (Buffer[i] == 1)
                {
                    if (Keys[i] == 0) Keys[i] = 1;
                    else if (Keys[i] == 1) Keys[i] = 2;
                }
                else
                {
                    if (Buffer[i] != BeforeBuffer[i])
                        Keys[i] = -1;
                    else
                        Keys[i] = 0;
                }
                BeforeBuffer[i] = Buffer[i];
            }
        }

        /// <summary>
        /// そのキーが入力されたかどうかチェックします。
        /// </summary>
        /// <param name="key">キーコード。</param>
        /// <returns>入力されたかどうか。</returns>
        public bool IsPushedKey(int key)
        {
            return Keys[key] == 1;
        }

        /// <summary>
        /// そのキーが入力されているかどうかチェックします。
        /// </summary>
        /// <param name="key">キーコード。</param>
        /// <returns>入力されているかどうか。</returns>
        public bool IsPushingKey(int key)
        {
            return Keys[key] > 0;
        }

        /// <summary>
        /// そのキーが離されたかどうかチェックします。
        /// </summary>
        /// <param name="key">キーコード。</param>
        /// <returns>入力されているかどうか。</returns>
        public bool IsReleasedKey(int key)
        {
            return Keys[key] < 0;
        }

        private readonly int[] Keys;
        private readonly byte[] Buffer;
        private readonly byte[] BeforeBuffer;
    }
}
