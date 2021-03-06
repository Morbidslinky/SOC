﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    public class CheckQuestMethodsList : LuaMainComponent
    {
        List<CheckQuestMethodsPair> CheckQuestMethods = new List<CheckQuestMethodsPair>();

        public void Add(CheckQuestMethodsPair pair)
        {
            CheckQuestMethods.Add(pair);
        }

        public bool Contains(CheckQuestMethodsPair methodsPair)
        {
            return (CheckQuestMethods.Exists(pair => pair.TallyMethod.Equals(methodsPair.TallyMethod) || pair.TargetMessageMethod.Equals(methodsPair.TargetMessageMethod)));
        }

        public override string GetComponent()
        {

            return $@"{GetCheckFunctions()}
{GetList()}
";
        }

        private string GetCheckFunctions()
        {
            StringBuilder checkQuestBuilder = new StringBuilder();
            foreach (CheckQuestMethodsPair pair in CheckQuestMethods)
            {
                checkQuestBuilder.Append($@"{pair.TargetMessageMethod.FunctionFull}
{pair.TallyMethod.FunctionFull}
");
            }
            return checkQuestBuilder.ToString();
        }

        private string GetList()
        {
            StringBuilder checkQuestBuilder = new StringBuilder();
            checkQuestBuilder.Append(@"local CheckQuestMethodList = {");

            foreach (CheckQuestMethodsPair pair in CheckQuestMethods)
            {
                checkQuestBuilder.Append($@"
  {pair.GetTableFormat()},");
            }
            checkQuestBuilder.Append(@"
}");
            return checkQuestBuilder.ToString();
        }
    }

    public abstract class CheckQuestMethodsPair
    {
        public LuaFunction TargetMessageMethod, TallyMethod;

        public CheckQuestMethodsPair(MainLua mainLua, LuaFunction a, LuaFunction b, string targetTableName, LuaFunction check, string objective)
        {
            TargetMessageMethod = a; TallyMethod = b;
            mainLua.AddToCheckQuestMethod(this);
            mainLua.AddToObjectiveTypes(targetTableName, new GenericTargetPair(check, objective));
        }

        public CheckQuestMethodsPair(MainLua mainLua, LuaFunction a, LuaFunction b, string oneLineObjective)
        {
            TargetMessageMethod = a; TallyMethod = b;
            mainLua.AddToCheckQuestMethod(this);
            mainLua.AddToObjectiveTypes(oneLineObjective);
        }

        public CheckQuestMethodsPair(MainLua mainLua, LuaFunction a, LuaFunction b)
        {
            TargetMessageMethod = a; TallyMethod = b;
            mainLua.AddToCheckQuestMethod(this);
        }

        public string GetTableFormat()
        {
            return $"{{IsTargetSetMessageMethod = this.{TargetMessageMethod.FunctionName}, TallyMethod = this.{TallyMethod.FunctionName}}}";
        }
    }
}
