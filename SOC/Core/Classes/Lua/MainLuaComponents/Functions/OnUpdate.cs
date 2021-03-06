﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    class OnUpdate : LuaMainComponent
    {
        List<string> onUpdateList = new List<string>();

        public override string GetComponent()
        {
            StringBuilder onUpdateBuilder = new StringBuilder(@"
function this.OnUpdate()
  TppQuest.QuestBlockOnUpdate(this)");

            foreach (string code in onUpdateList)
            {
                onUpdateBuilder.Append($@"
  {code}");
            }
            onUpdateBuilder.Append(@"
end");

            return onUpdateBuilder.ToString();
        }

        public void Add(string code)
        {
            onUpdateList.Add(code);
        }
    }
}
