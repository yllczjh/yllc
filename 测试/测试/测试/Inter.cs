using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 测试
{

    public interface I_PayHelper
    {
        /// <summary>
        /// 支付交易
        /// </summary>
        /// <param name="inParams">支付交易入参</param>
        /// <returns>返回支付交易结果</returns>
        void Pay();

        /// <summary>
        /// 支付退费交易
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        void UnPay();
    }

}
