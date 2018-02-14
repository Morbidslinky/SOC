local this = {}
local quest_step = {}
local questWalkerGearList = {}

local StrCode32 = Fox.StrCode32
local StrCode32Table = Tpp.StrCode32Table
local GetGameObjectId = GameObject.GetGameObjectId
local ELIMINATE = TppDefine.QUEST_TYPE.ELIMINATE
local RECOVERED = TppDefine.QUEST_TYPE.RECOVERED
local CLEAR = TppDefine.QUEST_CLEAR_TYPE.CLEAR
local NONE = TppDefine.QUEST_CLEAR_TYPE.NONE
local FAILURE = TppDefine.QUEST_CLEAR_TYPE.FAILURE
local UPDATE = TppDefine.QUEST_CLEAR_TYPE.UPDATE

local hostageCount = 0
local CPNAME = ""
local useInter = true
local SUBTYPE = ""
local questTrapName = ""

local enemyQuestType = RECOVERED
local vehicleQuestType = RECOVERED
local hostageQuestType = RECOVERED
local animalQuestType = RECOVERED
local walkerQuestType = RECOVERED

this.QUEST_TABLE = {

	questType = ELIMINATE,
	soldierSubType = SUBTYPE,
	isQuestArmor =  false,
	isQuestZombie = false,
	isQuestBalaclava = false,

	cpList = {
	  nil
	},

	enemyList = {
	  
	},

	vehicleList = {
	  
	},

	heliList = {
	  
	},

	hostageList = {
	  
	},

	animalList = {
	  
	},

	walkerList = {
	  
	},

	targetList = {
	  
	},

	targetAnimalList = {
	
	},
}

this.InterCall_hostage_pos01 = function( soldier2GameObjectId, cpID, interName )
  Fox.Log("CallBack : Quest InterCall_hostage_pos01")

  for i,hostageInfo in ipairs(this.QUEST_TABLE.hostageList)do
	if hostageInfo.isTarget then
	  TppMarker.EnableQuestTargetMarker(hostageInfo.hostageName)
	else
	  TppMarker.Enable(hostageInfo.hostageName,0,"defend","map_and_world_only_icon",0,false,true)
	end
  end
end

this.questCpInterrogation = {
  { name = "enqt1000_271b10", func = this.InterCall_hostage_pos01, },
}

function this.OnAllocate()
  TppQuest.RegisterQuestStepList{
	"QStep_Start",
	"QStep_Main",
	nil
  }

  TppEnemy.OnAllocateQuestFova(this.QUEST_TABLE)
  TppQuest.RegisterQuestStepTable( quest_step )
  TppQuest.RegisterQuestSystemCallbacks{
	OnActivate = function()
	  this.OnActivateQuest(this.QUEST_TABLE)
	end,
	OnDeactivate = function()
	  this.OnDeactivateQuest(this.QUEST_TABLE)
	end,
	OnOutOfAcitveArea = function()
	end,
	OnTerminate = function()
	  this.OnTerminateQuest(this.QUEST_TABLE)
	end,
  }

  mvars.fultonInfo = NONE
end

function this.OnActivateQuest(questTable)
  TppEnemy.OnActivateQuest(questTable)
  TppAnimal.OnActivateQuest(questTable)
end

function this.OnDeactivateQuest(questTable)
  TppEnemy.OnDeactivateQuest(questTable)
  TppAnimal.OnDeactivateQuest(questTable)
end

function this.OnTerminateQuest(questTable)
  this.SwitchEnableQuestHighIntTable( false, CPNAME, this.questCpInterrogation )
  TppEnemy.OnTerminateQuest(questTable)
  TppAnimal.OnTerminateQuest(questTable)
end

this.Messages = function()
  return
	StrCode32Table {
	  Block = {
		{
		  msg = "StageBlockCurrentSmallBlockIndexUpdated",
		  func = function() end,
		},
	  },
	}
end

function this.OnInitialize()
  TppQuest.QuestBlockOnInitialize( this )
end

function this.OnUpdate()
  TppQuest.QuestBlockOnUpdate( this )
end

function this.OnTerminate()
  TppQuest.QuestBlockOnTerminate( this )
end

