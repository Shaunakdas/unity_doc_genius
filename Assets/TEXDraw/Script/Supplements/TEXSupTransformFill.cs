using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace TexDrawLib
{
    [AddComponentMenu("TEXDraw/Supplemets/TEXSup Transform Fill", 16), ExecuteInEditMode]
    [TEXSupHelpTip("Transform UV2 mapping", true)]
	public class TEXSupTransformFill : TEXDrawMeshEffectBase
    {
        public Vector2 offset = Vector2.zero;
        public Vector2 scale = Vector2.one;
        public bool repeat01;
        
        public override void ModifyMesh(Mesh m)
        {
            var uv2 = m.uv2;
            var oX = offset.x;
            var oY = offset.y;
            var sX = scale.x;
            var sY = scale.y;
            var rep = repeat01;
            for (int i = uv2.Length; i-- > 0 ;)
            {
                var v = uv2[i];
                v = new Vector2((v.x * sX + oX), v.y * sY + oY);
                if (rep) {
                    v.x = Mathf.Repeat(v.x, 1);
                    v.y = Mathf.Repeat(v.y, 1);
                }
                uv2[i] = v;
            }
            m.uv2 = uv2;
                            
        }        
    }
}
