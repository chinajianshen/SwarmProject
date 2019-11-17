using System;
using System.Collections.Generic;
using System.Text;

namespace JianShen.Swarm.Common.Message
{
    /// <summary>
    /// 响应消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
   public class ReponseMessage<T> where T:class
    {
        public ReponseMessage()
        {
            IsSuccess = true;
        }

        public T Data { get; set; }
        public bool IsSuccess { get; set; }

        public MessageInfo Message
        {
            get; set;
        }
    }
}
