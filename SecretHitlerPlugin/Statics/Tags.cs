using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretHitlerPlugin
{
    static class Tags
    {
        public static readonly ushort SpawnPlayerTag = 0;
        public static readonly ushort DisconnectPlayerTag = 1;
        public static readonly ushort GameStartTag = 2;
        public static readonly ushort DrawCardsTag = 3;

        public static readonly ushort UpdateRoleTag = 4;
        public static readonly ushort UpdateColorTag = 5;
    }
}