quest_step.QStep_Start = {
  OnEnter = function()
	InfCore.PCall(this.WarpHostages)
	InfCore.PCall(this.WarpVehicles)
	InfCore.PCall(this.SetupGearsQuest)
    this.BuildWalkerGameObjectIdList()

	this.SwitchEnableQuestHighIntTable( true, CPNAME, this.questCpInterrogation )
	TppQuest.SetNextQuestStep( "QStep_Main" )

	local commandScared =   {id = "SetForceScared",   scared=true, ever=true }
	local commandUnlocked = {id = "SetHostage2Flag",  flag="unlocked",   on=true,}
	local commandInjured =  {id = "SetHostage2Flag",  flag="disableFulton",on=true }
	local commandBrave =	{id = "SetHostage2Flag",  flag="disableScared",on=true }

	--Hostage Attributes List--

  end,
}

local walkerResetPosition
local walkerGearGameId
local inMostActiveQuestArea = true
local exitOnce = true
local hostagei = 0

quest_step.QStep_Main = {
  Messages = function( self )
	return
	  StrCode32Table {
		Marker = {
		  {
			msg = "ChangeToEnable",
			func = function ( arg0, arg1 )
			  Fox.Log("### Marker ChangeToEnable  ###"..arg0 )
			  if arg0 == StrCode32("Hostage_0") then
				hostagei = hostagei + 1
				if hostagei >= hostageCount then
				  this.SwitchEnableQuestHighIntTable( false, CPNAME, this.questCpInterrogation )
				end
			  end
			end
		  },
		},
		GameObject = {
		  {
			msg = "Dead",
			func = function(gameObjectId,gameObjectId01,animalId)
			  local isClearType = this.CheckQuestAllTargetDynamic("Dead",gameObjectId,animalId)
			  TppQuest.ClearWithSave( isClearType )
			end
		  },
		  {
			msg = "FultonInfo",
			func = function( gameObjectId )
			  if mvars.fultonInfo ~= NONE then
				TppQuest.ClearWithSave( mvars.fultonInfo )
			  end
			  mvars.fultonInfo = NONE
			end
		  },
		  {
			msg = "Fulton",
			func = function(gameObjectId,animalId)
			  local isClearType = this.CheckQuestAllTargetDynamic("Fulton", gameObjectId,animalId)
			  TppQuest.ClearWithSave( isClearType )
			end
		  },
		  {
			msg = "FultonFailed",
			func = function(gameObjectId,locatorName,locatorNameUpper,failureType)
			  if failureType == TppGameObject.FULTON_FAILED_TYPE_ON_FINISHED_RISE then
				local isClearType = this.CheckQuestAllTargetDynamic("FultonFailed",gameObjectId,locatorName)
				TppQuest.ClearWithSave( isClearType )
			  end
			end
		  },
		  {
			msg = "PlacedIntoVehicle",
			func = function( gameObjectId, vehicleGameObjectId )
			  if Tpp.IsHelicopter( vehicleGameObjectId ) then
				local isClearType = this.CheckQuestAllTargetDynamic("InHelicopter", gameObjectId)
				TppQuest.ClearWithSave( isClearType )
			  end
			end
		  },
		  {	
			msg = "VehicleBroken",
			func = function( gameObjectId, state )
			  if state == StrCode32("End") then
				local isClearType = this.CheckQuestAllTargetDynamic("VehicleBroken", gameObjectId )
				TppQuest.ClearWithSave( isClearType )
			  end
			end
		  },
		  {	
			msg = "LostControl",
			func = function( gameObjectId, state )
			  if state == StrCode32("End") then
				local isClearType = this.CheckQuestAllTargetDynamic( "LostControl", gameObjectId )
				TppQuest.ClearWithSave( isClearType )
			  end
			end
		  },
		},
		Trap = {
          {
            msg = "Exit", sender = questTrapName,
            func = function()
              inMostActiveQuestArea = false
              walkerGearGameId = vars.playerVehicleGameObjectId
              if questWalkerGearList[walkerGearGameId] then
                walkerResetPosition = {pos= {vars.playerPosX, vars.playerPosY + 1, vars.playerPosZ},rotY= 0,}
                GkEventTimerManager.Start("OutOfMostActiveArea", 8)
				exitOnce = this.OneTimeAnnounce("The Walker Gear cannot travel beyond this point.", "Return to the Side Op area.", exitOnce)
              end
            end
          },
          {
            msg = "Enter", sender = questTrapName,
            func = function()
              inMostActiveQuestArea = true
              if GkEventTimerManager.IsTimerActive("OutOfMostActiveArea") and walkerGearGameId == vars.playerVehicleGameObjectId then
                GkEventTimerManager.Stop("OutOfMostActiveArea")
				GkEventTimerManager.Start("AnnounceOnceCooldown", 5)
              end
            end
          },
        },
        Timer={
          {
            msg="Finish", sender="OutOfMostActiveArea",
            func = function()
              if inMostActiveQuestArea == false then
                InfCore.DebugPrint("Returning Walker Gear to Side Op area...")
                this.ReboundWalkerGear(walkerGearGameId)
              end
            end
          },
		  {
            msg="Finish", sender="AnnounceOnceCooldown",
            func = function()
			  exitOnce = true
            end
          },
        },
      }
  end,
  OnEnter = function()
	Fox.Log("QStep_Main OnEnter")
  end,
  OnLeave = function()
	Fox.Log("QStep_Main OnLeave")
  end,
}

