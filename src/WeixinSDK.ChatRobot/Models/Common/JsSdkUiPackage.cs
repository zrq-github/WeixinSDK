﻿namespace WeixinSDK.Work.Models.Common
{
    /// <summary>
    ///     为UI输出准备的JSSDK信息包
    /// </summary>
    public class JsSdkUiPackage
    {
        public JsSdkUiPackage(string appId, string timestamp, string nonceStr, string signature)
        {
            AppId = appId;
            Timestamp = timestamp;
            NonceStr = nonceStr;
            Signature = signature;
        }

        /// <summary>
        ///     微信AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        ///     时间戳
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        ///     随机码
        /// </summary>
        public string NonceStr { get; set; }

        /// <summary>
        ///     签名
        /// </summary>
        public string Signature { get; set; }
    }
}