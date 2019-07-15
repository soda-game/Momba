using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace Action
{
    class FloorChip : Map
    {
        List<Vector2> floorChipPos = new List<Vector2>();

        public override bool Collition(Vector2 otherPos, int height)
        {
            Debug.WriteLine(otherPos.Y);
            int num = (int)otherPos.X / 64;
            if (otherPos.Y + height >= floorChipPos[num].Y)
            {
                return true;
            }
        
            return false;
        }

    public override void InList(float x, float y)
    {
        floorChipPos.Add(new Vector2(x, y));

    }
}
}
