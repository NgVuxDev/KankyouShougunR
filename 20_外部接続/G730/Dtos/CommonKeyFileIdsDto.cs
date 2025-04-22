using System.Collections.Generic;

namespace Shougun.Core.ExternalConnection.FileUpload.Dtos
{
    public class CommonKeyFileIdsDto<T>
    {
        public T Id { get; set; }
        public List<long> FileIds { get; set; }
    }
}
