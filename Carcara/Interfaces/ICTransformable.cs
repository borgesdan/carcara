using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Xna.Framework
{
    public interface ICTransformable
    {
        CTransform Transform { get; set; }
    }
}