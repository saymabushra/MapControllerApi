using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapControllerWebApi.Models
{
    public class ResponseMessage<T>
    {
        private EnumMessageType _messageType;
        public T Result { get; set; }
        public EnumMessageType MessageType
        {
            get => _messageType;

            set
            {
                switch (value)
                {
                    case EnumMessageType.Success:
                        this.Success = true;
                        break;
                    case EnumMessageType.Warning:
                        this.Success = true;
                        break;
                    case EnumMessageType.Error:
                        this.Success = false;
                        break;

                    default:
                        break;
                }
                _messageType = value;
            }
        }
        public bool Success { get; private set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }

    public enum EnumMessageType
    {
        Success = 1,
        Warning = 2,
        Error = 3

    }
}