function this.OneTimeAnnounce(announceString1, announceString2, isFresh)
  if isFresh == true then
    InfCore.DebugPrint(announceString1)
	InfCore.DebugPrint(announceString2)
  end
  
  return false
end

function this.BuildWalkerGameObjectIdList()
  for i,walkerInfo in ipairs(this.QUEST_TABLE.walkerList)do
    local walkerId = GetGameObjectId("TppCommonWalkerGear2",walkerInfo.walkerName)
    if walkerId ~= GameObject.NULL_ID then
      questWalkerGearList[walkerId] = walkerInfo.walkerName
    end
  end
end

function this.WarpHostages()
  for i,hostageInfo in ipairs(this.QUEST_TABLE.hostageList)do
	local gameObjectId= GetGameObjectId(hostageInfo.hostageName)
	if gameObjectId~=GameObject.NULL_ID then
	  local position=hostageInfo.position
	  local command={id="Warp",degRotationY=position.rotY,position=Vector3(position.pos[1],position.pos[2],position.pos[3])}
	  GameObject.SendCommand(gameObjectId,command)
	end
  end
end

function this.WarpVehicles()
  for i,vehicleInfo in ipairs(this.QUEST_TABLE.vehicleList)do
	local gameObjectId= GetGameObjectId(vehicleInfo.locator)
	if gameObjectId~=GameObject.NULL_ID then
	  local position=vehicleInfo.position
	  local command={id="SetPosition",rotY=position.rotY,position=Vector3(position.pos[1],position.pos[2],position.pos[3])}
	  GameObject.SendCommand(gameObjectId,command)
	end
  end
end

function this.SetupGearsQuest()
  for i,walkerInfo in ipairs(this.QUEST_TABLE.walkerList)do
    local walkerId = GetGameObjectId("TppCommonWalkerGear2",walkerInfo.walkerName)
    if walkerId ~= GameObject.NULL_ID then
      --[[ refuses to work
      local commandWeapon={ id = "SetMainWeapon", weapon = walkerInfo.weapon}
      GameObject.SendCommand(walkerId, commandWeapon)
      InfCore.Log("WG Weapon Set")
      ]]
      local commandColor = { id = "SetColoringType", type = walkerInfo.colorType }
      GameObject.SendCommand(walkerId, commandColor)
      InfCore.Log("WG Colour Set")

      local position = walkerInfo.position
      local commandPos={ id = "SetPosition", rotY = position.rotY, pos = position.pos}
      GameObject.SendCommand(walkerId,commandPos)
      InfCore.Log("WG Position Set")
    end
  end
end

function this.ReboundWalkerGear(walkerId)
  local commandPos={ id = "SetPosition", rotY = walkerResetPosition.rotY, pos = walkerResetPosition.pos}
  GameObject.SendCommand(walkerId,commandPos)
end

this.SwitchEnableQuestHighIntTable = function( flag, commandPostName, questCpInterrogation)
  local commandPostId = GetGameObjectId( "TppCommandPost2" , commandPostName )
  if useInter then
	if flag then

	  TppInterrogation.SetQuestHighIntTable( commandPostId, questCpInterrogation )
	else

	  TppInterrogation.RemoveQuestHighIntTable( commandPostId, questCpInterrogation )
	end
  end
end

