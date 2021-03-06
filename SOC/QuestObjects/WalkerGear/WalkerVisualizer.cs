﻿using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using SOC.Forms.Pages;
using SOC.UI;
using SOC.Classes.Common;

namespace SOC.QuestObjects.WalkerGear
{
    class WalkerVisualizer : LocationalVisualizer
    {
        public WalkerVisualizer(LocationalDataStub walkerStub, WalkerControl walkerControl) : base(walkerStub, walkerControl, walkerControl.panelQuestBoxes) { }
        
        public override void DrawMetadata(Metadata meta)
        {
            WalkerControl walkerControl = (WalkerControl)detailControl;
            walkerControl.SetMetadata((WalkerMetadata)meta);
        }

        public override Metadata GetMetadataFromControl()
        {
            return new WalkerMetadata((WalkerControl)detailControl);
        }

        public override QuestBox NewBox(QuestObject qObject)
        {
            return new WalkerBox((WalkerGear)qObject);
        }

        public override Detail NewDetail(Metadata meta, IEnumerable<QuestObject> qObjects)
        {
            return new WalkerDetail(qObjects.Cast<WalkerGear>().ToList(), (WalkerMetadata)meta);
        }

        public override QuestObject NewObject(Position objectPosition, int objectID)
        {
            return new WalkerGear(objectPosition, objectID);
        }
    }

}