function this.CheckQuestAllTargetDynamic(messageId, gameId, checkAnimalId)

  local dynamicQuestType = ELIMINATE
  local intendedTarget = true
  local objectiveCompleteCount = 0
  local objectiveFailedCount = 0
  local totalTargets = 0

  -- step 0: Confirm gameId is the target and set its new messageId
  local currentQuestName=TppQuest.GetCurrentQuestName()
  if TppQuest.IsEnd(currentQuestName) then
	return NONE
  end
  
  local inTargetList = false
  if mvars.ene_questTargetList[gameId] then
	local targetInfo = mvars.ene_questTargetList[gameId]
	if targetInfo.messageId ~= "None" and targetInfo.isTarget == true then
	  intendedTarget = false
	elseif targetInfo.isTarget == false then
	  intendedTarget = false
	end
	targetInfo.messageId = messageId or "None"
	inTargetList = true
  end
  
  if checkAnimalId ~= nil then
	local databaseId = TppAnimal.GetDataBaseIdFromAnimalId(checkAnimalId)

	for animalId, targetInfo in pairs(mvars.ani_questTargetList) do
	  if targetInfo.idType == "animalId" then
		if animalId == checkAnimalId then
		  targetInfo.messageId = messageId or "None"
		  inTargetList = true
		end
	  elseif targetInfo.idType == "databaseId" then
		if animalId == databaseId then
		  targetInfo.messageId = messageId or "None"
		  inTargetList=true
		end
	  elseif targetInfo.idType == "targetName" then
		local animalGameId = GetGameObjectId(animalId)
		if animalGameId == gameId then
		  targetInfo.messageId = messageId or "None"
		  inTargetList = true
		end
	  end
	end
  end

  if inTargetList == false then
	return NONE
  end

  for targetGameId, targetInfo in pairs(mvars.ene_questTargetList) do
	local isTarget = targetInfo.isTarget or false
	local targetMessageId = targetInfo.messageId

    --Step 1: Determine dynamicObjective
	if isTarget == true then
	  if Tpp.IsSoldier(targetGameId) then
		dynamicQuestType = enemyQuestType
	  elseif Tpp.IsHostage(targetGameId) then
		dynamicQuestType = hostageQuestType
	  elseif Tpp.IsVehicle(targetGameId) then
		dynamicQuestType = vehicleQuestType
	  elseif Tpp.IsEnemyWalkerGear(targetGameId) then
		dynamicQuestType = walkerQuestType
	  else
		dynamicQuestType = ELIMINATE
	  end
	  
	  --Step 2: Sort Messages into Complete and Failed counts
	  if targetMessageId ~= "None" then
		if dynamicQuestType == RECOVERED then
		  if (targetMessageId == "Fulton") or (targetMessageId == "InHelicopter") then
			objectiveCompleteCount = objectiveCompleteCount + 1
		  elseif (targetMessageId == "FultonFailed") or (targetMessageId == "Dead") or (targetMessageId == "VehicleBroken") or (targetMessageId == "LostControl") then
			objectiveFailedCount = objectiveFailedCount + 1
		  end
		elseif dynamicQuestType == ELIMINATE then -- setting recovery messages to add to objectiveFailedCount can allow for kill-required missions?
		  if (targetMessageId == "Fulton") or (targetMessageId == "InHelicopter") or (targetMessageId == "FultonFailed") or (targetMessageId == "Dead") or (targetMessageId == "VehicleBroken") or (targetMessageId == "LostControl") then
			objectiveCompleteCount = objectiveCompleteCount + 1
		  end
		end
	  end
	  totalTargets = totalTargets + 1

	end

  end

  --Step 1 & 2: For animals
  dynamicQuestType = animalQuestType
  for animalId, targetInfo in pairs(mvars.ani_questTargetList) do
	local targetMessageId = targetInfo.messageId

	if targetMessageId ~= "None" then
	  if dynamicQuestType == RECOVERED then
		if (targetMessageId == "Fulton") then
		  objectiveCompleteCount = objectiveCompleteCount + 1
		elseif (targetMessageId == "FultonFailed") or (targetMessageId == "Dead") then
		  objectiveFailedCount = objectiveFailedCount + 1
		end
	  elseif dynamicQuestType == ELIMINATE then
		if (targetMessageId == "Fulton") or (targetMessageId == "FultonFailed") or (targetMessageId == "Dead") then
		  objectiveCompleteCount = objectiveCompleteCount + 1
		end
	  end
	end
	totalTargets = totalTargets + 1
  end
  
  --Step 3: Determine Clear Type
  if totalTargets > 0 then
	if objectiveCompleteCount >= totalTargets then
	  return CLEAR
	elseif objectiveFailedCount > 0 then
	  return FAILURE
	elseif objectiveCompleteCount > 0 then
	  if intendedTarget == true then
		return UPDATE
	  end
	end
  end
  
  return NONE
end

return this